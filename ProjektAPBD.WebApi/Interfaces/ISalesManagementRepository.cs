using ProjektAPBD.WebApi.DTOs.SalesManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface ISalesManagementRepository
    {
        Task<int> AddSaleAsync(int idProduct, AddSaleDTO saleDTO, CancellationToken cancellationToken = default);
        Task<int> PayForSaleAsync(int idContract, decimal value, CancellationToken cancellationToken = default);
    }
}
