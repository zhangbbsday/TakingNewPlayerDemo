using System.IO;
using UnityEngine;

namespace GameEditor
{
    public class BuildManager
    {
        public string FileSavePath { get; } = Application.dataPath + @"/save";

        public BuildManager()
        {
            CheckDirectory();
        }

        public FileInfo[] GetAllFileInfo()
        {
            DirectoryInfo folder = new DirectoryInfo(FileSavePath);
            return folder.GetFiles("*.xml");
        }

        public string[] GetAllFilePath()
        {
            return Directory.GetFiles(FileSavePath, ".xml");
        }

        public void LoadFile(string fileName)
        {
            string path = FileSavePath + "/" + GetXmlFileName(fileName);

            XmlDataContainer dataContainer = new XmlDataContainer();
            dataContainer.ReadXml(path);
            LoadAllData(dataContainer);
        }

        public void SaveFile(string fileName)
        {
            string path = FileSavePath + "/" + GetXmlFileName(fileName);

            XmlDataContainer dataContainer = new XmlDataContainer();
            SaveAllData(dataContainer);
            dataContainer.SaveXml(path);
        }

        private string GetXmlFileName(string fileName)
        {
            if (fileName.EndsWith(".xml"))
                return fileName;

            return fileName + ".xml";
        }

        private void LoadAllData(XmlDataContainer dataContainer)
        {
            GameManager.Instance.NodesManager.LoadXmlData(dataContainer);
            GameManager.Instance.LinksManager.LoadXmlData(dataContainer);
            GameManager.Instance.EnemyContainerManager.LoadXmlData(dataContainer);
            GameManager.Instance.ArrowsManager.LoadXmlData(dataContainer);
        }

        private void SaveAllData(XmlDataContainer dataContainer)
        {
            dataContainer.AddElement(GameManager.Instance.NodesManager.GetXmlData());
            dataContainer.AddElement(GameManager.Instance.LinksManager.GetXmlData());
            dataContainer.AddElement(GameManager.Instance.EnemyContainerManager.GetXmlData());
            dataContainer.AddElement(GameManager.Instance.ArrowsManager.GetXmlData());
        }

        private void CheckDirectory()
        {
            if (!Directory.Exists(FileSavePath))
                Directory.CreateDirectory(FileSavePath);
        }
    }
}