using System.ComponentModel.DataAnnotations;

namespace CalculationAPI.Model
{
    public class CalculateRequest<T>
    {
        [Required]
        public OperationType type { get; set; }
       
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="First Operand should be greater than 0")]
        public T? FirstOperand { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Second Operand should be greater than 0")]  
        public T? SecondOperand { get; set; }
    }
}
