# FoxyCart XML Data Feed

For more information about the XML data feed see the official [documentation](https://wiki.foxycart.com/v/2.0/transaction_xml_datafeed).
FoxyCart does not provide a XSD file, so we need to generate our own from the sample XML.


Follow these steps to generate XML serializable classes from the sample XML (Windows only):

* go to http://www.freeformatter.com/xsd-generator.html
* upload FoxyData.xml
* select "Salami Slice"
* generate xsd
* copy generated xsd to FoxyData.xsd and save file
* open Developer Command Prompt for VS2015
* navigate to this folder
* run xsd FoxyData.xsd /c /n:FoxySharp.DataFeed
* remove the System.Serializable-Attribute from the generated classes as it's not supported by .NET Core
