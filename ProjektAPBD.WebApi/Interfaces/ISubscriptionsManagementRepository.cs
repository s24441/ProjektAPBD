using ProjektAPBD.WebApi.DTOs.SubscriptionsManagement;

namespace ProjektAPBD.WebApi.Interfaces
{
    public interface ISubscriptionsManagementRepository
    {
        Task<int> BuySubscriptionAsync(int idProduct, BuySubscriptionDTO subscriptionDTO, CancellationToken cancellationToken = default);
        Task<int> PayForSubscriptionAsync(int idContract, decimal value, CancellationToken cancellationToken = default);
    }
}
