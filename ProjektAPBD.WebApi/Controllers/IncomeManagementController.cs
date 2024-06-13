using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeManagementController : Controller
    {
        private readonly IIncomeManagemenRepository _repository;

        public IncomeManagementController(IIncomeManagemenRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Income")]
        public async Task<IActionResult> GetActualIncome(int? idProduct = default, string? currency = default)
        {
            decimal result = 0;

            try {
                result = await _repository.GetActualIncomeAsync(idProduct, currency);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpGet("IncomePrognosis")]
        public async Task<IActionResult> GetIncomePrognosis(int? idProduct = default, string? currency = default)
        {
            decimal result = 0;

            try
            {
                result = await _repository.GetActualIncomeAsync(idProduct, currency);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
    }
}
