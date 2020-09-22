using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GameEditor
{
    public class NodesManager : IXmlDataSave
    {
        public Transform NodesParent { get; private set; }
        private int GlobalIndex { get => globalIndex++; }
        private int globalIndex;
        private string NodesParentName { get; } = "Nodes";
        private Dictionary<int, Node> Nodes { get; } = new Dictionary<int, Node>();
        private Node StartNode { get; set; }
        private Node EndNode { get; set; }

        private static string[] NodesName { get; } =
        {
            "Node",
            "StartNode",
            "EndNode",
        };


        public NodesManager()
        {
            FindParent();
            globalIndex = 0;
        }

        public Node CreateNode(Node.NodeType type, Vector2 position, int id = -1)
        {
            string nodeName = NodesName[(int)type];

            Node node = GameManager.Instance.ResourcesManager.GetNode(nodeName);
            node = GameObject.Instantiate(node, position, Quaternion.identity, NodesParent);
            node.Init(id == -1 ? GlobalIndex : id, position, type);

            SetNode(node);

            return node;
        }

        public void DeleteNode(Node node)
        {
            ClearNode(node);
            GameObject.Destroy(node.GameObject);
        }

        public void DeleteAllNodes()
        {
            var objs = GetNodes();

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] == null)
                    continue;

                DeleteNode(objs[i]);
            }
        }

        public bool HasNodeTypeExisted(Node.NodeType type)
        {
            switch (type)
            {
                case Node.NodeType.NormalNode:
                    return Nodes.Count != 0;
                case Node.NodeType.StartNode:
                    return StartNode != null;
                case Node.NodeType.EndNode:
                    return EndNode != null;
            }

            throw new System.Exception($"不存在{type}类型的节点!");
        }

        public Node[] GetNodes()
        {
            return Nodes.Values.ToArray();
        }

        public Node GetNode(int id)
        {
            if (Nodes.ContainsKey(id))
                return Nodes[id];
            return null;
        }

        public Node GetMouseNearestNode(float selectRange = 0.5f)
        {
            Node[] nodes = GetNodes();
            if (nodes == null || nodes.Length == 0)
                return null;

            KeyValuePair<Node, float> nearest = new KeyValuePair<Node, float>(nodes[0], float.MaxValue);
            foreach (var n in nodes)
            {
                float distance = Vector2.Distance(n.Position, MouseUtils.MouseWorldPosition);
                if (distance < nearest.Value)
                    nearest = new KeyValuePair<Node, float>(n, distance);
            }

            if (nearest.Value < selectRange)
                return nearest.Key;
            return null;
        }

        private void SetNode(Node node)
        {
            Node temp = null;
            switch (node.Type)
            {
                case Node.NodeType.NormalNode:
                    break;
                case Node.NodeType.StartNode:
                    temp = StartNode;
                    SetSpecialNode(node, ref temp);
                    StartNode = temp;
                    break;
                case Node.NodeType.EndNode:
                    temp = EndNode;
                    SetSpecialNode(node, ref temp);
                    EndNode = temp;
                    break;
            }
            Nodes.Add(node.Id, node);
        }

        private void SetSpecialNode(Node node, ref Node target)
        {
            if (target == null)
            {
                target = node;
                return;
            }

            DeleteNode(target);
            target = node;
        }

        private void ClearNode(Node node)
        {
            switch (node.Type)
            {
                case Node.NodeType.NormalNode:
                    break;
                case Node.NodeType.StartNode:
                    StartNode = null;
                    break;
                case Node.NodeType.EndNode:
                    EndNode = null;
                    break;
            }
            RemoveNode(node);
        }

        private void RemoveNode(Node node)
        {
            if (Nodes.ContainsKey(node.Id))
            {
                Nodes.Remove(node.Id);
            }
        }

        private void FindParent()
        {
            GameObject obj = GameObject.Find(NodesParentName);
            if (obj == null)
                obj = new GameObject(NodesParentName);

            NodesParent = obj.transform;
            NodesParent.transform.position = Vector2.zero;
        }

        public XElement GetXmlData()
        {
            XElement root = new XElement("nodes");
            SetAllNodes(root);
            SetEntrance(root);

            return root;
        }

        private void SetAllNodes(XElement root)
        {
            XElement node = new XElement("allnodes");

            foreach (var n in Nodes)
            {
                node.Add(new XElement("node",
                            new XElement("id", n.Value.Id),
                            new XElement("x", n.Value.Position.x),
                            new XElement("y", n.Value.Position.y)));
            }

            root.Add(node);
        }

        private void SetEntrance(XElement root)
        {
            XElement node = new XElement("entrancenodes");
            if (StartNode != null)
                node.Add(new XElement("startnode", StartNode.Id));
            if (EndNode != null)
                node.Add(new XElement("endnode", EndNode.Id));

            root.Add(node);
        }

        public void LoadXmlData(XmlDataContainer dataContainer)
        {
            SetDefaultState();
            try
            {
                LoadAllNodes(dataContainer);
                LoadEntrance(dataContainer);
            }
            catch
            {
                Debug.LogError("读取的XML文件残损或格式有误!");
            }
        }

        private void SetDefaultState()
        {
            DeleteAllNodes();
        }

        private void LoadAllNodes(XmlDataContainer dataContainer)
        {
            var nodes = from node in dataContainer.Document.Root.Element("nodes").Element("allnodes").Elements()
                        select new
                        {
                            Id = node.Element("id").Value,
                            X = node.Element("x").Value,
                            Y = node.Element("y").Value,
                        };

            foreach (var n in nodes)
            {
                CreateNode(Node.NodeType.NormalNode, new Vector2(float.Parse(n.X), float.Parse(n.Y)), int.Parse(n.Id));
                globalIndex = Mathf.Max(int.Parse(n.Id), globalIndex);
            }
            globalIndex++;
        }

        private void LoadEntrance(XmlDataContainer dataContainer)
        {
            var root = dataContainer.Document.Root.Element("nodes").Element("entrancenodes");
            int StartNodeId = int.Parse(root.Element("startnode").Value);
            int EndNodeId = int.Parse(root.Element("endnode").Value);

            Vector2 tempPosition = Nodes[StartNodeId].Position;
            DeleteNode(Nodes[StartNodeId]);
            CreateNode(Node.NodeType.StartNode, tempPosition, StartNodeId);

            tempPosition = Nodes[EndNodeId].Position;
            DeleteNode(Nodes[EndNodeId]);
            CreateNode(Node.NodeType.EndNode, tempPosition, EndNodeId);
        }
    }
}