using System.Xml.Linq;

public interface IXmlDataSave
{
    XElement GetXmlData();
    void LoadXmlData(XmlDataContainer dataContainer);
}
