using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.DTOs.SalesManagement;
using ProjektAPBD.WebApi.Exceptions;
using ProjektAPBD.WebApi.Exceptions.SalesManagement;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Repositories
{
    public class SalesManagementRepository : ISalesManagementRepository
    {
        private readonly ManagementDbContext _context;

        public SalesManagementRepository(ManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddSaleAsync(int idProduct, AddSaleDTO saleDTO, CancellationToken cancellationToken = default)
        {
            saleDTO.CreationDate ??= DateTime.Now;

            if (saleDTO.Price <= 0)
                throw new PriceTooLowException("Product price should be greater than 0");

            if (saleDTO.ExpirationDaysRange < 3 || saleDTO.ExpirationDaysRange > 30)
                throw new InvalidDateException("The given sale expiration date range should be between 3 and 30 days");

            saleDTO.AdditionalSupportYearsAmount ??= 0;

            if (saleDTO.AdditionalSupportYearsAmount < 0 || saleDTO.AdditionalSupportYearsAmount > 3)
                throw new InvalidSupportYearsAmountException("The given product additional support years amount should be between 0 and 3 years");

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == saleDTO.IdClient, cancellationToken);
            
            if (client == default)
                throw new ClientNotExistsException("The given client does not exists in the database");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.IdSoftwareProduct == idProduct, cancellationToken);

            if (product == default)
                throw new ProductNotExistsException("The given product does not exists in the database");

            var discount = await _context.Discounts
                .Where(c => c.DateFrom <= saleDTO.CreationDate && saleDTO.CreationDate <= c.DateTo)
                .MaxAsync(c => (int?)c.PercentageValue, cancellationToken) ?? 0;

            var isAlreadyOurClient = await _context.Payments.AnyAsync(p => p.IdClient == saleDTO.IdClient);
            if (isAlreadyOurClient)
                discount = discount <= 95 ? discount + 5 : 100;

            var price = saleDTO.Price * (1M - discount/100M) + saleDTO.AdditionalSupportYearsAmount.Value * 1000;

            var newSale = new Sale
            {
                IdClient = saleDTO.IdClient,
                CreationDate = saleDTO.CreationDate.Value,
                ExpirationDate = saleDTO.CreationDate.Value.AddDays(saleDTO.ExpirationDaysRange),
                IdSoftwareProduct = idProduct,
                SupportYearsAmount = 1 + saleDTO.AdditionalSupportYearsAmount.Value,
                Price = price
            };
            
            _context.Sales.Add(newSale);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<int> PayForSaleAsync(int idContract, decimal value, CancellationToken cancellationToken = default)
        {
            var paymentDay = DateTime.Now;

            if (value <= 0)
                throw new PaymentValueException("Payment value should be greater than 0");

            var sale = await _context.Sales
                .Where(s => s.IdContract == idContract)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(cancellationToken);

            if (sale == default)
                throw new SaleNotExistsException("The given sale does not exists in the database");

            if (!(sale.CreationDate <= paymentDay && paymentDay <= sale.ExpirationDate))
                throw new InactiveSaleException("You can't pay inactive sale");

            if (sale.Payments.Sum(p => p.Value) + value > sale.Price)
                throw new PaymentValueException("You can't pay more than the price of the sale");

            var newPayment = new Payment
            {
                IdContract = idContract,
                IdClient = sale.IdClient,
                Date = paymentDay,
                Value = value
            };

            _context.Payments.Add(newPayment);

            var result = await _context.SaveChangesAsync(cancellationToken); 
            
            return result;
        }
    }
}
