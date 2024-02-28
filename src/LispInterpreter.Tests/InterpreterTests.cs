using LispInterpreter.BusinessLogic;

namespace LispInterpreter.Tests
{
    public class InterpreterTests
    {
        private readonly Interpreter _interpreter;

        public InterpreterTests()
        {
            this._interpreter = new Interpreter();
        }

        [Fact]
        public void Interpret_Calculates_Basic_Arithmetic()
        {
            // Act & Assert
            Assert.Equal("5", _interpreter.Process("(+ 2 3)"));
            Assert.Equal("25", _interpreter.Process("(- 40 15)"));
            Assert.Equal("20", _interpreter.Process("(* 4 5)"));
            Assert.Equal("6", _interpreter.Process("(/ 30 5)"));
        }

        [Fact]
        public void Interpret_Evaluates_Nested_Expressions()
        {
            // Act & Assert
            Assert.Equal("15", _interpreter.Process("(* (+ 2 3) (- 5 2))"));
        }

        [Fact]
        public void Interpret_DefineVariableAndUseInExpression_ReturnsExpectedResult()
        {
            // Act
            _interpreter.Process("(define a 10)");
            string result = _interpreter.Process("(+ a 5)");

            // Assert
            Assert.Equal("15", result);
        }

        [Fact]
        public void Interpret_Handles_ConditionalLogic()
        {
            // Act && Assert
            Assert.Equal("2", _interpreter.Process("(if (> 10 5) (+ 1 1) (+ 2 2))"));
            Assert.Equal("4", _interpreter.Process("(if (> 5 10) (+ 1 1) (+ 2 2))"));
        }

        [Fact]
        public void Interpret_DefineFunctionAndCall_ReturnsExpectedResult()
        {
            // Act
            _interpreter.Process("(define (add x y) (+ x y))");
            string result = _interpreter.Process("(add 3 4)");

            // Assert
            Assert.Equal("7", result);
        }
    }
}