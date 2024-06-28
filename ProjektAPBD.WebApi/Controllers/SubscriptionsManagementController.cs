using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.SubscriptionsManagement;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SubscriptionsManagementController : Controller
    {
        private readonly ISubscriptionsManagementRepository _repository;

        public SubscriptionsManagementController(ISubscriptionsManagementRepository repository) 
        { 
            _repository = repository; 
        }

        [HttpPost("Buy/{idProduct}")]
        public async Task<IActionResult> BuySubscription([FromRoute] int idProduct, [FromBody] BuySubscriptionDTO subscriptionDTO)
        {
            var result = await _repository.BuySubscriptionAsync(idProduct, subscriptionDTO);

            return result < 1 ? BadRequest("Can not buy this subscription") : Ok();
        }

        [HttpPost("Pay/{idSubscription}")]
        public async Task<IActionResult> PayForSubscription([FromRoute] int idSubscription, decimal value)
        {
            var result = await _repository.PayForSubscriptionAsync(idSubscription, value);

            return result < 1 ? BadRequest("Can not pay for this subsciption") : Ok();
        }
    }
}
