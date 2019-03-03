import glob
import xml.etree.ElementTree as ET

packages_path_format = '*/*/packages.config'
file_paths = glob.glob(packages_path_format)
packages = ['.//package[@id="Costura.Fody"]', './/package[@id="Fody"]']
for file_path in file_paths:
    file = ET.parse(file_path)
    root = file.getroot()
    for package in packages:
        elem = root.find(package)
        if elem is not None:
            root.remove(elem)
    file.write(file_path, encoding="utf-8", xml_declaration=True)