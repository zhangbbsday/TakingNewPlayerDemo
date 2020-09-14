using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActor : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public GameObject GameObject { get; private set; }

    protected virtual void Awake()
    {
        Initialization();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        
    }

    private void Initialization()
    {
        Transform = transform;
        GameObject = gameObject;
    }
}
