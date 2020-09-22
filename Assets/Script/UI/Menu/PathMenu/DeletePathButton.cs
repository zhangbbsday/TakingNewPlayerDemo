using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class DeletePathButton : MenuButton
    {
        private bool IsSelecting { get; set; }
        private float SelectRange { get; } = 0.3f;
        private Link SelectedOne { get; set; }


        protected override void Update()
        {
            base.Update();
            DeleteLink();
        }

        private void DeleteLink()
        {
            if (!IsSelecting)
                return;

            Link link = FindNearestLik();
            SelectEffect(link);
            if (link == null)
                return;

            if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
                DeleteOne(link);
        }


        private Link FindNearestLik()
        {
            return GameManager.Instance.LinksManager.GetMouseNearestLink(SelectRange);
        }

        private void SelectEffect(Link link)
        {
            if (SelectedOne == link)
                return;

            if (SelectedOne != null)
                SelectedOne.ReleaseEffect();

            SelectedOne = link;
            if (SelectedOne != null)
                SelectedOne.SelectEffect();
        }

        private void DeleteOne(Link link)
        {
            GameManager.Instance.LinksManager.DeleteLink(link);
            //IsSelecting = false;
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