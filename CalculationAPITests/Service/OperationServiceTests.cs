using CalculationAPI.Interface;
using CalculationAPI.Model;
using CalculationAPI.Service;
using Moq;

namespace CalculationAPI.Tests.Service
{
    [TestClass]
    public class OperationServiceTests
    {
        private Mock<IOperationFactory<decimal>>? _mockFactory;
        private Mock<IOperationStrategy<decimal>>? _mockStrategy;
        private OperationService<decimal>? _service;

        [TestInitialize]
        public void Setup()
        {
            _mockFactory = new Mock<IOperationFactory<decimal>>();
            _mockStrategy = new Mock<IOperationStrategy<decimal>>();
            _service = new OperationService<decimal>(_mockFactory.Object);
        }

        [TestMethod]
        public async Task CalculateAsync_Addition_ReturnsExpectedResult()
        {
            // Arrange
            var first = 10m;
            var second = 5m;
            var expected = 15m;

            _mockFactory!
                .Setup(f => f.GetOperationStrategy(OperationType.Add))
                .Returns(_mockStrategy!.Object);

            _mockStrategy!
                .Setup(s => s.CalculateAsync(first, second))
                .ReturnsAsync(expected);

            // Act
            var result = await _service!.CalculateAsync(OperationType.Add, first, second);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CalculateAsync_ThrowsException_WhenFactoryFails()
        {
            // Arrange
            _mockFactory!
                .Setup(f => f.GetOperationStrategy(It.IsAny<OperationType>()))
                .Throws(new InvalidOperationException("Strategy not found"));

            // Act
            await _service!.CalculateAsync(OperationType.Substract, 10, 5);

            // Assert handled by ExpectedException
        }
    }
}
