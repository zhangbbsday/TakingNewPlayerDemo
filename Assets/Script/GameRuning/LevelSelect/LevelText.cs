using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] public Text Text;
    public string Name;

    public void Init(string name)
    {
        Name = name;
        Text.text = " "+name+" ";
    }
}
