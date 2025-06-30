using CalculationAPI.Model;
using System.Numerics;

namespace CalculationAPI.Interface
{
    public interface IOperationService<T> where T : INumber<T>
    {
        Task<T> CalculateAsync(OperationType type, T first , T second);
    }
}
