import glob
import xml.etree.ElementTree as ET

from shyprint import Logger, LogLevel


class FodyCleaner:


    def __init__(self, silent = False):        
        self.logger = Logger(self)
        self.logger.silent = silent
        self.logger.log("FodyCleaner initialised.")

    
    def clear_refs(self):
        packages_path_format = '*/*/packages.config'
        file_paths = glob.glob(packages_path_format)
        package_keys = ['.//package[@id="Costura.Fody"]', './/package[@id="Fody"]']
        for file_path in file_paths:            
            self.logger.log(f"Searching for Fody references in {file_path}.")
            file = ET.parse(file_path)
            root = file.getroot()
            was_ref_found = False
            for package in package_keys:
                elem = root.find(package)
                if elem is not None:
                    root.remove(elem)
                    was_ref_found = True
            if (was_ref_found == True):
                file.write(file_path, encoding="utf-8", xml_declaration=True)
                self.logger.log(f"Cleared Fody refs in {file_path}.", LogLevel.SUCCESS)
        return 0



if __name__ == "__main__":
    cleaner = FodyCleaner()
    cleaner.clear_fody_refs()
