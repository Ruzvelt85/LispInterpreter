namespace LispInterpreter.BusinessLogic.Nodes
{
    public class SymbolNode : Node
    {
        public string Value { get; }

        public SymbolNode(string value)
        {
            this.Value = value;
        }
    }
}
