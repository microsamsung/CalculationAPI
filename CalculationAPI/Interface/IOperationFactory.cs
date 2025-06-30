using CalculationAPI.Model;
using System.Numerics;

namespace CalculationAPI.Interface
{
    public interface IOperationFactory<T> where T : INumber<T>
    {
        IOperationStrategy<T> GetOperationStrategy(OperationType type);
    }
}
