using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.ClientsManagement;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "admin")]
    public class ClientsManagementController : Controller
    {
        private readonly IClientsManagementRepository _repository;

        public ClientsManagementController(IClientsManagementRepository repository) 
        {
            _repository = repository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddClient([FromBody] AddClientDTO clientDTO)
        {
            var result = await _repository.AddClientAsync(clientDTO);

            return result < 1 ? BadRequest("Can not add client") : Ok();
        }

        [HttpPatch("Update/{idClient}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int idClient, [FromBody]UpdateClientDTO clientDTO)
        {
            var result = await _repository.UpdateClientAsync(idClient, clientDTO);

            return result < 1 ? BadRequest("Can not update client") : Ok();
        }

        [HttpDelete("Delete/{idPerson}")]
        public async Task<IActionResult> RemovePhysicalPerson([FromRoute] int idPerson)
        {
            var result = await _repository.RemovePhysicalPersonAsync(idPerson);

            return result < 1 ? BadRequest("Can not delete given person") : Ok();
        }
    }
}
