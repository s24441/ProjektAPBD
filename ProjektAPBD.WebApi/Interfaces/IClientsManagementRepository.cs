using ProjektAPBD.WebApi.DTOs.ClientsManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface IClientsManagementRepository
    {
        Task<int> AddClientAsync(AddClientDTO clientDTO, CancellationToken cancellationToken = default);
        Task<int> UpdateClientAsync(int idClient, UpdateClientDTO clientDTO, CancellationToken cancellationToken = default);
        Task<int> RemovePhysicalPersonAsync(int idPerson, CancellationToken cancellationToken = default);
    }
}
