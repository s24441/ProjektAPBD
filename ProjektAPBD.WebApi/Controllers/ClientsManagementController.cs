using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.DTOs.ClientsManagement;
using ProjektAPBD.WebApi.Interfaces;
using System.Net.Sockets;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var result = 0;

            try {
                result = await _repository.AddClientAsync(clientDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (result < 1)
                return BadRequest("Can not add client");

            return Ok();
        }

        [HttpPatch("Update/{idClient}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int idClient, [FromBody]UpdateClientDTO clientDTO)
        {
            var result = 0;

            try {
                result = await _repository.UpdateClientAsync(idClient, clientDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (result < 1)
                return BadRequest("Can not update client");

            return Ok();
        }

        [HttpDelete("Delete/{idPerson}")]
        public async Task<IActionResult> RemovePhysicalPerson([FromRoute] int idPerson)
        {
            var result = 0;

            try {
                result = await _repository.RemovePhysicalPersonAsync(idPerson);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (result < 1)
                return BadRequest("Can not delete given person");

            return Ok();
        }
    }
}
