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

        [HttpPost("{idProduct}")]
        public async Task<IActionResult> AddSale([FromRoute] int idProduct, [FromBody] AddSaleDTO saleDTO)
        {
            bool result = false;

            try {
                result = await _repository.AddSaleAsync(idProduct, saleDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not add sale");

            return Ok();
        }

        [HttpPost("{idSale}")]
        public async Task<IActionResult> PayForSale([FromRoute] int idSale, decimal value)
        {
            bool result = false;

            try {
                result = await _repository.PayForSaleAsync(idSale, value);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not pay for sale");

            return Ok();
        }
    }
}
