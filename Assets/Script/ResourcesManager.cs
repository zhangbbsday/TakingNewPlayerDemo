using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class ResourcesManager
    {
        private Dictionary<string, Node> Nodes { get; } = new Dictionary<string, Node>();
        private Dictionary<string, Link> Links { get; } = new Dictionary<string, Link>();
        private Dictionary<string, EnemyContainer> EnemyContainers { get; } = new Dictionary<string, EnemyContainer>();
        private Dictionary<string, Arrow> Arrows { get; } = new Dictionary<string, Arrow>();
        private Dictionary<string, FileContainer> FileContainers { get; } = new Dictionary<string, FileContainer>();

        private string[] NodesPath { get; } =
        {
        "Prefabs/Nodes/StartNode",
        "Prefabs/Nodes/EndNode",
        "Prefabs/Nodes/Node",
    };

        private string[] LinkPath { get; } =
        {
        "Prefabs/Links/Link",
    };

        private string[] EnemiesPath { get; } =
        {
        "Prefabs/Enemies/EnemyContainer",
    };

        private string[] ArrowsPath { get; } =
        {
       "Prefabs/Arrows/AttackArrow",
       "Prefabs/Arrows/ReturnArrow",
    };

        private string[] FilesContainerPath { get; } =
        {
        "Prefabs/Files/FileContainer",
    };

        public ResourcesManager()
        {
            Load();
        }

        private void Load()
        {
            LoadNodes();
            LoadLinks();
            LoadEnemies();
            LoadArrows();
            LoadFileContainers();
        }

        public Node GetNode(string name)
        {
            return Nodes.ContainsKey(name) ? Nodes[name] : null;
        }

        public Link GetLink(string name)
        {
            return Links.ContainsKey(name) ? Links[name] : null;
        }

        public EnemyContainer GetEnemyContainer(string name)
        {
            return EnemyContainers.ContainsKey(name) ? EnemyContainers[name] : null;
        }

        public Arrow GetArrow(string name)
        {
            return Arrows.ContainsKey(name) ? Arrows[name] : null;
        }

        public FileContainer GetFileContainer(string name)
        {
            return FileContainers.ContainsKey(name) ? FileContainers[name] : null;
        }

        private void LoadNodes()
        {
            foreach (var path in NodesPath)
            {
                Node obj = Resources.Load<Node>(path);

                if (Nodes.ContainsKey(obj.name))
                    throw new System.Exception($"存在重名文件{obj.name}!");

                Nodes.Add(obj.name, obj);
            }
        }

        private void LoadLinks()
        {
            foreach (var path in LinkPath)
            {
                Link obj = Resources.Load<Link>(path);

                if (Links.ContainsKey(obj.name))
                    throw new System.Exception($"存在重名文件{obj.name}!");

                Links.Add(obj.name, obj);
            }
        }

        private void LoadEnemies()
        {
            foreach (var path in EnemiesPath)
            {
                EnemyContainer obj = Resources.Load<EnemyContainer>(path);

                if (EnemyContainers.ContainsKey(obj.name))
                    throw new System.Exception($"存在重名文件{obj.name}!");

                EnemyContainers.Add(obj.name, obj);
            }
        }

        private void LoadArrows()
        {
            foreach (var path in ArrowsPath)
            {
                Arrow obj = Resources.Load<Arrow>(path);

                if (Arrows.ContainsKey(obj.name))
                    throw new System.Exception($"存在重名文件{obj.name}!");

                Arrows.Add(obj.name, obj);
            }
        }

        private void LoadFileContainers()
        {
            foreach (var path in FilesContainerPath)
            {
                FileContainer obj = Resources.Load<FileContainer>(path);

                if (Arrows.ContainsKey(obj.name))
                    throw new System.Exception($"存在重名文件{obj.name}!");

                FileContainers.Add(obj.name, obj);
            }
        }
    }
}