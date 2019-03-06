import glob
import xml.etree.ElementTree as ET

class FodyCleaner:
    def clear_fody_refs(self):
        packages_path_format = '*/*/packages.config'
        file_paths = glob.glob(packages_path_format)
        packages = ['.//package[@id="Costura.Fody"]', './/package[@id="Fody"]']
        for file_path in file_paths:
            file = ET.parse(file_path)
            root = file.getroot()
            found_fody_ref = False
            for package in packages:
                elem = root.find(package)
                if elem is not None:
                    root.remove(elem)
                    found_fody_ref = True
            if (found_fody_ref == True):
                file.write(file_path, encoding="utf-8", xml_declaration=True)
                print("Cleared Fody refs in " + file_path)
            return 0


if __name__ == "__main__":
    cleaner = FodyCleaner()
    cleaner.clear_fody_refs()