using System;
using System.Collections.Generic;
using System.Xml;
using Assets.Script.Rocket;
using UnityEngine;
using System.IO;

namespace Assets.Script
{
    public class GameManager : MonoBehaviour
    {
        public enum StateEnum
        {
            None,
            SettingRocket,
            OnPlaying,
            GameFail,
            GameSuccess,
        }

        public static StateMachine<StateEnum> StateMachine { get; set; }
        public static List<Link> LinkList { get; private set; }
        public static List<RocketBase> RocketList { get; private set; }
        public static Dictionary<int, Node> NodeDic { get; private set; }
        public static Queue<Enemy.EnemyTypeEnum> EnemyWaiting { get; private set; }
        public static int Frame { get; private set; } = 0;
        public static int DrillRocketUnused { get; set; }
        public static int ReturnRocketUnused { get; set; }
        public static string LevelPath { get; set; }
        public static Node BeginNode { get; set; }
        public static Node EndNode { get; set; }
        private static Link _firstLink;
        private static Link FirstLink {
            get
            {
                if (_firstLink == null)
                    foreach (var l in LinkList)
                    {
                        if (l.EndPoint1 == BeginNode || l.EndPoint2 == BeginNode)
                        {
                            _firstLink = l;
                        }
                    }

                return _firstLink;
            }
        }
        private static float _enemyPlacingFrame = 15;

        GameManager()
        {
            StateMachine = new StateMachine<StateEnum>();
            RocketList = new List<RocketBase>();
            EnemiesList = new EnemyList();
            NodeDic = new Dictionary<int, Node>();
            EnemyWaiting = new Queue<Enemy.EnemyTypeEnum>();
            LinkList = new List<Link>();
        }

        void Start()
        {
            try
            {
                SetMap();
            }
            catch
            {
                Debug.LogError("读取XML文件错误!");
            }
            StateMachineInit();
        }

        void Test_Start()
        {
            DrillRocketUnused = 5;
            ReturnRocketUnused = 2;
            
            Time.timeScale = 0f;
            Node begin = Factory.CreatNode(0,new Vector2(-6.44f, 2.91f));
            begin.BecomeBegin();
            Node end = Factory.CreatNode(1,new Vector2(4.43f,2.8f));
            end.BecomeEnd();
            Node node0 = Factory.CreatNode(2,new Vector2(-1.13f, 0.22f));
            Node node1 = Factory.CreatNode(3,new Vector2(3.83f, -3.29f));
            Node node2 = Factory.CreatNode(4,new Vector2(4, 0));
            
            LinkList = new List<Link>
            {
                Factory.CreatLink(begin,node0),
                Factory.CreatLink(node0,node1),
                Factory.CreatLink(node0,node2),
                Factory.CreatLink(node1,node2),
                Factory.CreatLink(node0,end)
            };

            RoutePosition r = new RoutePosition(LinkList[0],node0,0);
            EnemiesList.Add(Factory.CreatEnemy(r)); 
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            StateMachine.Run();
        }

        private bool test_shouldBorn = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var link = EnemiesList[3].Position.Link;
                var result =EnemiesList[3].SearchOneCrowding(link.GetNodeBeside(EnemiesList[3].Position.To));
                foreach (var e in result)
                {
                    if (e == null) continue;
                    Debug.Log("按下空格來使某些敵人變大");
                    e.BecomeBig();
                    Time.timeScale = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                test_shouldBorn = !test_shouldBorn;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                EnemiesList[0].TurnAround();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                EnemiesList[1].TurnAround();
            }
        }

        public static List<Link> SearchLinks(Node node)
        {
            var result = new List<Link>();
            foreach (var link in LinkList)
            {
                if(link.EndPoint1==node||link.EndPoint2==node)
                    result.Add(link);
            }

            return result;
        }

        private void StateMachineInit()
        {
            StateMachine.State = StateEnum.SettingRocket;
            StateMachine.RegisterAction(StateEnum.OnPlaying, () =>
            {
                Frame++;
                TrySetEnemyLoop();
                Time.timeScale = UIManager.CustomTimeScale;
                LoopEnemyMove();
                if (EnemiesList.IsEmpty()&& EnemyWaiting.Count == 0)
                {
                    StateMachine.State = StateEnum.GameSuccess;
                    UIManager.Instruction = "游戏胜利:D";
                }
            });
        }

        public static void GameStart()
        {
            StateMachine.State = StateEnum.OnPlaying;
            foreach (var r in GameManager.RocketList)
            {
                r.StateMachine.State = RocketBase.StateEnum.ReadyToLaunch;
            }
            Time.timeScale = UIManager.CustomTimeScale;
        }

