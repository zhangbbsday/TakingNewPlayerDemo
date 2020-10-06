using System;
using System.Linq;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Script
{
    public class Enemy : MonoBehaviour
    {
        public enum EnemyTypeEnum
        {
            None,
            Small,
            Big
        }

        public enum MoveEnum
        {
            None,
            Forward,
            Backward,
            Stop
        }

        private GameObject _smallPic;
        private GameObject _bigPic;
        private RoutePosition _routePosition;
        public static float _moveDistance = 0.05f;
        private float _touchDistance = 0.02f; //两个人检测接触时，在他们之间隔着这个距离也算作接触
        private EnemyTypeEnum _type = EnemyTypeEnum.Big;
        private int _pushingPriority;
        [SerializeField] private AudioClip _popSound;
        public static float BigSize { get; } = 0.55f;
        public static float SmallSize { get; } = 0.25f;
        private GameObject SmallPic
        {
            get
            {
                if (_smallPic == null) Init();
                return _smallPic;
            }
        }
        public GameObject BigPic
        {
            get
            {
                if (_bigPic == null) Init();
                return _bigPic;
            }
        }
        public RoutePosition Position
        {
            get => _routePosition;
            set
            {
                //赋值
                _routePosition = value;

                //设置位置
                Vector2 to = _routePosition.To.Position;
                Vector2 from = Position.Link.GetNodeBeside(Position.To).Position;
                Vector2 delta = to - from;
                if (_routePosition.Distance > delta.magnitude) Debug.LogError("有一个敌人被设置了错误的位置：距连接开头的距离大于连接的距离");
                transform.position = from + delta.normalized * _routePosition.Distance;

                //设置朝向
                transform.rotation =
                    Quaternion.AngleAxis(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            }
        }
        public Vector2 GlobalPosition => transform.position;
        public float Size { get; private set; } = 0.68f;
        public int PushingPriority
        {
            get=>_pushingPriority;
            private set { 
                ForwardExpectation = value;
                _pushingPriority = value;
            }
        }
        public EnemyTypeEnum Type
        {
            get => _type;
            set
            {
                _type = value;
                switch (value)
                {
                    case EnemyTypeEnum.Big:
                        BigPic.SetActive(true);
                        SmallPic.SetActive(false);
                        Size = BigSize;
                        PushingPriority = 2;
                        break;
                    case EnemyTypeEnum.Small:
                        BigPic.SetActive(false);
                        SmallPic.SetActive(true);
                        Size = SmallSize;
                        PushingPriority = 1;
                        break;
                }
            }
        }

        public float DistanceToNode(Node n)
        {
            if (Position.To == n) return Position.Link.Distance - Position.Distance;
            else return Position.Distance;
        }
        /// <summary>
        /// 根据当前位置，获得应该前往的下一个连接
        /// </summary>
        public Link NextLink
        {
            get
            {
                return GetNextLink(Position);
            }
        }
        public static Link GetNextLink(RoutePosition r)
        {
            return r.To.RightSideOf(r.Link);
        }
        public static Link GetPreLink(RoutePosition r)
        {
            return r.From.LeftSideOf(r.Link);
        }
        public Link PreLink => GetPreLink(Position);
        public bool CrowdedFront
        {
            get
            {
                var result = SearchOneCrowding(Position.To);
                if (result[0] == null && result[1] == null && result[2] == null) return false;
                else return true;
            }
        }
        public bool CrowdedBack
        {
            get
            {
                var result = SearchOneCrowding(Position.From);
                if (result[0] == null && result[1] == null && result[2] == null) return false;
                else return true;
            }
        }

        /// <summary>
        /// 和前面一个人面对面
        /// </summary>
        public bool HasConflict{
            get
            {
                bool noConflict = true;
                var next = SearchOneCrowding(Position.To);
                if (next[1] != null)
                {
                    if (next[1].Position.To == Position.From)
                        noConflict = false;
                }
                if (next[0] != null)//左边的路有毗邻敌人
                {
                    if (next[0].Position.To == Position.To)
                        noConflict = false;
                }

                if (next[2] != null)
                {
                    if (next[2].Position.To == Position.To)
                        noConflict = false;
                }

                return !noConflict;
            }
        }

        #region 为Move服务
        private void MoveForward(float distance)
        {
            if (test_select)
            {
                Debug.Log("f="+ForwardExpectation+",b="+BackwardExpectation);
            }
            RoutePosition targetPosition = Position;
            targetPosition.Distance += distance;
            if (targetPosition.Distance >= Position.Link.Distance)//通过了节点
            {
                if (Position.To.EndPic.activeSelf)
                {//到达终点
                    GameManager.GameFail();
                }
                else if (GameManager.SearchLinks(Position.To).Count == 1)
                {//到达末端
                    Die();
                }
                else
                {//通过一般末端
                    float distanceUsed = targetPosition.Distance - Position.Link.Distance;//在本次连接中移动的距离
                    //设置新出发地，并且以剩余距离为参数重新出发
                    targetPosition.Link = NextLink;
                    targetPosition.To = targetPosition.Link.GetNodeBeside(Position.To);
                    targetPosition.Distance = 0;
                    Position = targetPosition;
                    MoveForward(distance-distanceUsed);
                }
            }
            else
            {//没有通过节点
                Position = targetPosition;
            }
        }
        public void MoveForward()
        {
            MoveForward(_moveDistance);
        }
        public void TurnAround()
        {
            var p = Position;
            p.To = p.From;
            p.Distance = p.Link.Distance - p.Distance;
            Position = p;
        }
        private void MoveBack()
        {
            //掉头、向前走再掉头
            TurnAround();
            MoveForward(_moveDistance);
            TurnAround();
        }
        private void MoveStop()
        {
        }

        /// <summary>
        /// 0距离side最远,last距离side最近
        /// </summary>
        private static EnemyList GetOrderedEnemyBySide(Link link,Node side)
        {
            var enemiesOnLink = new EnemyList();
            foreach (var e in GameManager.EnemiesList)
            {
                if(e.Position.Link==link)
                    enemiesOnLink.Add(e);
            }
            enemiesOnLink.Sort((x,y)=>x.DistanceToNode(side)>y.DistanceToNode(side)?-1:1);
            return enemiesOnLink;
        }
        /// <summary>
        /// 对某条链接从某个端点开始，展开长度为Distance的搜索，获取全部挤压着的敌人
        /// </summary>
        /// <return>搜索到的敌人，最多只能搜索到一个</return>
        public static Enemy SearchOneCrowding(Link link, Node from, float distance)
        {//放弃搜索道路时搜索到两条下路的情况
            var enemiesOnLink = GetOrderedEnemyBySide(link, from);
            if (enemiesOnLink.Count == 0)
            {//路上没有敌人时，看这条路够不够长
                if (link.Distance > distance)
                {//长度足够，没有敌人就返回一个空list
                    return null;
                }
                else
                {//长度不足，需要对下一条路径搜索。但有一个特殊情况
                    var linksOfNextNode = GameManager.SearchLinks(link.GetNodeBeside(from));
                    if (linksOfNextNode.Count == 1) //特殊情况，下一条链接不存在
                        return null;

                    //正常情况：继续对下一条链接进行搜索
                    return SearchOneCrowding(GetPreLink(new RoutePosition(link, from, 0)), link.GetNodeBeside(from),
                        distance - link.Distance);
                }
            }
            else
            {//路上有敌人时，根据敌人类型判断是否接纳他
                var target = enemiesOnLink.Last();
                if (Math.Abs(target.Size - SmallSize) < 1e-5)
                {//如果目标是小的：我们要对distance进行进一步减小。因为我们当初在设置distance时默认这个目标是大的
                    distance -= BigSize - SmallSize;
                }
                //真正开始判断目标是否离from端点足够近
                if (target.DistanceToNode(from) <= distance)
                {
                    var result = GetOrderedEnemyBySide(link, from).Last();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取我一侧的敌人，返回的链表0左1中2右，相应位置没有敌人则为null
        /// </summary>
        public EnemyList SearchOneCrowding(Node side)
        {
            var result = new EnemyList() {null, null, null};
            if (side != Position.Link.EndPoint1 && side != Position.Link.EndPoint2)
            {
                Time.timeScale = 0;
                Debug.LogError("一个敌人尝试搜索临近人时，参数不是他两侧的节点");
            }
            
            //获取连接上所有的敌人，按照距离side的距离排序
            var enemiesOnLink = GetOrderedEnemyBySide(Position.Link,side);
            enemiesOnLink.Reverse();
            //如果我是最靠近side的，就进行脱离人查找，反之就判断比我更靠近的那个人是否符合条件
            int myIndex = enemiesOnLink.FindIndex(x => x == this);
            if (myIndex == 0)
            {//我是最靠近side的，需要进行脱离人的链接查找，这里的算法还要实现搜索哪条路（如果那是一个末端节点，就截止搜索）
                //检查来源是否是末端
                var linkList = GameManager.SearchLinks(side);
                if(linkList.Count==1) return result;
                
                //展开脱离人的查找
                var myPos = Position;
                myPos.To = myPos.Link.GetNodeBeside(side);
                var targetLink = side.LeftSideOf(Position.Link);
                var left = SearchOneCrowding(targetLink,side,Size+BigSize-DistanceToNode(side)+_touchDistance+0.051f);
                targetLink = side.RightSideOf(Position.Link);
                var right = SearchOneCrowding(targetLink, side,
                    Size + BigSize - DistanceToNode(side) + _touchDistance+0.051f);
                result[0] = left;
                result[2] = right;
                return result;
            }
            else
            {//有人比我距离side更近，查找比我更靠近side的那个人
                var target = enemiesOnLink[myIndex - 1];
                if (DistanceToNode(side)-target.DistanceToNode(side)>Size+target.Size+_touchDistance)
                {//如果那个更近的人他离我太远。结束查找
                    return result;
                }
                else
                {//那个人距离合适，即我们呈挤压状态。对他进行查找
                    result[1] = target;
                    return result;
                }
            }
        }
        /*
        /// <summary>
        /// 如果有注定的行动方式，就直接行动
        /// </summary>
        public bool TryNoDetectMove()
        {
            if (noDectectMovement.ContainsKey(GameManager.Frame) == false) return false;
            switch (noDectectMovement[GameManager.Frame])
            {
                case MoveEnum.Backward:
                    MoveBack();
                    break;
                case MoveEnum.Forward:
                    MoveForward();
                    break;
                case MoveEnum.Stop:
                    MoveStop();
                    break;
            }

            return true;

        }*/

        #region 前进后退期望
        private int ForwardExpectation { set; get; } = 0;//前进期望
        private int BackwardExpectation { set; get; } = 0;//后退期望
        public void TrySetExpectation(MoveEnum move,int value)
        {
            if(move==MoveEnum.Forward)
                if (value > ForwardExpectation)
                    ForwardExpectation = value;
            if(move==MoveEnum.Backward)
                if (value > BackwardExpectation)
                    BackwardExpectation = value;
        }
        public void TrySetExpectation(Node side,int value)
        {
            if(Position.To==side)
                TrySetExpectation(MoveEnum.Forward,value);
            else
                TrySetExpectation(MoveEnum.Backward,value);
        }

        public int GetExpectation(Node side)
        {
            if (Position.To == side) return ForwardExpectation;
            else return BackwardExpectation;
        }

        private static EnemyList SignBook { get; set; }//给下面两个函数用，递归头会生成一本签名书，递归尾会把它删掉

        public void SetAllExpectation()
        {
            SetAllExpectation(Position.To);
        }

        public void SetAllExpectation(Node side)
        {
            if(_bigPic.activeSelf) Debug.Log("f="+ForwardExpectation+",b="+BackwardExpectation);
            //防死循环：在书上签名，如果已经签过名了，就向上返回
            bool thisIsRecurrenceHead = false;//这是递归头
            if (SignBook == null)
            {
                SignBook = new EnemyList();
                thisIsRecurrenceHead = true;
            }

            if (SignBook.Contains(this))
            {
                if(BackwardExpectation<ForwardExpectation)
                    return;
            }
            else SignBook.Add(this);
            
            EnemyList next = SearchOneCrowding(side);
            if (next[1] != null)//与我同条队列，前面的人
            {
                var n = next[1];
                n.TrySetExpectation(side,GetExpectation(side));
                n.SetAllExpectation(side);
            }
            if (next[0] != null) //越过节点的左边队列
            {
                var n = next[0];
                var nextSide = n.Position.Link.GetNodeBeside(side);//推力传导的方向
                n.TrySetExpectation(nextSide, GetExpectation(side));
                n.SetAllExpectation(nextSide);
            }
            if (next[2] != null) //同上右边
            {
                var n = next[2];
                var nextSide = n.Position.Link.GetNodeBeside(side);//推力传导的方向
                n.TrySetExpectation(nextSide, GetExpectation(side));
                n.SetAllExpectation(nextSide);
            }
            
            //防死循环：如果我是最顶级递归，就把签名书删掉
            if (thisIsRecurrenceHead) SignBook = null;
        }

        public void ResetExpectation()
        {
            ForwardExpectation = PushingPriority;
            BackwardExpectation = 0;
        }
        #endregion

        #endregion
        public void Move()
        {
            if(BackwardExpectation<ForwardExpectation) MoveForward();
            if(BackwardExpectation==ForwardExpectation) MoveStop();
            if(BackwardExpectation>ForwardExpectation) MoveBack();
        }

        public void Die()
        {
            GameManager.PlaySound(_popSound);
            GameManager.EnemiesToDelete.Add(this);
        }

        void Init()
        {
            _smallPic = transform.Find("EnemySmall").gameObject;
            _bigPic = transform.Find("EnemyBig").gameObject;
        }
        public void BecomeBig()
        {
            Type = EnemyTypeEnum.Big;
        }
        public void BecomeSmall()
        {
            Type = EnemyTypeEnum.Small;
        }

        #region 測試
        public bool test_select = false;
        public void OnMouseDown()
        {
            Debug.Log("selected");
            test_select = true;
        }
        
        #endregion
       
    }
}
