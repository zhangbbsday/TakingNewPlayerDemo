using UnityEngine;
using Assets.Script;
using Assets.Script.Rocket;

namespace Assets.Script
{
    public class Factory
    {
        private static GameObject _node;
        private static GameObject _link;
        private static GameObject _enemy;
        private static GameObject _drillRocket;
        private static GameObject _returnRocket;

        public static void Init()
        {
            _node = Resources.Load<GameObject>("Prefab/Node");
            _link = Resources.Load<GameObject>("Prefab/Link");
            _enemy = Resources.Load<GameObject>("Prefab/Enemy");
            _drillRocket = Resources.Load<GameObject>("Prefab/DrillRocket");
            _returnRocket = Resources.Load<GameObject>("Prefab/ReturnRocket");
            
        }

        public static Node CreatNode(int id,Vector2 position)
        {
            if(_node == null) Init();
            GameObject node = Object.Instantiate(_node);
            node.transform.position = position;
            GameManager.NodeDic.Add(id,node.GetComponent<Node>());
            return node.GetComponent<Node>();
        }

        private static Link CreatLink()
        {
            if (_link == null) Init();
            GameObject link = Object.Instantiate(_link);
            var script = link.GetComponent<Link>();
            GameManager.LinkList.Add(script);
            return script;
        }
        public static Link CreatLink(Node node1,Node node2)
        {
            Link l = CreatLink();
            l.Init(node1,node2);
            return l;
        }
        public static Enemy CreatEnemy(RoutePosition routePosition)
        {
            if(_enemy == null) Init();
            GameObject enemy = Object.Instantiate(_enemy);
            Enemy script =  enemy.GetComponent<Enemy>();
            script.Position = routePosition;
            script.BecomeBig();
            GameManager.EnemiesList.Add(script);
            return script;
        }
        public static DrillRocket CreatDrillRocket()
        {
            if(_drillRocket==null) Init();
            GameObject rocket = Object.Instantiate(_drillRocket);
            DrillRocket script = rocket.GetComponent<DrillRocket>();
            return script;
        }
        public static DrillRocket CreatDrillRocket(Vector2 position, Vector2 orientation)
        {
            var r = CreatDrillRocket();
            r.Position = position;
            r.Orientation = orientation;
            return r;
        }
        public static ReturnRocket CreatReturnRocket()
        {
            if(_returnRocket==null) Init();
            GameObject rocket = Object.Instantiate(_returnRocket);
            ReturnRocket script = rocket.GetComponent<ReturnRocket>();
            return script;
        }
        public static ReturnRocket CreatReturnRocket(Vector2 position, Vector2 orientation)
        {
            var r = CreatReturnRocket();
            r.Position = position;
            r.Orientation = orientation;
            return r;
        }
    }
}
