using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script
{
    public class Node : MonoBehaviour
    {
        private GameObject _beginPic;
        private GameObject _endPic;
        private GameObject _normalPic;
        public List<KeyValuePair<float,Link>> Links { get; set; } = new List<KeyValuePair<float, Link>>();

        public GameObject BeginPic
        {
            get
            {
                if (_beginPic == null) Init();
                return _beginPic;
            }
        }
        public GameObject EndPic
        {
            get
            {
                if (_endPic == null) Init();
                return _endPic;
            }
        }
        public GameObject NormalPic
        {
            get
            {
                if (_normalPic == null) Init();
                return _normalPic;
            }
        }

        private void Init()
        {
            _beginPic = transform.Find("RelocateBegin").gameObject;
            _endPic = transform.Find("RelocateEnd").gameObject;
            _normalPic = transform.Find("RelocateNormal").gameObject;
        }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void BecomeBegin()
        {
            GameManager.BeginNode = this;
            BeginPic.SetActive(true);
            EndPic.SetActive(false);
            NormalPic.SetActive(false);
        }
        public void BecomeEnd()
        {
            GameManager.EndNode = this;
            BeginPic.SetActive(false);
            EndPic.SetActive(true);
            NormalPic.SetActive(false);
        }
        public void BecomeNormal()
        {
            if (GameManager.BeginNode == this) GameManager.BeginNode = null;
            if (GameManager.EndNode == this) GameManager.EndNode = null;
            BeginPic.SetActive(false);
            EndPic.SetActive(false);
            NormalPic.SetActive(true);
        }
        public void RegisterLink(Link l)
        {
            Node centerNode = this;
            Node anotherNode;
            anotherNode = l.EndPoint1 == centerNode ? l.EndPoint2 : l.EndPoint1;
            Vector2 delta = anotherNode.Position - centerNode.Position;
            float angle = Mathf.Atan2(delta.y, delta.x);
            Links.Add(new KeyValuePair<float, Link>(angle,l));
            Links.Sort((p1, p2) =>
            {
                if (p1.Key < p2.Key) return -1;
                if (p1.Key == p2.Key) return 0;
                else return 1;
            });
        }

        public Link RightSideOf(Link l)
        {
            if (Links.Count <= 1)
            {
                Debug.LogError("别你妈搜了，没路了");
                return null;
            }
            var ordered = new List<Link>();
            foreach (var pair in Links)
            {
                ordered.Add(pair.Value);
            }
            int lPosition = ordered.IndexOf(l);
            if (lPosition == Links.Count-1) return ordered[0];//逆时针1234
            else return ordered[lPosition + 1];
        }
        public Link LeftSideOf(Link l)
        {
            if (Links.Count <= 1)
            {
                Debug.LogError("别你妈搜了，没路了");
                return null;
            }
            var ordered = new List<Link>();
            foreach (var pair in Links)
            {
                ordered.Add(pair.Value);
            }
            int lPosition = ordered.IndexOf(l);
            if (lPosition == 0) return ordered.Last();
            else return ordered[lPosition - 1];
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Im Node");
                foreach (var l in Links)
                {
                    Debug.Log("Link between"+l.Value.EndPoint1.Position+"and"+l.Value.EndPoint2.Position+".angle is "+l.Key);
                }
            }
        }
    }
}
