using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartNodeButton : MenuButton
{
    protected override Action AddMethod()
    {
        return () => SetStartNode();
    }

    private void SetStartNode()
    {

    }
}
