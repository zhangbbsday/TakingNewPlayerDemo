using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyContainerManager
{
    public Transform EnemyContainersParent { get; private set; }
    public int EnemyContainerNumber { get => EnemyContainers.Count; }
    private string EnemyContainersName { get; } = "EnemyContainer";
    private Dictionary<int, EnemyContainer> EnemyContainers { get; } = new Dictionary<int, EnemyContainer>();
    private int GlobalIndex { get => globalIndex++; }
    private int globalIndex;

    public EnemyContainerManager()
    {
        FindParent();
    }

    public EnemyContainer CreateEnemyContainer()
    {
        EnemyContainer container = GameManager.Instance.ResourcesManager.GetEnemyContainer(EnemyContainersName);
        container = GameObject.Instantiate(container, Vector2.zero, Quaternion.identity, EnemyContainersParent);
        container.Init(GlobalIndex);

        SetContainer(container);
        return container;
    }

    public void DeleteEnemyContainer(EnemyContainer container)
    {
        ClearContainer(container);
        GameObject.Destroy(container.GameObject);
    }

    public EnemyContainer[] GetEnemyContainers()
    {
        return EnemyContainers.Values.ToArray();
    }

    private void SetContainer(EnemyContainer container)
    {
        if (!EnemyContainers.ContainsKey(container.Id))
            EnemyContainers.Add(container.Id, container);
    }

    private void ClearContainer(EnemyContainer container)
    {
        if (!EnemyContainers.ContainsKey(container.Id))
            return;

        EnemyContainers.Remove(container.Id);
    }

    private void FindParent()
    {
        try
        {
            EnemyContainersParent = GameObject.Find("Canvas").transform.Find("Menus").
                Find("EnemiesMenu").Find("Tool").Find("EnemiesView").Find("Viewport").Find("Content");
        }
        catch
        {
            Debug.Log("请检查是否正确设置了EnemiesView.");
        }
    }
}
