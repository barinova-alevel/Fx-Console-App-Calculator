using Calculator.BL;
using Serilog;

namespace Calculator.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("5+2.5*2", 10)]
        public void EvaluateExpression_InitTest(string expression, double expectedResult)
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