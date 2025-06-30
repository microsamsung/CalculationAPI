using CalculationAPI.Interface;
using CalculationAPI.Model;

namespace CalculationAPI.Service
{
    public class OperationService : IOperationService<decimal>
    {
        private readonly IOperationFactory<decimal> _factory;

        public OperationService(IOperationFactory<decimal> factory)
        {
            _factory = factory;
        }

        public async Task<decimal> CalculateAsync(OperationType type, decimal first, decimal second)
        {
            var strategy = _factory.GetOperationStrategy(type);
            return await strategy.CalculateAsync(first, second);

        }
    }
}
