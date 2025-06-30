using CalculationAPI.Interface;
using CalculationAPI.Model;
using CalculationAPI.Operations;
using System.Numerics;

namespace CalculationAPI.Factory
{
    public class OperationFactory<T> : IOperationFactory<T> where T : INumber<T>
    {
        private readonly IDictionary<OperationType, IOperationStrategy<T>> _strategies;

        public OperationFactory(
            Addition<T> addition,
            Substraction<T> substraction,
            Multiplication<T> multiplication,
            Division<T> division)
        {
            _strategies = new Dictionary<OperationType, IOperationStrategy<T>>
            {
                { OperationType.Add, addition },
                { OperationType.Substract, substraction },
                { OperationType.Multiply, multiplication },
                { OperationType.Devide, division }
            };
        }

        public IOperationStrategy<T> GetOperationStrategy(OperationType type)
        {
            if (_strategies.TryGetValue(type, out var strategy))
                return strategy;

            throw new NotSupportedException($"Operation '{type}' is not supported.");
        }
    }
}
