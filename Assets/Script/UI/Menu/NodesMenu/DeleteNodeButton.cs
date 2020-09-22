using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class DeleteNodeButton : MenuButton
    {
        private bool IsSelecting { get; set; }
        private float SelectRange { get; } = 0.5f;
        private Node SelectedOne { get; set; }

        protected override void Update()
        {
            base.Update();
            DeleteNode();
        }

        private void DeleteNode()
        {
            if (!IsSelecting)
                return;

            Node node = FindNearestNode();
            SelectedEffect(node);
            if (node == null)
                return;

            if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
                DeleteOne(node);
        }

        private Node FindNearestNode()
        {
            return GameManager.Instance.NodesManager.GetMouseNearestNode(SelectRange);
        }

        private void DeleteOne(Node node)
        {
            GameManager.Instance.NodesManager.DeleteNode(node);
            //IsSelecting = false;
        }

        private void SelectedEffect(Node node)
        {
            if (SelectedOne == node)
                return;

            if (SelectedOne != null)
                SelectedOne.ReleaseEffect();

            SelectedOne = node;
            if (SelectedOne != null)
                SelectedOne.SelectEffect();
        }

        public override void PressAction()
        {
            IsSelecting = true;
            SelectedOne = null;
        }

        public override void ReleseAction()
        {
            IsSelecting = false;
            SelectedOne = null;
        }
    }
}