using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEndNodeButton : MenuButton
{
    protected override Action AddMethod()
    {
        return () => SetEndNode();
    }

    private void SetEndNode()
    {

    }
}
