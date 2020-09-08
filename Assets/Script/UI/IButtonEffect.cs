using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonEffect
{
    void EnterEffect();
    void ExitEffect();
    void PressEffect();
    void ReleseEffect();
    void SelectedEffect();
}
