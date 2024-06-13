using ProjektAPBD.WebApi.DTOs.SalesManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface ISalesManagementRepository
    {
        Task<bool> AddSaleAsync(int idProduct, AddSaleDTO saleDTO);
        Task<bool> PayForSaleAsync(int idContract, decimal value);
    }
}
