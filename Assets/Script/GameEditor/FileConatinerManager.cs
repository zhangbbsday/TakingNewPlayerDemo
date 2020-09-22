using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEditor
{
    public class FileContainerManager
    {
        public Transform FileContainersParent { get; private set; }
        public int FileContainerNumber { get => FileContainers.Count; }
        private string FileContainersName { get; } = "FileContainer";
        private Dictionary<int, FileContainer> FileContainers { get; } = new Dictionary<int, FileContainer>();
        private int GlobalIndex { get => globalIndex++; }
        private int globalIndex;

        public FileContainerManager()
        {
            FindParent();
        }

        public FileContainer CreateFileContainer(string fileName, string fileUpdateDate)
        {
            FileContainer container = GameManager.Instance.ResourcesManager.GetFileContainer(FileContainersName);
            container = GameObject.Instantiate(container, Vector2.zero, Quaternion.identity, FileContainersParent);
            container.Init(GlobalIndex, fileName, fileUpdateDate);

            SetContainer(container);
            return container;
        }

        public FileContainer[] CreateAllFileContainers()
        {
            var filesInfo = GameManager.Instance.BuildManager.GetAllFileInfo();
            List<FileContainer> containers = new List<FileContainer>();
            foreach (var file in filesInfo)
            {
                var container = CreateFileContainer(file.Name, file.LastWriteTime.ToString());
                containers.Add(container);
            }

            return containers.ToArray();
        }

        public void DeleteFileContainer(FileContainer container)
        {
            ClearContainer(container);
            GameObject.Destroy(container.GameObject);
        }

        public void DeleteAllFileConatiner()
        {
            var containers = GetContainers();

            for (int i = 0; i < containers.Length; i++)
            {
                if (containers[i] == null)
                    continue;

                DeleteFileContainer(containers[i]);
            }
        }

        public FileContainer[] GetContainers()
        {
            return FileContainers.Values.ToArray();
        }

        private void ClearContainer(FileContainer container)
        {
            if (FileContainers.ContainsKey(container.Id))
                FileContainers.Remove(container.Id);
        }

        private void SetContainer(FileContainer container)
        {
            if (!FileContainers.ContainsKey(container.Id))
                FileContainers.Add(container.Id, container);
        }

        private void FindParent()
        {
            try
            {
                FileContainersParent = GameObject.Find("Canvas").transform.Find("Menus").
                    Find("BuildMenu").Find("FilesList").Find("FilesView").Find("Viewport").Find("Content");
            }
            catch
            {
                Debug.Log("请检查是否正确设置了FilesView.");
            }
        }
    }
}