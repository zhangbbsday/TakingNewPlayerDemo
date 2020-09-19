﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instacne == null)
            {
                GameObject obj = new GameObject("GameManager");
                instacne = obj.AddComponent<GameManager>();
            }
             
            return instacne;
        }
    }
    private static GameManager instacne;

    public ResourcesManager ResourcesManager { get; private set; }
    public NodesManager NodesManager { get; private set; }
    public LinksManager LinksManager { get; private set; }
    public EnemyContainerManager EnemyContainerManager { get; private set; }
    public ArrowsManager ArrowsManager { get; private set; }
    public FileContainerManager FileContainerManager { get; private set; }
    public BuildManager BuildManager { get; private set; }

    private void Awake()
    {
        SetManagers();

        instacne = this;
    }

    private void Start()
    {

    }

    private void SetManagers()
    {
        ResourcesManager = new ResourcesManager();
        NodesManager = new NodesManager();
        LinksManager = new LinksManager();
        EnemyContainerManager = new EnemyContainerManager();
        ArrowsManager = new ArrowsManager();
        FileContainerManager = new FileContainerManager();
        BuildManager = new BuildManager();
    }
}
