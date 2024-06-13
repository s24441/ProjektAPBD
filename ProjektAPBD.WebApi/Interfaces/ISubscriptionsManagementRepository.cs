using ProjektAPBD.WebApi.DTOs.SubscriptionsManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface ISubscriptionsManagementRepository
    {
        Task<bool> BuySubscriptionAsync(int idProduct, BuySubscriptionDTO subscriptionDTO);
        Task<bool> PayForSubscriptionAsync(int idContract, decimal value);
    }
}
