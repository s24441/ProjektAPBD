using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.DTOs.SubscriptionsManagement;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Repositories
{
    public class SubscriptionsManagementRepository : ISubscriptionsManagementRepository
    {
        private readonly ManagementDbContext _context;

        public SubscriptionsManagementRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BuySubscriptionAsync(int idProduct, BuySubscriptionDTO subscriptionDTO)
        {
            var paymentDay = DateTime.Now;

            if (subscriptionDTO.Price <= 0)
                throw new Exception("Product price should be greater than 0");

            if (subscriptionDTO.RenewalMonthsPeriod < 1 || subscriptionDTO.RenewalMonthsPeriod > 24)
                throw new Exception("The given subscription renewal months period should be between 1 and 24 days");

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == subscriptionDTO.IdClient);

            if (client == default)
                throw new Exception("The given client does not exists in the database");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.IdSoftwareProduct == idProduct);

            if (product == default)
                throw new Exception("The given product does not exists in the database");

            var discount = await _context.Discounts
                .Where(c => c.DateFrom <= paymentDay && paymentDay <= c.DateTo)
                .MaxAsync(c => (int?)c.PercentageValue) ?? 0;

            var isAlreadyOurClient = await _context.Payments.AnyAsync(p => p.IdClient == subscriptionDTO.IdClient);
            if (isAlreadyOurClient)
                discount = discount <= 95 ? discount + 5 : 100;

            var price = subscriptionDTO.Price * (1M - discount / 100);

            var newSubscription = new Subscription()
            {
                IdClient = subscriptionDTO.IdClient,
                IdSoftwareProduct = idProduct,
                Name = subscriptionDTO.Name,
                EndTime = paymentDay.AddMonths(subscriptionDTO.RenewalMonthsPeriod),
                RenewalPeriod = subscriptionDTO.RenewalMonthsPeriod,
                Price = subscriptionDTO.Price
            };

            newSubscription.Payments.Add(new() { Date = paymentDay, Value = price });

            _context.Subscriptions.Add(newSubscription);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> PayForSubscriptionAsync(int idContract, decimal value)
        {
            var paymentDay = DateTime.Now;

            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.IdContract == idContract);

            if (subscription == default)
                throw new Exception("The given subscription does not exists in the database");

            if (value != subscription.Price)
                throw new Exception("The given payment is not equal subscription price");

            if (paymentDay > subscription.EndTime.AddMonths(subscription.RenewalPeriod))
                throw new Exception("You cant pay for inactive subscription");

            var isAlreadyPaid = await _context.Payments
                .AnyAsync(p => 
                    p.IdContract == subscription.IdContract 
                    && 
                    p.Date >= subscription.EndTime.AddMonths(-subscription.RenewalPeriod) 
                    && 
                    p.Date <= subscription.EndTime);

            if (isAlreadyPaid)
                throw new Exception("The given subscription is already paid");

            var discount = 0;

            var isAlreadyOurClient = await _context.Payments.AnyAsync(p => p.IdClient == subscription.IdClient);
            if (isAlreadyOurClient)
                discount = 5;

            var price = value * (1M - discount / 100);

            var newPayment = new Payment {
                Date = paymentDay,
                Value = price
            };

            subscription.EndTime = subscription.EndTime.AddMonths(subscription.RenewalPeriod);
            subscription.Payments.Add(newPayment);

            _context.Subscriptions.Update(subscription);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
