using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class Node : GameActor, ISelectableActor
    {
        public enum NodeType
        {
            NormalNode,
            StartNode,
            EndNode,
        }

        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public NodeType Type { get; set; }
        private float SelectedSize { get; } = 0.2f;

        public void Init(int id, Vector2 pos, NodeType type)
        {
            Id = id;
            Position = pos;
            Type = type;
        }

        protected override void Start()
        {
            base.Start();
            Position = Transform.position;
        }

        protected override void Update()
        {
            base.Update();

            Transform.position = Position;
        }

        public void SelectEffect()
        {
            Transform.localScale += new Vector3(1, 1, 0) * SelectedSize;
        }

        public void ReleaseEffect()
        {
            Transform.localScale = Vector3.one;
        }
    }
}
