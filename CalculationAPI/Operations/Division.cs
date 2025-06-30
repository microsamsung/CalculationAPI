using CalculationAPI.Interface;
using System.Numerics;
namespace CalculationAPI.Operations
{
    public class Division<T> : IOperationStrategy<T> where T : INumber<T>
    {
        public Task<T> CalculateAsync(T firstOperand, T secondOperand)
        {
            var result = firstOperand / secondOperand;
            return Task.FromResult(result);
        }
    }
}
