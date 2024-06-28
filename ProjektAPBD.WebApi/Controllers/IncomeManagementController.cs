using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
            decimal result = await _repository.GetActualIncomeAsync(idProduct, currency);

            return Ok(result);
        }

        [HttpGet("IncomePrognosis")]
        public async Task<IActionResult> GetIncomePrognosis(int? idProduct = default, string? currency = default)
        {
            decimal result = await _repository.GetActualIncomeAsync(idProduct, currency);

            return Ok(result);
        }
    }
}
