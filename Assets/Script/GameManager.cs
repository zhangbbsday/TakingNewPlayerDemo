using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
