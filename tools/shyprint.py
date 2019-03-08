from colorama import init, Fore, Back, Style
from enum import Enum



class LogLevel(Enum):
    INFO = 1
    WARNING = 2
    ERROR = 3
    SUCCESS = 4



class Logger:

    style_map = {LogLevel.INFO: Fore.WHITE,
                LogLevel.WARNING: Fore.LIGHTYELLOW_EX,
                LogLevel.ERROR: Fore.LIGHTRED_EX,
                LogLevel.SUCCESS: Fore.LIGHTGREEN_EX}

    def __init__(self, owner = None):
        init()
        self.silent = False
        self.log(f"Logger initialised. Owner: {str(owner)}")


    def log(self, msg, log_level = LogLevel.INFO):
        if not self.silent:
            print(self.style_map[log_level] + msg + Style.RESET_ALL)

    
    def log(self, msg, log_level = LogLevel.INFO, overridden_silence = False):
        if not overridden_silence:
            print(self.style_map[log_level] + msg + Style.RESET_ALL)


    def log_linebreaks(self, count = 1):
        if self.silent: return None
        i = 0
        while (i < count):
            print()
            i += 1

    
    def debug_log_all_styles(self):
        print("Printing default color styles.\n")
        print(Fore.BLACK + "BLACK")
        print(Fore.BLUE + "BLUE")
        print(Fore.CYAN + "CYAN")
        print(Fore.GREEN + "GREEN")
        print(Fore.LIGHTBLACK_EX + "LIGHTBLACK_EX")
        print(Fore.LIGHTBLUE_EX + "LIGHTBLUE_EX")
        print(Fore.LIGHTCYAN_EX + "LIGHTCYAN_EX")
        print(Fore.LIGHTGREEN_EX + "LIGHTGREEN_EX")
        print(Fore.LIGHTMAGENTA_EX + "LIGHTMAGENTA_EX")
        print(Fore.LIGHTRED_EX + "LIGHTRED_EX")
        print(Fore.LIGHTWHITE_EX + "LIGHTWHITE_EX")
        print(Fore.LIGHTYELLOW_EX + "LIGHTYELLOW_EX")
        print(Fore.MAGENTA + "MAGENTA")
        print(Fore.RED + "RED")
        print(Fore.RESET + "RESET")
        print(Fore.WHITE + "WHITE")
        print(Fore.YELLOW + Back.BLACK + "YELLOW")

        
if __name__ == "__main__":
    logger = Logger()
    logger.debug_log_all_styles()