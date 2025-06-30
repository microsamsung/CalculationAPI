using System.Numerics;

namespace CalculationAPI.Interface
{
    public interface IOperationStrategy<T> where T : INumber<T>
    {
        Task<T> CalculateAsync(T firstOperand, T secondOperand);
    }
}
