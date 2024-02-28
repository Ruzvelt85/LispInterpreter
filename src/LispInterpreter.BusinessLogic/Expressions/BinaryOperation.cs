using LispInterpreter.BusinessLogic.Common;
using LispInterpreter.BusinessLogic.Exceptions;

namespace LispInterpreter.BusinessLogic.Expressions
{
    /// <summary>
    /// Represents a built-in operation on a list of arguments (like (+ 1 2 3))
    /// </summary>
    public class BinaryOperation : IExpression
    {
        private readonly string _operation;
        private readonly IExpression _operand1;
        private readonly IExpression _operand2;

        public BinaryOperation(string operation, IExpression operand1, IExpression operand2)
        {
            _operation = operation; 
            _operand1 = operand1; 
            _operand2 = operand2; 
        }

        public Value Evaluate(IDictionary<string, IExpression> environment)
        {
            var operand1 = _operand1.Evaluate(environment) as NumberValue;
            var operand2 = _operand2.Evaluate(environment) as NumberValue;

            if (operand1 == null || operand2 == null)
            {
                throw new SyntaxErrorException("At least one operand of the binary expression is not a number...");
            }

            switch (_operation)
            {
                case MathOperations.Add: 
                    return new NumberValue(operand1.Value + operand2.Value);
                case MathOperations.Subtract: 
                    return new NumberValue(operand1.Value - operand2.Value);
                case MathOperations.Multiply: 
                    return new NumberValue(operand1.Value * operand2.Value);
                case MathOperations.Divide: 
                    return new NumberValue(operand1.Value / operand2.Value);
                case LogicalOperations.Less: 
                    return new BoolValue(operand1.Value < operand2.Value);
                case LogicalOperations.LessOrEqual: 
                    return new BoolValue(operand1.Value <= operand2.Value);
                case LogicalOperations.Greater: 
                    return new BoolValue(operand1.Value > operand2.Value);
                case LogicalOperations.GreaterOrEqual: 
                    return new BoolValue(operand1.Value >= operand2.Value);
                case LogicalOperations.Equal: 
                    return new BoolValue(operand1.Value == operand2.Value);
                case LogicalOperations.NotEqual: 
                    return new BoolValue(operand1.Value != operand2.Value);
                default:
                    throw new InvalidOperationException($"The operation {_operation} is not supported...");
            }
        }
    }
}
