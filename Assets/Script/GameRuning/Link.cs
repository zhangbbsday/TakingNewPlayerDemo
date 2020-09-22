using UnityEngine;

namespace Assets.Script
{
    public class Link : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Node _endPoint1;
        private Node _endPoint2;
        public LineRenderer LineRenderer
        {
            get
            {
                if(_lineRenderer==null) _lineRenderer = GetComponent<LineRenderer>();
                return _lineRenderer;
            }
        }
        public Node EndPoint1
        {
            get=>_endPoint1;
            private set
            {
                _endPoint1 = value;
                LineRenderer.SetPosition(0, _endPoint1.Position);
            }
        }
        public Node EndPoint2
        {
            get=>_endPoint2;
            private set
            {
                _endPoint2 = value;
                LineRenderer.SetPosition(1, _endPoint2.Position);
            }
        }

        public Node GetNodeBeside(Node n)
        {
            if (_endPoint1 == n) return _endPoint2;
            return _endPoint1;
        }
        public float Distance { get=>Vector2.Distance(EndPoint1.Position,EndPoint2.Position); }

        public void Init(Node end1, Node end2)
        {
            EndPoint1 = end1;
            EndPoint2 = end2;
            EndPoint1.RegisterLink(this);
            EndPoint2.RegisterLink(this);
        }
    }
}
