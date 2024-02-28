namespace LispInterpreter.BusinessLogic.Nodes
{
    public class ListNode : Node
    {
        public List<Node> Elements { get; }

        public ListNode(List<Node> elements)
        {
            this.Elements = elements;
        }
    }
}
