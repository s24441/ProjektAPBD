using ProjektAPBD.WebApi.DTOs.ClientsManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface IClientsManagementRepository
    {
        Task<bool> AddClientAsync(AddClientDTO clientDTO);
        Task<bool> UpdateClientAsync(int idClient, UpdateClientDTO clientDTO);
        Task<bool> RemovePhysicalPersonAsync(int idPerson);
    }
}
