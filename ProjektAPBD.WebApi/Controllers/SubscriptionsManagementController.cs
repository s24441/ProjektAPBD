﻿using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.SubscriptionsManagement;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsManagementController : Controller
    {
        private readonly ISubscriptionsManagementRepository _repository;

        public SubscriptionsManagementController(ISubscriptionsManagementRepository repository) 
        { 
            _repository = repository; 
        }

        [HttpPost("{idProduct}")]
        public async Task<IActionResult> BuySubscription([FromRoute] int idProduct, [FromBody] BuySubscriptionDTO subscriptionDTO)
        {
            bool result = false;

            try {
                result = await _repository.BuySubscriptionAsync(idProduct, subscriptionDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not buy this subscription");

            return Ok();
        }

        [HttpPost("{idSubscription}")]
        public async Task<IActionResult> PayForSubscription([FromRoute] int idSubscription, decimal value)
        {
            bool result = false;

            try {
                result = await _repository.PayForSubscriptionAsync(idSubscription, value);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not pay for this subsciption");

            return Ok();
        }
    }
}
