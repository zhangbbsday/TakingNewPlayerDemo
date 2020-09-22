namespace Assets.Script
{
    public struct RoutePosition
    {
        public Link Link { get; set; }
        public Node To { get; set; }
        public Node From { get=>Link.GetNodeBeside(To); }
        public float Distance { get; set; }

        public RoutePosition(Link link, Node to,float distance)
        {
            Link = link;
            To = to;
            Distance = distance;
        }
    }
}
