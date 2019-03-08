import argparse
import glob
import os
import pathlib
import platform

from fodyclean import FodyCleaner
from processrun import ProcessRunner
from shyprint import Logger, LogLevel


class NetBuilder:


    def __init__(self):
        print("NetBuilder\n")
        self.parser = argparse.ArgumentParser(description='Build VS solution along with project tests.')
        self.parser.add_argument("--slnpath", "-s", type=str, metavar="SolutionPath", default=None, help="The path to the solution to build.")
        self.parser.add_argument("--config", "-c", choices=["Release", "Debug"], type=str, metavar="ConfigurationName", default="Debug", help="The ConfigurationName to be used with MSBuild.")


    def build(self, silent = False):
        # Initialize shyprint
        self.logger = Logger(self)
        self.logger.silent = silent

        # Initialize processrun
        self.process_run = ProcessRunner(silent)        

        # Parse CLI args
        args = self.parser.parse_args()
        sln_path = args.slnpath
        config_name = args.config
        
        self.logger.log(f"Solution path: {sln_path}"
                        + f"\nConfiguration: {config_name}",
                        LogLevel.WARNING)

        if (sln_path == None): self.exit_with_error(1, "Solution path not provided for build.")

        result = self.run_nuget_restore(sln_path)
        if (result.status != 0): self.exit_with_error(result.status, "NuGet packages for solution were not restored correctly.")

        exit_code = self.run_fody_cleaner()
        if (exit_code != 0): self.exit_with_error(exit_code, "Fody references in projects were not removed successfully.")

        result = self.run_msbuild(sln_path, config_name)
        if (result.status != 0): self.exit_with_error(result.status, "MSBuild failed to run successfully on solution.")

        self.logger.log("Build was successful.", LogLevel.SUCCESS)


    def run_fody_cleaner(self):
        self.logger.log_linebreaks(2)
        self.logger.log("Cleaning Fody references before build...", LogLevel.WARNING)
        fody_cleaner = FodyCleaner()
        return fody_cleaner.clear_refs()


    def run_nuget_restore(self, sln_path):
        self.logger.log_linebreaks(2)
        self.logger.log("Running nuget restore...", LogLevel.WARNING)
        return self.process_run.run_line(f"nuget restore {sln_path}")


    def run_msbuild(self, sln_path, config_name):
        self.logger.log_linebreaks(2)
        self.logger.log("Building solution...", LogLevel.WARNING)
        msbuild_path = self.locate_msbuild()
        cmd_line = f"{msbuild_path} {sln_path} -p:Configuration={config_name}"
        return self.process_run.run_line(cmd_line)

        
    def locate_vs_installation(self):
        program_files_location = os.environ.get("ProgramFiles")
        if program_files_location == None:
            self.logger.log("No ProgramFiles entry found in environment.", LogLevel.WARNING)
            return None
        vswhere_path = program_files_location + "/Microsoft Visual Studio/Installer/vswhere.exe"
        vswhere_cmd = f"{vswhere_path} -latest -requires Microsoft.Component.MSBuild"

        final_path = None

        result = self.process_run.run_line(vswhere_cmd)

        for item in result.output.splitlines():
            split_item = item.split(":", 1)
            if (split_item[0] == "installationPath"):
                final_path = split_item[1].strip()
                self.logger.log(f"Found VS Installation at: {final_path}", LogLevel.SUCCESS)
                self.logger.log_linebreaks(2)

        return final_path


    def locate_msbuild(self):
        if (platform.system() != "Windows"):
            self.logger.log("Not on Windows. MSBuild is usually available on PATH here.", LogLevel.WARNING)
            return "msbuild"
            
        self.logger.log("On Windows OS. Locating MSBuild via vswhere...")
        vs_installation_path = self.locate_vs_installation()
        if (vs_installation_path == None):
            self.logger.log("VS Installation was not found.", LogLevel.ERROR)
            return None
        
        vs_msbuild_pattern = "MSBuild/*/Bin/MSBuild.exe"
        locations = glob.glob(os.path.join(vs_installation_path, vs_msbuild_pattern))
        for location in sorted(locations, reverse=True):
            if self.is_file(location): return location

        return None


    def exit_with_error(self, error_code, error_msg):
        self.logger.log("ERROR! Exiting..."
                        + f"\nError code: {str(error_code)}"
                        + f"\nError message: {error_msg}",
                        LogLevel.ERROR)
        exit(error_code)


    def is_file(self, location):
        path = pathlib.Path(location)
        return path.is_file



if __name__ == "__main__":
    builder = NetBuilder()
    builder.build(False)
