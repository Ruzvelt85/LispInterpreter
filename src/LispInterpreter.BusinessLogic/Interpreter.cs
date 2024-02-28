using LispInterpreter.BusinessLogic.Expressions;
using LispInterpreter.BusinessLogic.Nodes;
using LispInterpreter.BusinessLogic.Common;
using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.BusinessLogic
{
    public class Interpreter
    {
        private readonly IDictionary<string, IExpression> _environment;

        public Interpreter()
        {
            this._environment = new Dictionary<string, IExpression>();
        }

        public string Process(string expression)
        {
            if (string.IsNullOrEmpty(expression) || !expression.Contains('(') || !expression.Contains(')'))
            {
                throw new SyntaxErrorException("Entered input is not a LISP expression");
            }

            // Step 1: Tokenize the expression into list of tokens
            var tokens = Tokenizer.Tokenize(expression);

            // Step 2: Parse the list of tokens into Abstract Syntax Tree
            var nodeList = Parse(tokens);
            var ast = TranslateToAst(nodeList);

            if (ast is DefineExpression)
            {
                return "Defined...";
            }

            // Step 3: Evaluate the result
            var evaluation = ast.Evaluate(this._environment);

            if (evaluation is NumberValue number)
            {
                return number.Value.ToString();
            }

            throw new SyntaxErrorException("An expression cannot be evaluated...");
        }

        /// <summary>
        /// Parses list of tokens into listed or symbol (atomic) nodes
        /// </summary>
        internal Node Parse(List<string> tokens)
        {
            if (tokens.Count == 0)
            {
                throw new SyntaxErrorException("No tokens were found...");
            }

            string token = tokens[0];
            tokens.RemoveAt(0);

            if (token == "(")
            {
                List<Node> nodes = new List<Node>();
                while (tokens[0] != ")")
                {
                    nodes.Add(Parse(tokens));
                }
                // Remove closing parenthesis
                tokens.RemoveAt(0); 
                
                return new ListNode(nodes);
            }

            return new SymbolNode(token);
        }

        /// <summary>
        /// Translates a list of nodes into Abstract Syntax Tree
        /// </summary>
        internal IExpression TranslateToAst(Node node)
        {
            if (node == null)
            {
                throw new SyntaxErrorException("A node has no value...");
            }

            // A symbol (atomic) node can be only translated into number or variable
            if (node is SymbolNode symbolNode)
            {
                if (int.TryParse(symbolNode.Value, out int arg))
                {
                    return new NumberValue(arg);
                }

                return new Variable(symbolNode.Value);
            }

            // The rest of the method - analyzing node as ListNode
            var tokens = ((ListNode)node).Elements;

            var head = tokens[0];
            tokens.RemoveAt(0);

            if (head is ListNode)
            {
                throw new SyntaxErrorException("This expression is not supported by our simple interpreter...");
            }

            var firstSymbolNode = (SymbolNode)head;

            switch (firstSymbolNode.Value)
            {
                case MathOperations.Add:
                case MathOperations.Subtract:
                case MathOperations.Multiply:
                case MathOperations.Divide:
                case LogicalOperations.Less:
                case LogicalOperations.LessOrEqual:
                case LogicalOperations.Greater:
                case LogicalOperations.GreaterOrEqual:
                case LogicalOperations.Equal:
                case LogicalOperations.NotEqual:
                    if (tokens.Count != 2)
                    {
                        throw new SyntaxErrorException("Binary operations can support only 2 arguments...");
                    }

                    return new BinaryOperation(firstSymbolNode.Value, TranslateToAst(tokens[0]), TranslateToAst(tokens[1]));

                case Statements.Define:
                    if (tokens.Count != 2)
                    {
                        throw new SyntaxErrorException("Define expression is not correct...");
                    }

                    // Variable
                    if (tokens[0] is SymbolNode && tokens[1] is SymbolNode)
                    {
                        var varName = (SymbolNode)tokens[0];

                        return DefineVariableOrFunction(varName.Value, TranslateToAst(tokens[1]));
                    }

                    // Function
                    if (tokens[0] is ListNode && tokens[1] is ListNode)
                    {
                        var nameAndParams = ((ListNode)tokens[0]).Elements;
                        if (nameAndParams.Count < 1 || nameAndParams.Any(x => x is not SymbolNode))
                        {
                            throw new SyntaxErrorException("Expression for defining a function is not correct...");
                        }
                        var functionName = ((SymbolNode)nameAndParams[0]).Value;
                        nameAndParams.RemoveAt(0);

                        return DefineVariableOrFunction(functionName, new Function(functionName, TranslateToAst(tokens[1]),
                            nameAndParams.Select(param => ((SymbolNode)param).Value).ToArray()));
                    }

                    throw new SyntaxErrorException("Unexpected end of expression");
                case Statements.If:
                    if (tokens.Count != 3)
                    {
                        throw new SyntaxErrorException("Conditional expression is not correct...");
                    }

                    return new ConditionalExpression(TranslateToAst(tokens[0]), TranslateToAst(tokens[1]),
                        TranslateToAst(tokens[2]));

                default:
                    if (!this._environment.ContainsKey(firstSymbolNode.Value))
                    {
                        throw new SyntaxErrorException($"Function with name '{firstSymbolNode.Value}' is not found");
                    }

                    return new CallExpression(this._environment[firstSymbolNode.Value], tokens.Select(TranslateToAst).ToArray());
            }
        }

        internal IExpression DefineVariableOrFunction(string name, IExpression value)
        {
            this._environment[name] = value;
            return new DefineExpression();
        }
    }
}