        public static void GameFail()
        {
            if (StateMachine.State != StateEnum.OnPlaying)
            {
                Debug.LogError("游戏都没在玩，是谁调用这个函数啊？？");
                return;
            }
            Time.timeScale = 0;
            StateMachine.State = StateEnum.GameFail;
            UIManager.Instruction = "游戏结束：敌人到达终点\n点右边的按钮重置关卡吧";
        }
        private void SetMap()
        {
            //一、设置读取器
            Stream stream  = new FileStream(LevelPath,FileMode.Open);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(stream, settings);
            
            //二、生成点
            reader.ReadToFollowing("allnodes");
            while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "allnodes")
            {//读取循环到结束节点为止
                reader.ReadToFollowing("id");
                int id = Convert.ToInt32(reader.ReadString());
                reader.ReadToFollowing("x");
                float x = Convert.ToSingle(reader.ReadString());
                reader.ReadToFollowing("y");
                float y = Convert.ToSingle(reader.ReadString());
                Factory.CreatNode(id,new Vector2(x, y));
                reader.ReadEndElement();
                reader.ReadEndElement();
            }
            //设置起点终点
            reader.ReadToFollowing("startnode");
            reader.Read();
            NodeDic[reader.ReadContentAsInt()].BecomeBegin();
            reader.ReadToFollowing("endnode");
            reader.Read();
            NodeDic[reader.ReadContentAsInt()].BecomeEnd();
            
            //三、生成链接
            while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "links")
            {
                reader.ReadToFollowing("start");
                reader.Read();
                int start = reader.ReadContentAsInt();
                reader.ReadToFollowing("end");
                reader.Read();
                int end = reader.ReadContentAsInt();
                Factory.CreatLink(NodeDic[start], NodeDic[end]);
                reader.ReadEndElement();
                reader.ReadEndElement();
            }
            
            //四、设置敌人列表
            reader.ReadToFollowing("enemies");
            if (!reader.IsEmptyElement)
                while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "enemies")
                {
                    reader.ReadToFollowing("type");
                    reader.Read();
                    EnemyWaiting.Enqueue( (Enemy.EnemyTypeEnum)reader.ReadContentAsInt() );
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                }
            
            //五、生成既有火箭
            reader.ReadToFollowing("allarrows");
            if (!reader.IsEmptyElement)
                while (reader.NodeType != XmlNodeType.EndElement || reader.Name != "allarrows")
                {
                    reader.ReadToFollowing("type");
                    reader.Read();
                    int type = reader.ReadContentAsInt();
                    reader.ReadToFollowing("x");
                    reader.Read();
                    float x = reader.ReadContentAsFloat();
                    reader.ReadToFollowing("y");
                    reader.Read();
                    float y = reader.ReadContentAsFloat();
                    reader.ReadToFollowing("direction");
                    reader.Read();
                    float direction = reader.ReadContentAsFloat();
                    Vector2 position = new Vector2(x,y);
                    Vector2 dVector = new Vector2(Mathf.Cos(direction*Mathf.Deg2Rad+Mathf.PI/2), Mathf.Sin(direction*Mathf.Deg2Rad+Mathf.PI/2));
                    switch (type)
                    {
                        case 0 :
                            Factory.CreatDrillRocket(position, dVector);
                            break;
                        case 1 :
                            Factory.CreatReturnRocket(position, dVector);
                            break;
                    }
                    
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                }
            //六、设置玩家可用火箭
            reader.ReadToFollowing("attackarrow");
            reader.Read();
            DrillRocketUnused = reader.ReadContentAsInt();
            reader.ReadToFollowing("returnarrow");
            reader.Read();
            ReturnRocketUnused= reader.ReadContentAsInt();
            reader.Close();
        }

        private int _lastFrameSetEnemy = 0;
        private void TrySetEnemyLoop()
        {
            if (EnemyWaiting.Count == 0) return;
            if (Frame - _lastFrameSetEnemy > _enemyPlacingFrame) //准备写创造新敌人的条件
            {
                _lastFrameSetEnemy = Frame;
                RoutePosition r = new RoutePosition(FirstLink,FirstLink.GetNodeBeside(BeginNode),0);
                switch (EnemyWaiting.Dequeue())
                {
                    case Enemy.EnemyTypeEnum.Big:
                        Factory.CreatEnemy(r);
                        break;
                    case Enemy.EnemyTypeEnum.Small:
                        var e = Factory.CreatEnemy(r);
                        e.BecomeSmall();
                        break;
                    case Enemy.EnemyTypeEnum.None:
                        break;
                }
            }
        }

        #region 敌人的管理
        public static EnemyList EnemiesList { get; private set; }
        public static EnemyList EnemiesToDelete { get; } = new EnemyList();

        /// <summary>
        /// 每帧运行，处理敌人的移动
        /// </summary>
        private void LoopEnemyMove()
        {
            //零、充值所有敌人的期望
            foreach (var e in EnemiesList)
            {
                e.ResetExpectation();
                //Debug.Log("Im the last, FC="+EnemiesList.Last().CrowdedFront+", BC="+EnemiesList.Last().CrowdedBack);
            }
            
            //一、设置所有敌人的前力后力
            foreach (var e in EnemiesList)
            {
                if ( (e.CrowdedFront && (!e.CrowdedBack))//这个人在末端
                    || e.HasConflict//或者这个人有冲突
                    )
                {//对这个人，向前开始运算它们的前进后退期望（递归）
                    e.SetAllExpectation();
                }
            }

            //二、让每个敌人根据自己的情况运动，应该被删掉的敌人会把自己放到EnemiesToDelete里
            for(int i=0;i<EnemiesList.Count;i++)
            {
                if (EnemiesList[i] == null) continue;
                EnemiesList[i].Move();
            }
            
            //三、删掉刚才应该被删掉的敌人
            foreach (var e in EnemiesToDelete)
            {
                if (EnemiesList.Contains(e))
                {
                    EnemiesList.Remove(e);
                    Destroy(e.gameObject);
                }
            }
        }

        #endregion
    }
}
