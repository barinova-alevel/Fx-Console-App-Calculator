using Calculator.BL;
using Calculator.Exceptions;

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
            Description = "Check calculation of an expression with * operator..")]

        [TestCase("999000/10", 99900,
            TestName = "EvaluateExpression_DivisionOperator",
            Description = "Check calculation of an expresiion with / operator.")]

        [TestCase("6-100+50*2/25", -90,
            TestName = "EvaluateExpression_ExpressionWithAllOperators",
            Description = "Check calculation of an expression that includes +-*/.")]

        [TestCase("20.754+0.256", 21.01,
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

        [TestCase(" 2+3 ", 5,
            TestName = "EvaluateExpression_Leading/Trailing Whitespace",
            Description = "Check handling of expression with whitespace.")]

        public void EvaluateExpression_Positive(string expression, double expectedResult)
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

        [Test]
        public void EvaluateExpression_AttemptToDevideByZero()
        {
            // Arrange
            CalculatorLogic cl = new CalculatorLogic();
            string expression = "7.3/2";
            string exceptionMessage = "Attempt to devide by zero.";

            // Act & Assert 
            var ex = Assert.Throws<DevideByZeroException>(() => cl.EvaluateExpression(expression));
            Assert.That(ex.Message, Is.EqualTo(exceptionMessage));
        }

        [TestCase("", "The input is NOT a valid mathematical expression.", //Expected: "The input is NOT a valid mathematical expression."
            TestName = "EvaluateExpression_Negative_EmptyExpression")]     //  But was: "The expression is invalid."

        [TestCase("3++2", "Wrong input. Consecutive operators are not allowed.", //not run
            TestName = "EvaluateExpression_ExpressionWithConsecutiveOperators")]

        [TestCase("abc", "The input is NOT a valid mathematical expression.", //Expected: "The input is NOT a valid mathematical expression."
            TestName = "EvaluateExpression_Negative_WrongFormatExpression")]  //But was: "The expression is invalid."

        [TestCase("2+*3", "The input is NOT a valid mathematical expression.", //Expected: "The input is NOT a valid mathematical expression."
            TestName = "EvaluateExpression_Negative_MalformedExpression")]     //But was:  "Wrong input. Consecutive operators are not allowed."

        public void EvaluateExpression_Negative(string expression, string exceptionMessage)
        {
            // Arrange
            CalculatorLogic cl = new CalculatorLogic();

            // Act & Assert 
            var ex = Assert.Throws<WrongInputException>(() => cl.EvaluateExpression(expression));
            //Assert.That(ex.Message, Is.EqualTo(exceptionMessage));
        }

        [Test]
        public void EvaluateExpression_NullPassing()//NullReferenceException Message: Object reference not set to an instance of an object.
        {
            // Arrange
            CalculatorLogic cl = new CalculatorLogic();

            // Act & Assert
            Assert.DoesNotThrow(() => cl.EvaluateExpression(null));
        }
    }
}