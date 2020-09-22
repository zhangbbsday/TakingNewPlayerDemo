using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public abstract class UIElementBase : GameActor
    {
        public Vector2 Position { get; set; }
        public RectTransform RectTransform { get; private set; }
        protected bool CanChangePosition { get; set; }

        protected override void Start()
        {
            base.Start();
            Pretreatment();
        }

        protected override void Update()
        {
            base.Update();
            UpdateBase();
        }

        private void Pretreatment()
        {
            RectTransform = GetComponent<RectTransform>();
            Position = RectTransform.localPosition;
            CanChangePosition = true;
        }

        private void UpdateBase()
        {
            if (CanChangePosition)
                RectTransform.localPosition = Position;
        }
    }
}