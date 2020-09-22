using System;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace GameEditor
{
    public class XmlDataContainer
    {
        public XDocument Document { get; private set; } = new XDocument();

        public XmlDataContainer()
        {
            Document.Add(new XElement("game"));
        }

        public void AddElement(XElement element)
        {
            Document.Root.Add(element);
        }

        public void ReadXml(string path)
        {
            Document = XDocument.Load(path);
        }

        public void SaveXml(string path)
        {
            Document.Save(path);
        }
    }
}