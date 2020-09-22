using System.Xml.Linq;

namespace GameEditor
{
    public interface IXmlDataSave
    {
        XElement GetXmlData();
        void LoadXmlData(XmlDataContainer dataContainer);
    }
}