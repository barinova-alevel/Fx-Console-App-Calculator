using Calculator.BL;

namespace Calculator.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("5+2.5*2", 10, 
            TestName = "EvaluateExpression_InitTest",
            Description = "Check calculation of simple math expression.")]

        [TestCase("-5+2.5*2", 0, 
            TestName = "EvaluateExpression_ExpressionStartsFromNegativeNumber",
            Description = "Check calculation of an expression when the first number is negative.")]

        public void EvaluateExpression(string expression, double expectedResult)
        {
            // Arrange
            CalculatorLogic cl = new CalculatorLogic();
            double actualResult;

            // Act
            actualResult = cl.EvaluateExpression(expression);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
            Console.WriteLine($"Actual result: {actualResult}, Expected result: {expectedResult}");
        }
    }
}