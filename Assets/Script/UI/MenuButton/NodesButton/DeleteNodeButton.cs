using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNodeButton : MenuButton
{
    protected override Action AddMethod()
    {
        return () => DeleteNode();
    }

    private void DeleteNode()
    {

    }
}
