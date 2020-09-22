using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class DeleteArrowButton : MenuButton
    {
        private bool IsSelecting { get; set; }
        private float SelectRange { get; } = 0.5f;
        private Arrow SelectedOne { get; set; }

        protected override void Update()
        {
            base.Update();
            DeleteArrow();
        }

        private void DeleteArrow()
        {
            if (!IsSelecting)
                return;

            Arrow arrow = FindNearestArrow();
            SelectedEffect(arrow);
            if (arrow == null)
                return;

            if (Input.GetMouseButtonDown(0) && !MouseUtils.IsMouseOverUIObject())
                DeleteOne(arrow);
        }

        private Arrow FindNearestArrow()
        {
            return GameManager.Instance.ArrowsManager.GetMouseNearestArrow(SelectRange);
        }

        private void DeleteOne(Arrow arrow)
        {
            GameManager.Instance.ArrowsManager.DeleteArrow(arrow);
            //IsSelecting = false;
        }

        private void SelectedEffect(Arrow arrow)
        {
            if (SelectedOne == arrow)
                return;

            if (SelectedOne != null)
                SelectedOne.ReleaseEffect();

            SelectedOne = arrow;
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
