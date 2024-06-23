using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.SalesManagement;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var result = 0;

            try {
                result = await _repository.AddSaleAsync(idProduct, saleDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (result < 1)
                return BadRequest("Can not add sale");

            return Ok();
        }

        [HttpPost("Pay/{idSale}")]
        public async Task<IActionResult> PayForSale([FromRoute] int idSale, decimal value)
        {
            var result = 0;

            try {
                result = await _repository.PayForSaleAsync(idSale, value);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (result < 1)
                return BadRequest("Can not pay for sale");

            return Ok();
        }
    }
}
