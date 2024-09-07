using Calculator.BL;
using NUnit.Framework;

namespace Calculator.UnitTests
{
    [TestFixture]
    public class PrioritiesOfOperationsTests
    {
        [TestCase("+", 1)]
        [TestCase("-", 1)]
        [TestCase("*", 2)]
        [TestCase("/", 2)]
        [TestCase("%", 2)]
        public void GetPrecedence_ValidOperators(string op, int expectedResult)
        {
            // Arrange
            PrioritiesOfOperations prioritiesOfOperations = new PrioritiesOfOperations();
            int actualResult;

            // Act
            actualResult = prioritiesOfOperations.GetPrecedence(op);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetPrecedence_InvalidOperator_ReturnsDefault()
        {
            // Arrange
            PrioritiesOfOperations prioritiesOfOperations = new PrioritiesOfOperations();
            int actualResult;

            // Act
            actualResult = prioritiesOfOperations.GetPrecedence("^");

            // Assert
            Assert.That(actualResult, Is.EqualTo(0));
        }

        [Test]
        public void GetPrecedence_EmptyOperator()
        {
            // Arrange
            PrioritiesOfOperations po = new PrioritiesOfOperations();

            // Act & Assert
            Assert.That(() => po.GetPrecedence(""), Is.EqualTo(0));
        }

        [Test]
        public void GetPrecedence_MultipleCharacters()
        {
            // Arrange
            PrioritiesOfOperations po = new PrioritiesOfOperations();

            // Act & Assert
            Assert.That(() => po.GetPrecedence("**"), Is.EqualTo(0));
        }

        [Test]
        public void GetPrecedence_NullPassing()
        {
            // Arrange
            PrioritiesOfOperations po = new PrioritiesOfOperations();

            // Act & Assert
            Assert.DoesNotThrow(() => po.GetPrecedence(null));
        }
    }
}
