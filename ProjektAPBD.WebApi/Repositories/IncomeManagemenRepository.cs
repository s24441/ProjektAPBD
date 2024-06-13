using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Interfaces.ApiClients;
using ProjektAPBD.WebApi.Models;

namespace ProjektAPBD.WebApi.Repositories
{
    public class IncomeManagemenRepository : IIncomeManagemenRepository
    {
        private readonly ManagementDbContext _context;
        private readonly IExchangeApiClient _exchangeClient;

        public IncomeManagemenRepository(ManagementDbContext context, IExchangeApiClient exchangeClient)
        {
            _context = context;
            _exchangeClient = exchangeClient;
        }

        public async Task<decimal> GetActualIncomeAsync(int? idProduct = default, string? currency = default)
        {
            var today = DateTime.Now;

            var salesIcome = await _context.Sales
                .Include(s => s.Payments)
                .Where(s =>
                    (idProduct == default || s.IdSoftwareProduct == idProduct)
                    &&
                    s.ExpirationDate.Year == today.Year
                    &&
                    s.Payments.Sum(p => p.Value) == s.Price)
                .SumAsync(s => (decimal?)s.Price) ?? 0M;

            var subscriptionsIncome = await _context.Subscriptions
                .Where(s =>
                    (idProduct == default || s.IdSoftwareProduct == idProduct)
                    &&
                    s.EndTime.AddMonths(-s.RenewalPeriod) <= today && today <= s.EndTime)
                .Join(
                    _context.Payments.Where(p => p.Date.Year == today.Year), 
                    s => s.IdContract, 
                    p => p.IdContract, 
                    (subscription, payment) => payment)
                .SumAsync(p => (decimal?)p.Value) ?? 0;


            var currencyFactor = 1M;

            if (currency != default)
                currencyFactor = await GetMidRateAsync(currency);

            var currencyIncome = (salesIcome + subscriptionsIncome) / currencyFactor;

            return currencyIncome;
        }

        public async Task<decimal> GetIncomePrognosisAsync(int? idProduct = default, string? currency = default)
        {
            var today = DateTime.Now;

            var salesIcome = await _context.Sales
                .Include(s => s.Payments)
                .Where(s => (idProduct == default || s.IdSoftwareProduct == idProduct) && s.ExpirationDate.Year == today.Year)
                .SumAsync(s => (decimal?)s.Price) ?? 0M;

            var subscriptionsIncome = await _context.Subscriptions
                .Include(s => s.Payments)
                .Where(s =>
                    (idProduct == default || s.IdSoftwareProduct == idProduct)
                    &&
                    s.EndTime.AddMonths(-s.RenewalPeriod) <= today && today <= s.EndTime
                    &&
                    (s.Payments.Any(p => p.Date.Year == today.Year) || !s.Payments.Any(p => s.EndTime.AddMonths(-s.RenewalPeriod) <= p.Date && p.Date < new DateTime(today.Year,1,1))))
                .SumAsync(s => (decimal?)s.Price * 0.95M) ?? 0;

            var currencyFactor = 1M;

            if (currency != default)
                currencyFactor = await GetMidRateAsync(currency);

            var currencyIncome = (salesIcome + subscriptionsIncome) / currencyFactor;

            return currencyIncome;
        }

        private async Task<decimal> GetMidRateAsync(string currency)
            => ((decimal?)(await _exchangeClient.GetExchangeRatesAsync(currency))?.Rates.MaxBy(r => r.EffectiveDate)?.Mid ?? throw new Exception("No exchange rates"));
    }
}
