import argparse
import subprocess
import collections
from fodycleaner import FodyCleaner


class SlnBuilder:


    def __init__(self):
        print("SlnBuilder\n")
        self.parser = argparse.ArgumentParser(description='Build VS solution along with project tests.')
        self.parser.add_argument("--config", "-c", choices=["Release", "Debug"], type=str, metavar="ConfigurationName", default="Debug", help="Specify the ConfigurationName to be used with MSBuild.")
        self.parser.add_argument("--tests", "-t",  action="store_true", default=False, help="Specify if tests should be run. Use it as a flag, i.e. simply add -t or --tests without additional args.")
        self.solution_path = "./JsonButler/JsonButler.sln"
        self.tests_path = "./JsonButler/JsonButler.Tests/bin/Debug/Andeart.JsonButler.Tests.dll"


    def build(self, silent = False):
        if not silent:
            print ("Running build...")
        args = self.parser.parse_args()
        config_name = args.config
        should_run_tests = args.tests
        if not silent:
            print("Configuration: " + config_name)
            print("Should run tests?: " + str(should_run_tests))
            print("")

        result = self.run_nuget_restore(silent)        
        if (result.status != 0):
            self.exit_with_error(result.status, silent)

        exit_code = self.run_fody_cleaner()
        if (exit_code != 0):
            self.exit_with_error(exit_code, silent)

        result = self.run_msbuild(config_name, silent)
        if (result.status != 0):
            self.exit_with_error(result.status, silent)

        if not silent:
            print("Build was successful.")

        if config_name == "Debug" and should_run_tests:
            result = self.run_vstest(silent)
            if (result.status != 0):
                self.exit_with_error(result.status, silent)
            if not silent:
                print("Tests run successfully.")
        else:
            print("No tests were run")


    def exit_with_error(self, error_int, silent = False):
        if (error_int != 0):
            if not silent:
                print("Exiting with error-code: " + str(error_int))
            exit(error_int)
        

    def run_fody_cleaner(self, silent = False):
        if not silent:
            print("Cleaning Fody references before build...")
        fody_cleaner = FodyCleaner()
        return fody_cleaner.clear_fody_refs()


    def run_nuget_restore(self, silent = False):
        if not silent:
            print("Running nuget restore...")
        args = ["nuget"]
        args.append("restore")
        args.append(self.solution_path)
        return self.run_os_process(args, silent)


    def run_msbuild(self, config_name, silent = False):
        if not silent:
            print("Building solution...")
        args = ["dotnet"]
        args.append("msbuild")
        args.append(self.solution_path)
        args.append("-p:Configuration=" + config_name)
        return self.run_os_process(args, silent)


    def run_vstest(self, silent=False):
        if not silent:
            print("Running .Net Framwork tests...")
        args = ["dotnet"]
        args.append("vstest")
        args.append(self.tests_path)
        args.append("/Framework:.NETFramework,Version=v4.7.1")
        args.append("/InIsolation")
        args.append("/logger:trx")
        return self.run_os_process(args, silent)


    def run_os_process(self, args, silent = False):
        p = subprocess.Popen(args, stdout=subprocess.PIPE, shell=True)
        (output, err) = p.communicate()
        p_status = p.wait()
        output = str(output.decode("utf-8"))
        if not silent:
            print("Command output:\n" + output)
            print("Command exit-status/return-code: " + str(p_status))
            print("")
        result = SubprocessOutputStatus(output, p_status)
        return result


SubprocessOutputStatus = collections.namedtuple("SubprocessOutputStatus", ["output", "status"])


if __name__ == "__main__":
    sln_builder = SlnBuilder()
    sln_builder.build(False)