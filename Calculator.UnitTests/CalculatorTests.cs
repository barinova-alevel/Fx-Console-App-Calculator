using Calculator.BL;

namespace Calculator.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("5+2.5*2", 10, 
            TestName = "EvaluateExpression_InitTest",
            Description = "Check calculation of simple math expression.")]

        [TestCase("125+16", 141,
            TestName = "EvaluateExpression_AdditionOperator",
            Description = "Check calculation of an expresiion with + operator.")]

        [TestCase("5007-7", 5000,
            TestName = "EvaluateExpression_SubtractionOperator",
            Description = "Check calculation of an expresiion with - operator.")]

        [TestCase("80010*3", 240030,
            TestName = "EvaluateExpression_MultiplicationOperator",
            Description = "Check calculation of an expresiion with * operator..")]

        [TestCase("999000/10", 99900,
            TestName = "EvaluateExpression_DivisionOperator",
            Description = "Check calculation of an expresiion with / operator.")]

        [TestCase("6-100+50*2/25", -90,
            TestName = "EvaluateExpression_ExpressionWithAllOperators",
            Description = "Check calculation of an expression that includes +-*/.")]

        [TestCase("20.754+0.246", 21,
            TestName = "EvaluateExpression_ExpressionWithDecimalNumbers",
            Description = "Check calculation of an expression where numbers are decimal.")]

        [TestCase("5-20005-0.9", -20000.9,
            TestName = "EvaluateExpression_ExpressionWithNegativeNumbers",
            Description = "Check calculation of an expression with negative numbers.")]

        [TestCase("-5+2.5*2", 0, 
            TestName = "EvaluateExpression_ExpressionStartsFromNegativeNumber",
            Description = "Check calculation of an expression when the first number is negative.")]

        [TestCase("+70/10", 7,
            TestName = "EvaluateExpression_ExpressionStartsFromNumberWithPlus",
            Description = "Check calculation of an expression when the first number is positive.")]

        [TestCase("999999999999999.9-0.3", 999999999999999.6,
            TestName = "EvaluateExpression_ExpressionWithBigNumber",
            Description = "Check calculation of an expression with big number.")]

        //[TestCase("-5+2.5", 0,
        //    TestName = "EvaluateExpression_ExpressionWithConsecutiveOperators",
        //    Description = "Check calculation of an expression with consecutive operators.")]

        //[TestCase("-5+2.5*2", 0,
        //    TestName = "EvaluateExpression_Negative_WrongFormatExpression",
        //    Description = "Check handling of providing not valid expression.")]

        //[TestCase("2+*3", 0,
        //    TestName = "EvaluateExpression_Negative_MalformedExpression",
        //    Description = "Check handling of providing malformed expression.")]

        //[TestCase(" 2+3 ", 0,
        //    TestName = "EvaluateExpression_Negative_Leading/Trailing Whitespace",
        //    Description = "Check handling of expression with whitespace.")]

        //[TestCase("-5+2.5*2", 0,
        //    TestName = "EvaluateExpression_Negative_AttemptToDevideByZero",
        //    Description = "Check handling of attempt to devide by zero.")]

        //[TestCase("-5+2.5*2", 0,
        //    TestName = "EvaluateExpression_Negative_EmptyExpression",
        //    Description = "Check handling of empty expression.")]

        //[TestCase("-5+2.5*2", 0,
        //    TestName = "EvaluateExpression_Negative_NullPassing",
        //    Description = "Check handling of null passing.")]

        public void EvaluateExpression(string expression, double expectedResult)
        {
            // Arrange
            CalculatorLogic cl = new CalculatorLogic();
            double actualResult;

            // Act
            actualResult = cl.EvaluateExpression(expression);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
            Console.WriteLine(TestContext.CurrentContext.Test.Properties.Get("Description"));
            Console.WriteLine($"Actual result: {actualResult}, Expected result: {expectedResult}");
        }
    }
}