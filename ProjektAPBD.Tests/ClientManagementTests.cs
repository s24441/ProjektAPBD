using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Tests.DbSeed;
using ProjektAPBD.WebApi.DTOs.ClientsManagement;
using ProjektAPBD.WebApi.Exceptions;
using ProjektAPBD.WebApi.Exceptions.ClientManagement;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Repositories;
using Shouldly;

namespace ProjektAPBD.Tests
{
    public class ClientManagementTests
    {
        private ManagementDbContext _context;

        private IClientsManagementRepository _clientsRepository;

        public ClientManagementTests() 
        {
            _context = new ManagementDbContext(new DbContextOptionsBuilder<ManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options)
            .TestSeed();
            _clientsRepository = new ClientsManagementRepository(_context);
        }

        [Fact]
        public async Task AddClient_Should_Throw_ClientValidationException_When_AddClientDTO_Has_No_PhysicalPerson_Nor_Company_Data()
        {
            var command = new AddClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890"
            };

            await Should
                .ThrowAsync<ClientValidationException>(_clientsRepository.AddClientAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task AddClient_Should_Throw_PeselValidationException_When_Person_Pesel_Already_Exists_In_The_Database()
        {
            var command = new AddClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890",
                PhysicalPerson = new()
                {
                    FirstName = "Maria",
                    LastName = "Awaria",
                    Pesel = "89050729992"
                }
            };

            await Should
                .ThrowAsync<PeselValidationException>(_clientsRepository.AddClientAsync(command, CancellationToken.None));
        }

        [Fact]
        public async Task AddClient_Should_Add_New_PhysicalPerson_Into_Database()
        {
            var command = new AddClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890",
                PhysicalPerson = new()
                {
                    FirstName = "Maria",
                    LastName = "Awaria",
                    Pesel = "95050729992"
                }
            };

            var result = await _clientsRepository.AddClientAsync(command, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task AddClient_Should_Add_New_Company_Into_Database()
        {
            var command = new AddClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890",
                Company = new()
                {
                    Name = "TestComp",
                    KrsNumber = "95050729992"
                }
            };

            var result = await _clientsRepository.AddClientAsync(command, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task UpdateClient_Should_Throw_ClientNotExistsException_When_Given_IdClient_Not_Exists_In_The_Database()
        {
            var command = new UpdateClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890"
            };

            var id = 0;

            await Should
                .ThrowAsync<ClientNotExistsException>(_clientsRepository.UpdateClientAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateClient_Should_Update_Address_Email_PhoneNumber_FirstName_Lastname_For_PhysicalPerson()
        {
            var command = new UpdateClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890",
                PhysicalPerson = new()
                {
                    FirstName = "Maria",
                    LastName = "Awaria"
                }
            };

            var id = 1;

            var result = await _clientsRepository.UpdateClientAsync(id, command, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task UpdateClient_Should_Update_Address_Email_PhoneNumber_Name_For_Company()
        {
            var command = new UpdateClientDTO
            {
                Address = "Test",
                Email = "test@test.pl",
                PhoneNumber = "1234567890",
                Comapny = new()
                {
                    Name = "TestCorp"
                }
            };

            var id = 1;

            var result = await _clientsRepository.UpdateClientAsync(id, command, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task RemovePhysicalPerson_Should_Throw_PersonNotExistsException_When_Given_Person_Id_Not_Exists_In_The_Database()
        {
            var id = 0;

            await Should
                .ThrowAsync<PersonNotExistsException>(_clientsRepository.RemovePhysicalPersonAsync(id, CancellationToken.None));
        }

        [Fact]
        public async Task RemovePhysicalPerson_Should_Replace_Address_Email_PhoneNumber_FirstName_Lastname_With_SoftDeleteMessage()
        {
            var id = 4;

            var changed = await _clientsRepository.RemovePhysicalPersonAsync(id, CancellationToken.None);

            var client = await _context.PersonClients.FirstOrDefaultAsync(p => p.IdClient == id);

            var result = changed == 1 && 
                client?.FirstName == Message.SoftDelete &&
                client?.LastName == Message.SoftDelete &&
                client?.Address == Message.SoftDelete &&
                client?.Email == Message.SoftDelete &&
                client?.Phone == Message.SoftDelete;

            result.ShouldBeEquivalentTo(true);
        }
    }
}