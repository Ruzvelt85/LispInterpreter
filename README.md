# A simple LISP interpreter


This simple solution correctly parses LISP expressions. 

LISP expressions are typically written in the form of nested lists, such as `(operator operand1 operand2 ...)`.

It can parse and evaluate:

**Basic Arithmetic Operations**. It supports basic arithmetic operations: `+`, `-`, `*`, `/`. These works with integers and handles 2 arguments, e.g., `(+ 1 2)`.

**Variables**. The solution allows defining variables and using them in expressions. E.g., `(define x 10)` and then `(+ x 5)`.

**Conditionals**. It supports the `if` statement for basic conditional logic.

**Functions**. The solution allows defining and calling simple functions. E.g., `(define (square x) (* x x))` and then `(square 3)`.


The solution consists of 3 projects: ConsoleApplication, BusinessLogic and Tests.

###  Build and Run

To build and run the solution, please perform the following steps:

1) Open the solution in MS Visual Studio
2) Execute command Build - Build solution
3) To run API press F5
