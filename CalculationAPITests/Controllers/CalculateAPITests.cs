using CalculationAPI.Interface;
using CalculationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CalculationAPI.Controllers.Tests
{
    [TestClass]
    public class CalculateAPITests
    {
        private Mock<IOperationService<decimal>>? _mockService;
        private Mock<ILogger<CalculateAPI>>? _mockLogger;
        private CalculateAPI? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IOperationService<decimal>>();
            _mockLogger = new Mock<ILogger<CalculateAPI>>();
            _controller = new CalculateAPI(_mockLogger.Object, _mockService.Object);
        }

        [TestMethod]
        public async Task Calculate_withSuccessResultTest()
        {
            var request = new CalculateRequest<decimal>
            {
                type = OperationType.Add,
                FirstOperand = 10,
                SecondOperand = 5
            };

            _mockService!
                .Setup(s => s.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand))
                .ReturnsAsync(15m);

            var result = await _controller!.Calculate(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = result as OkObjectResult;
            var response = ok!.Value as CalculateResponse;

            Assert.IsTrue(response!.Success);
            
        }

        [TestMethod]
        public async Task Calculate_withBadRequestTest()
        {
            var request = new CalculateRequest<decimal>
            {
                type = OperationType.Substract,
                FirstOperand = 20,
                SecondOperand = 10
            };

            _mockService!
                .Setup(s => s.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand))
                .ThrowsAsync(new InvalidOperationException("Invalid operation"));

            var result = await _controller!.Calculate(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var bad = result as BadRequestObjectResult;
            var response = bad!.Value as CalculateResponse;

            Assert.IsFalse(response!.Success);
            
        }

        [TestMethod]
        public async Task Calculate_withDivideByZeroTest()
        {
            var request = new CalculateRequest<decimal>
            {
                type = OperationType.Devide,
                FirstOperand = 10,
                SecondOperand = 0
            };

            _mockService!
                .Setup(s => s.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand))
                .ThrowsAsync(new DivideByZeroException("Cannot divide by zero"));

            var result = await _controller!.Calculate(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var bad = result as BadRequestObjectResult;
            var response = bad!.Value as CalculateResponse;

            Assert.IsFalse(response!.Success);
        }

        [TestMethod]
        public async Task Calculate_withUnsupportedOperationTest()
        {
            var request = new CalculateRequest<decimal>
            {
                type = (OperationType)999, // invalid enum value
                FirstOperand = 1,
                SecondOperand = 1
            };

            _mockService!
                .Setup(s => s.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand))
                .ThrowsAsync(new NotSupportedException("Operation not supported"));

            var result = await _controller!.Calculate(request);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var bad = result as BadRequestObjectResult;
            var response = bad!.Value as CalculateResponse;

            Assert.IsFalse(response!.Success);
        }

        [TestMethod]
        public async Task Calculate_withNullLoggerStillWorksTest()
        {
            _controller = new CalculateAPI(null!, _mockService!.Object);

            var request = new CalculateRequest<decimal>
            {
                type = OperationType.Multiply,
                FirstOperand = 3,
                SecondOperand = 3
            };

            _mockService
                .Setup(s => s.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand))
                .ReturnsAsync(9m);

            var result = await _controller.Calculate(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var ok = result as OkObjectResult;
            var response = ok!.Value as CalculateResponse;

            Assert.IsTrue(response!.Success);
        }
    }
}