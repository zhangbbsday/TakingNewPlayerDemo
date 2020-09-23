using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GameEditor
{
    public class ArrowsManager : IXmlDataSave
    {
        public Transform ArrowsParent { get; private set; }
        private int[] ArrowsCount { get; set; } = new int[2];
        private string ArrowsParentName { get; } = "Arrows";
        private Dictionary<int, Arrow> Arrows { get; } = new Dictionary<int, Arrow>();
        private string[] ArrowsName { get; } =
        {
        "AttackArrow",
        "ReturnArrow",
    };

        private int GlobalIndex { get => globalIndex++; }
        private int globalIndex;

        public ArrowsManager()
        {
            FindParent();
        }

        public Arrow CreateArrow(Arrow.ArrowType type, Vector2 position)
        {
            return CreateArrow(type, position, 0);
        }

        public Arrow CreateArrow(Arrow.ArrowType type, Vector2 position, Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            return CreateArrow(type, position, angle);
        }

        public Arrow CreateArrow(Arrow.ArrowType type, Vector2 position, float angle)
        {
            Arrow arrow = GameManager.Instance.ResourcesManager.GetArrow(ArrowsName[(int)type]);
            arrow = GameObject.Instantiate(arrow, position, Quaternion.identity, ArrowsParent);
            arrow.Init(GlobalIndex, position, angle, type);

            SetArrow(arrow);
            return arrow;
        }

        public void DeleteArrow(Arrow arrow)
        {
            ClearArrow(arrow);
            GameObject.Destroy(arrow.GameObject);
        }

        public void DeleteAllArrows()
        {
            var objs = GetArrows();

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] == null)
                    continue;

                DeleteArrow(objs[i]);
            }
        }

        public Arrow[] GetArrows()
        {
            return Arrows.Values.ToArray();
        }

        public Arrow GetMouseNearestArrow(float selectRange = 0.5f)
        {
            Arrow[] arrows = GetArrows();
            if (arrows == null || arrows.Length == 0)
                return null;

            KeyValuePair<Arrow, float> nearest = new KeyValuePair<Arrow, float>(arrows[0], float.MaxValue);
            foreach (var a in arrows)
            {
                float distance = Vector2.Distance(a.Position, MouseUtils.MouseWorldPosition);
                if (distance < nearest.Value)
                    nearest = new KeyValuePair<Arrow, float>(a, distance);
            }

            if (nearest.Value < selectRange)
                return nearest.Key;
            return null;
        }

        public void ChangeArrowCount(Arrow.ArrowType type, int value)
        {
            ArrowsCount[(int)type] = value;
        }

        private void SetArrow(Arrow arrow)
        {
            if (!Arrows.ContainsKey(arrow.Id))
                Arrows.Add(arrow.Id, arrow);
        }

        private void ClearArrow(Arrow arrow)
        {
            if (Arrows.ContainsKey(arrow.Id))
                Arrows.Remove(arrow.Id);
        }

        private void FindParent()
        {
            GameObject obj = GameObject.Find(ArrowsParentName);
            if (obj == null)
                obj = new GameObject(ArrowsParentName);

            ArrowsParent = obj.transform;
            ArrowsParent.transform.position = Vector2.zero;
        }

        public XElement GetXmlData()
        {
            XElement root = new XElement("arrows");

            SetAllArrow(root);
            SetAllArrowCount(root);
            return root;
        }

        private void SetAllArrow(XElement root)
        {
            XElement element = new XElement("allarrows");

            foreach (var arrow in Arrows)
            {
                element.Add(new XElement("arrow",
                                new XElement("type", (int)arrow.Value.Type),
                                new XElement("x", arrow.Value.Position.x),
                                new XElement("y", arrow.Value.Position.y),
                                new XElement("direction", arrow.Value.Angle)));
            }

            root.Add(element);
        }

        private void SetAllArrowCount(XElement root)
        {
            XElement element = new XElement("userarrowcount");
            element.Add(new XElement("attackarrow", ArrowsCount[(int)Arrow.ArrowType.AttackArrow]));
            element.Add(new XElement("returnarrow", ArrowsCount[(int)Arrow.ArrowType.ReturnArrow]));

            root.Add(element);
        }

        public void LoadXmlData(XmlDataContainer dataContainer)
        {
            SetDefaultState();

                LoadAllArrows(dataContainer);
                LoadArrowCount(dataContainer);
        }

        private void SetDefaultState()
        {
            DeleteAllArrows();
            globalIndex = 0;
        }

        private void LoadAllArrows(XmlDataContainer dataContainer)
        {
            var arrows = from arrow in dataContainer.Document.Root.Element("arrows").Element("allarrows").Elements()
                         select new
                         {
                             Type = arrow.Element("type").Value,
                             X = arrow.Element("x").Value,
                             Y = arrow.Element("y").Value,
                             Direction = arrow.Element("direction").Value,
                         };

            foreach (var a in arrows)
            {
                CreateArrow((Arrow.ArrowType)(int.Parse(a.Type)), 
                    new Vector2(float.Parse(a.X), float.Parse(a.Y)),float.Parse(a.Direction));
            }
        }

        private void LoadArrowCount(XmlDataContainer dataContainer)
        {
            var root = dataContainer.Document.Root.Element("arrows").Element("userarrowcount");
            ArrowsCount[(int)Arrow.ArrowType.AttackArrow] = int.Parse(root.Element("attackarrow").Value);
            ArrowsCount[(int)Arrow.ArrowType.ReturnArrow] = int.Parse(root.Element("returnarrow").Value);

            StartArrowsNumber.Instance.SetValues(ArrowsCount);
        }
    }
}