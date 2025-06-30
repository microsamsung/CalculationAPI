using CalculationAPI.Interface;
using CalculationAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace CalculationAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CalculateAPI : ControllerBase
    {
        private readonly ILogger<CalculateAPI>? _logger;
        private readonly IOperationService<decimal> _service;

        public CalculateAPI(ILogger<CalculateAPI>? logger, IOperationService<decimal> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Calculate([FromQuery] CalculateRequest<decimal> request)
        {
            try
            {
                var result = await _service.CalculateAsync(request.type, request.FirstOperand, request.SecondOperand);
                return Ok(new CalculateResponse { Success = true, Data = new { result = result } });
            }
            catch (Exception ex)
            {
                _logger!.LogError(ex, "Calculation Operation got failed");
                return BadRequest(new CalculateResponse { Success = false, Data = new { error = ex.Message } });
            }
        }
    }
}
