using CalculationAPI.Interface;
using CalculationAPI.Model;
using System.Numerics;

namespace CalculationAPI.Service
{
    public class OperationService<T> : IOperationService<T> where T : INumber<T>
    {
        private readonly IOperationFactory<T> _factory;

        public OperationService(IOperationFactory<T> factory)
        {
            _factory = factory;
        }

        public async Task<T> CalculateAsync(OperationType type, T first, T second)
        {
            var strategy = _factory.GetOperationStrategy(type);
            return await strategy.CalculateAsync(first, second);
        }
    }
}
