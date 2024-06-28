using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.SalesManagement;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SalesManagementController : Controller
    {
        private readonly ISalesManagementRepository _repository;

        public SalesManagementController(ISalesManagementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Add/{idProduct}")]
        public async Task<IActionResult> AddSale([FromRoute] int idProduct, [FromBody] AddSaleDTO saleDTO)
        {
            var result = await _repository.AddSaleAsync(idProduct, saleDTO);

            return result < 1 ? BadRequest("Can not add sale") : Ok();
        }

        [HttpPost("Pay/{idSale}")]
        public async Task<IActionResult> PayForSale([FromRoute] int idSale, decimal value)
        {
            var result = await _repository.PayForSaleAsync(idSale, value);

            return result < 1 ? BadRequest("Can not pay for sale") : Ok();
        }
    }
}
