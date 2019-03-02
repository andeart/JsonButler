import xml.etree.ElementTree as ET

file_path = 'JsonButler/packages.config'
file = ET.parse(file_path)
root = file.getroot()

elem = root.find('.//package[@id="Costura.Fody"]')
if elem is not None:
    root.remove(elem)

elem = root.find('.//package[@id="Fody"]')
if elem is not None:
    root.remove(elem)

file.write(file_path, encoding="utf-8", xml_declaration=True)