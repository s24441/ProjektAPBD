using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.DTOs.ClientsManagement;
using ProjektAPBD.WebApi.Exceptions;
using ProjektAPBD.WebApi.Exceptions.ClientManagement;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Repositories
{
    public class ClientsManagementRepository : IClientsManagementRepository
    {
        private readonly ManagementDbContext _context;

        public ClientsManagementRepository(ManagementDbContext context) 
        {
            _context = context;
        }

        public async Task<int> AddClientAsync(AddClientDTO clientDTO, CancellationToken cancellationToken = default)
        {
            if (!(clientDTO.PhysicalPerson != default ^ clientDTO.Company != default))
                throw new ClientValidationException("Client has to be either Physical Person or Company");

            if (clientDTO.PhysicalPerson != default)
            {
                if (await _context.PersonClients.AnyAsync(p => p.Pesel == clientDTO.PhysicalPerson.Pesel, cancellationToken))
                    throw new PeselValidationException("The given pesel number already exists in the database");

                var person = new PhysicalPerson
                {
                    FirstName = clientDTO.PhysicalPerson.FirstName,
                    LastName = clientDTO.PhysicalPerson.LastName,
                    Pesel = clientDTO.PhysicalPerson.Pesel,
                    Address = clientDTO.Address,
                    Email = clientDTO.Email,
                    Phone = clientDTO.PhoneNumber
                };

                _context.PersonClients.Add(person);
            }
            else if (clientDTO.Company != default) {
                var company = new Company 
                { 
                    Name = clientDTO.Company.Name,
                    KrsNumber = clientDTO.Company.KrsNumber,
                    Address = clientDTO.Address,
                    Email = clientDTO.Email,
                    Phone = clientDTO.PhoneNumber
                };

                _context.CompanyClients.Add(company);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<int> UpdateClientAsync(int idClient, UpdateClientDTO clientDTO, CancellationToken cancellationToken = default)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == idClient, cancellationToken);

            if (client == default)
                throw new ClientNotExistsException("The given client does not exists in the database");

            if (clientDTO.Address != default) client.Address = clientDTO.Address;
            if (clientDTO.Email != default) client.Email = clientDTO.Email;
            if (clientDTO.PhoneNumber != default) client.Phone = clientDTO.PhoneNumber;

            if (client is PhysicalPerson person) {
                if (clientDTO.PhysicalPerson?.FirstName != default) person.FirstName = clientDTO.PhysicalPerson.FirstName;
                if (clientDTO.PhysicalPerson?.LastName != default) person.LastName = clientDTO.PhysicalPerson.LastName;

                _context.PersonClients.Update(person);
            }
            else if (client is Company company) {
                if (clientDTO.Comapny?.Name != default) company.Name = clientDTO.Comapny.Name;

                _context.CompanyClients.Update(company);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<int> RemovePhysicalPersonAsync(int idPerson, CancellationToken cancellationToken = default)
        {
            var person = await _context.PersonClients.FirstOrDefaultAsync(p => p.IdClient == idPerson);

            if (person == default)
                throw new PersonNotExistsException("The given person does not exists in the database");

            person.FirstName = Message.SoftDelete;
            person.LastName = Message.SoftDelete;
            person.Address = Message.SoftDelete;
            person.Email = Message.SoftDelete;
            person.Phone = Message.SoftDelete;

            _context.PersonClients.Update(person);

            var result = await _context.SaveChangesAsync(); 
            
            return result;
        }
    }
}
