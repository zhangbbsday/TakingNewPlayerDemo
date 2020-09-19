using System.IO;
using UnityEngine;

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

    }

    public void SaveFile(string fileName)
    {

    }

    private void CheckDirectory()
    {
        if (!Directory.Exists(FileSavePath))
            Directory.CreateDirectory(FileSavePath);
    }
}