import argparse
import pathlib

from processrun import ProcessRunner
from shyprint import Logger, LogLevel


class NetTester:

    def __init__(self):
        print("NetTester\n")
        self.parser = argparse.ArgumentParser(description='Run tests for VS solution.')
        self.parser.add_argument("--testspath", "-t", type=str, metavar="TestsPath", default=None, help="The path to the tests assembly.")


    def run_tests(self, silent = False):        
        # Initialize shyprint
        self.logger = Logger(self)
        self.logger.silent = silent        

        # Initialize processrun
        self.process_run = ProcessRunner(silent)

        # Parse CLI args
        args = self.parser.parse_args()
        tests_path = args.testspath

        self.logger.log(f"Tests path: {tests_path}", LogLevel.WARNING)
        
        if (tests_path == None): self.exit_with_error(1, "Tests path not provided for running tests.")
        
        if not self.is_file(tests_path): self.exit_with_error(1, "Tests assembly is not a valid file.")
        
        
        result = self.run_vstest(tests_path)
        if (result.status != 0):
            self.exit_with_error(result.status, "Tests were not run successfully.")

        self.logger.log("Tests were run successfully.", LogLevel.SUCCESS)


    def run_vstest(self, tests_path):
        self.logger.log_linebreaks(2)
        self.logger.log("Running .Net Framework tests...", LogLevel.WARNING)
        cmd_line = f"dotnet vstest {tests_path} /Framework:.NETFramework,Version=v4.7.1 /InIsolation /logger:trx"
        return self.process_run.run_line(cmd_line)

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
    tester = NetTester()
    tester.run_tests(False)
