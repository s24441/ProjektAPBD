﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] AddClientDTO clientDTO)
        {
            bool result = false;

            try {
                result = await _repository.AddClientAsync(clientDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not add client");

            return Ok();
        }

        [HttpPatch("{idClient}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int idClient, [FromBody]UpdateClientDTO clientDTO)
        {
            bool result = false;

            try {
                result = await _repository.UpdateClientAsync(idClient, clientDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not update client");

            return Ok();
        }

        [HttpDelete("{idPerson}")]
        public async Task<IActionResult> RemovePhysicalPerson([FromRoute] int idPerson)
        {
            bool result = false;

            try {
                result = await _repository.RemovePhysicalPersonAsync(idPerson);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            if (!result)
                return BadRequest("Can not delete given person");

            return Ok();
        }
    }
}
