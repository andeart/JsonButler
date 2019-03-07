import argparse
import collections
import glob
import os
import pathlib
import platform
import subprocess

from fodycleaner import FodyCleaner


class SlnBuilder:


    def __init__(self):
        print("SlnBuilder\n")
        self.parser = argparse.ArgumentParser(description='Build VS solution along with project tests.')
        self.parser.add_argument("--config", "-c", choices=["Release", "Debug"], type=str, metavar="ConfigurationName", default="Debug", help="The ConfigurationName to be used with MSBuild.")
        self.parser.add_argument("--runtests", "-r",  action="store_true", default=False, help="Should tests be run? Use as a flag, i.e. simply add -r or --runtests without additional args.")
        self.parser.add_argument("--slnpath", "-s", type=str, metavar="SolutionPath", default=None, help="The path to the solution to build.")
        self.parser.add_argument("--testspath", "-t", type=str, metavar="TestsPath", default=None, help="The path to the tests assembly.")


    def build(self, silent = False):
        self.print_if_loud ("Running build...", silent)
        args = self.parser.parse_args()
        config_name = args.config
        should_run_tests = args.runtests
        sln_path = args.slnpath
        tests_path = args.testspath
        self.print_if_loud("Configuration: " + config_name + "\n"
                            + "Should run tests?: " + str(should_run_tests),
                            silent)

        if (sln_path == None):
            self.exit_with_error_msg("Solution path not provided for build.", silent)

        result = self.run_nuget_restore(sln_path, silent)        
        if (result.status != 0):
            self.exit_with_error_code(result.status, silent)

        exit_code = self.run_fody_cleaner()
        if (exit_code != 0):
            self.exit_with_error_code(exit_code, silent)

        result = self.run_msbuild(sln_path, config_name, silent)
        if (result.status != 0):
            self.exit_with_error_code(result.status, silent)

        self.print_if_loud("Build was successful.", silent)

        if config_name == "Debug" and should_run_tests:
            if (tests_path == None):
                self.exit_with_error_msg("Tests path not provided for testing.", silent)
            result = self.run_vstest(tests_path, silent)
            if (result.status != 0):
                self.exit_with_error_code(result.status, silent)
            self.print_if_loud("Tests run successfully.", silent)
        else:
            self.print_if_loud("No tests were run", silent)


    def exit_with_error_code(self, error_int, silent = False):
        if (error_int != 0):
            self.print_if_loud("Exiting with error-code: " + str(error_int), silent)
            exit(error_int)


    def exit_with_error_msg(self, error_msg, silent = False):
        self.print_if_loud("Exiting with error: " + error_msg, silent)
        exit(1)


    def run_fody_cleaner(self, silent = False):
        self.print_linebreaks(4, silent)
        self.print_if_loud("Cleaning Fody references before build...", silent)
        fody_cleaner = FodyCleaner()
        return fody_cleaner.clear_fody_refs()


    def run_nuget_restore(self, sln_path, silent = False):
        self.print_linebreaks(4, silent)
        self.print_if_loud("Running nuget restore...", silent)
        args = ["nuget"]
        args.append("restore")
        args.append(sln_path)
        return self.run_os_process(args, silent)


    def run_msbuild(self, sln_path, config_name, silent = False):
        self.print_linebreaks(4, silent)
        self.print_if_loud("Building solution...", silent)
        args = []
        if (platform.system() == "Windows"):
            self.print_if_loud("On Windows. Locating MSBuild via vswhere...", silent)
            msbuild_location = self.locate_msbuild()
            args.append(msbuild_location)
        else:
            args.append("msbuild")
        args.append(sln_path)
        args.append("-p:Configuration=" + config_name)
        return self.run_os_process(args, silent)


    def run_vstest(self, tests_path, silent = False):
        self.print_linebreaks(4, silent)
        self.print_if_loud("Running .Net Framework tests...", silent)
        args = ["dotnet"]
        args.append("vstest")
        args.append(tests_path)
        args.append("/Framework:.NETFramework,Version=v4.7.1")
        args.append("/InIsolation")
        args.append("/logger:trx")
        return self.run_os_process(args, silent)

        
    def locate_vs_installation(self, silent = False):
        program_files_location = os.environ.get("ProgramFiles")
        if program_files_location == None:
            self.print_if_loud("No ProgramFiles entry found in environment.", silent)
            return None
        vswhere_path = program_files_location + "/Microsoft Visual Studio/Installer/vswhere.exe"
        vswhere_with_args = [vswhere_path, '-latest', '-requires', 'Microsoft.Component.MSBuild']

        final_path = None

        args = [vswhere_path]
        args.append("-latest")
        args.append("-requires")
        args.append("Microsoft.Component.MSBuild")
        result = self.run_os_process(args, silent)

        for item in result.output.splitlines():
            split_item = item.split(":", 1)
            if (split_item[0] == "installationPath"):
                final_path = split_item[1].strip()
                self.print_if_loud("Found VS Installation at: " + final_path, silent)
                self.print_linebreaks(4, silent)

        return final_path


    def locate_msbuild(self, silent = False):
        vs_installation_path = self.locate_vs_installation()
        if (vs_installation_path == None):
            self.print_if_loud("VS Installation was not found.", silent)
            return None
        
        vs_msbuild_pattern = "MSBuild/*/Bin/MSBuild.exe"
        locations = glob.glob(os.path.join(vs_installation_path, vs_msbuild_pattern))
        for location in sorted(locations, reverse=True):
            if self.is_file(location):
                return location

        return None


    def run_os_process(self, args, silent = False):
        p = subprocess.Popen(args, stdout=subprocess.PIPE, shell=False)
        (output, err) = p.communicate()
        p_status = p.wait()
        output = str(output.decode("utf-8"))
        self.print_if_loud("Command output:\n" + output + "\n"
                            + "Command exit-status/return-code: " + str(p_status),
                            silent)
        result = SubprocessOutputStatus(output, p_status)
        return result


    def print_if_loud(self, msg, silent = False):
        if not silent:
            print(msg)


    def print_linebreaks(self, count, silent = False):
        i = 0
        linebreak_str = ""
        while (i < count):
            linebreak_str = linebreak_str + "\n"
            i += 1
        self.print_if_loud(linebreak_str, silent)
        

    def is_file(self, location):
        path = pathlib.Path(location)
        return path.is_file


SubprocessOutputStatus = collections.namedtuple("SubprocessOutputStatus", ["output", "status"])

if __name__ == "__main__":
    sln_builder = SlnBuilder()
    sln_builder.build(False)