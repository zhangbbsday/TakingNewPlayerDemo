using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GameEditor
{
    public class LinksManager : IXmlDataSave
    {
        public Transform LinksParent { get; private set; }
        private string LinksParentName { get; } = "Links";
        private Dictionary<int, Link> Links { get; } = new Dictionary<int, Link>();
        private string LinksName { get; } = "Link";
        private int GlobalIndex { get => globalIndex++; }
        private int globalIndex;
        public LinksManager()
        {
            FindParent();
            globalIndex = 0;

        }

        public Link CreateLink(Node left, Node right)
        {
            Link link = GameManager.Instance.ResourcesManager.GetLink(LinksName);
            link = GameObject.Instantiate(link, Vector2.zero, Quaternion.identity, LinksParent);
            link.Init(GlobalIndex, left, right);

            SetLink(link);
            return link;
        }

        public void DeleteLink(Link link)
        {
            ClearLink(link);
            GameObject.Destroy(link.GameObject);
        }

        public void DeleteLink(Node node)
        {
            var links = Links.Values.ToArray();
            for (int i = 0; i < links.Length; i++)
            {
                if (links[i] == null)
                    continue;

                var nodes = links[i].GetNodes();
                if (nodes.Key == node || nodes.Value == node)
                    DeleteLink(links[i]);
            }
        }

        public void DeleteAllLinks()
        {
            var objs = GetLinks();

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] == null)
                    continue;

                DeleteLink(objs[i]);
            }
        }

        public Link[] GetLinks()
        {
            return Links.Values.ToArray();
        }

        public Link GetMouseNearestLink(float selectRange = 0.5f)
        {
            Link[] links = GetLinks();
            if (links == null || links.Length == 0)
                return null;

            KeyValuePair<Link, float> nearest = new KeyValuePair<Link, float>(links[0], float.MaxValue);
            foreach (var n in links)
            {
                var nodes = n.GetNodes();
                float distance = VectorUtils.DistanceFromPoint2Line(MouseUtils.MouseWorldPosition,
                                                        nodes.Key.Position, nodes.Value.Position);
                if (distance < nearest.Value)
                    nearest = new KeyValuePair<Link, float>(n, distance);
            }

            if (nearest.Value < selectRange)
                return nearest.Key;
            return null;
        }

        private void SetLink(Link link)
        {
            foreach (var l in Links.Values)
            {
                if (l.IsEqual(link))
                {
                    DeleteLink(link);
                    return;
                }
            }

            Links.Add(link.Id, link);
        }

        private void ClearLink(Link link)
        {
            if (Links.ContainsKey(link.Id))
                Links.Remove(link.Id);
        }

        private void FindParent()
        {
            GameObject obj = GameObject.Find(LinksParentName);
            if (obj == null)
                obj = new GameObject(LinksParentName);

            LinksParent = obj.transform;
            LinksParent.transform.position = Vector2.zero;
        }

        public XElement GetXmlData()
        {
            XElement root = new XElement("links");
            SetAllLinks(root);

            return root;
        }

        private void SetAllLinks(XElement root)
        {
            foreach (var link in Links)
            {
                var nodes = link.Value.GetNodes();
                root.Add(new XElement("link",
                    new XElement("start", nodes.Key.Id),
                    new XElement("end", nodes.Value.Id)));
            }
        }

        public void LoadXmlData(XmlDataContainer dataContainer)
        {
            SetDefaultState();
            try
            {
                LoadAllLinks(dataContainer);
            }
            catch
            {
                Debug.LogError("读取的XML文件残损或格式有误!");
            }
        }

        private void SetDefaultState()
        {
            DeleteAllLinks();
            globalIndex = 0;
        }

        private void LoadAllLinks(XmlDataContainer dataContainer)
        {
            var links = from link in dataContainer.Document.Root.Element("links").Elements()
                        select new
                        {
                            StartNodeId = link.Element("start").Value,
                            EndNodeId = link.Element("end").Value,
                        };

            foreach (var l in links)
            {
                Node left = GameManager.Instance.NodesManager.GetNode(int.Parse(l.StartNodeId));
                Node right = GameManager.Instance.NodesManager.GetNode(int.Parse(l.EndNodeId));
                CreateLink(left, right);
            }
        }
    }

    public static class LinkExtension
    {
        public static bool IsEqual(this Link a, Link b)
        {
            var aNodes = a.GetNodes();
            var bNodes = b.GetNodes();

            return (aNodes.Key == bNodes.Key && aNodes.Value == bNodes.Value) ||
                (aNodes.Key == bNodes.Value && aNodes.Value == bNodes.Key);
        }
    }
}