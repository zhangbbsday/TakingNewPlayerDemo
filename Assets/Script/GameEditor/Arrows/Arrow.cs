using UnityEngine;

namespace GameEditor
{
    public class Arrow : GameActor, ISelectableActor
    {
        public enum ArrowType
        {
            AttackArrow,
            ReturnArrow,
        }

        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public float Angle { get; private set; }
        public ArrowType Type { get; set; }
        private float SelectedSize { get; } = 0.2f;
        private Vector2 StartScale { get; set; }

        public void Init(int id, Vector2 pos, float angle, ArrowType type)
        {
            Id = id;
            Position = pos;
            Angle = angle;
            Type = type;
        }

        protected override void Start()
        {
            base.Start();
            Position = Transform.position;
            StartScale = Transform.localScale;
            SetDirection(Angle);
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
            Transform.localScale = StartScale;
        }

        public void SetDirection(float angle)
        {
            Angle = angle;
            Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, Transform.eulerAngles.y, Angle);
        }

        public void SetDirection(Vector2 direction)
        {
            Angle = Vector2.SignedAngle(Vector2.up, direction);
            Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, Transform.eulerAngles.y, Angle);
        }
    }
}