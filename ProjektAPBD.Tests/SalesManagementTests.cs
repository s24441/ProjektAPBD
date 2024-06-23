using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Tests.DbSeed;
using ProjektAPBD.WebApi.DTOs.SalesManagement;
using ProjektAPBD.WebApi.Exceptions;
using ProjektAPBD.WebApi.Exceptions.SalesManagement;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Repositories;
using Shouldly;

namespace ProjektAPBD.Tests
{
    [Collection("TestsSequence")]
    public class SalesManagementTests
    {
        private ISalesManagementRepository _salesRepository = new SalesManagementRepository(new ManagementDbContext(new DbContextOptionsBuilder<ManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options)
            .TestSeed());

        [Fact]
        public async Task AddSale_Should_Throw_PriceTooLowException_When_Price_Is_Lower_Than_Zero()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = -1,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            await Should
                .ThrowAsync<PriceTooLowException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_InvalidDateException_When_ExpirationDaysRange_Is_Lower_Than_3()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 2
            };

            await Should
                .ThrowAsync<InvalidDateException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_InvalidDateException_When_ExpirationDaysRange_Is_Higher_Than_30()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 31
            };

            await Should
                .ThrowAsync<InvalidDateException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_InvalidSupportYearsAmountException_When_AdditionalSupportYearsAmount_Is_Lower_Than_Zero()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = -1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            await Should
                .ThrowAsync<InvalidSupportYearsAmountException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_InvalidSupportYearsAmountException_When_AdditionalSupportYearsAmount_Is_Higher_Than_3()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = 4,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            await Should
                .ThrowAsync<InvalidSupportYearsAmountException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_ClientNotExistsException_When_Given_IdClient_Not_Exists_In_The_Database()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 0,
                Price = 1000,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            await Should
                .ThrowAsync<ClientNotExistsException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Throw_ProductNotExistsException_When_Given_IdProduct_Not_Exists_In_The_Database()
        {
            var id = 0;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            await Should
                .ThrowAsync<ProductNotExistsException>(_salesRepository.AddSaleAsync(id, command, CancellationToken.None));
        }

        [Fact]
        public async Task AddSale_Should_Add_New_Sale_Into_Database()
        {
            var id = 1;

            var command = new AddSaleDTO
            {
                IdClient = 1,
                Price = 1000,
                AdditionalSupportYearsAmount = 1,
                CreationDate = DateTime.Now,
                ExpirationDaysRange = 5
            };

            var result = await _salesRepository.AddSaleAsync(id, command, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task PayForSale_Should_Throw_PaymentValueException_When_Payment_Is_Lower_Than_Zero()
        {
            var id = 1;

            var value = -1M;

            await Should
                .ThrowAsync<PaymentValueException>(_salesRepository.PayForSaleAsync(id, value, CancellationToken.None));
        }

        [Fact]
        public async Task PayForSale_Should_Throw_SaleNotExistsException_When_Given_IdContract_Not_Exists_In_The_Database()
        {
            var id = 0;

            var value = 2000M;

            await Should
                .ThrowAsync<SaleNotExistsException>(_salesRepository.PayForSaleAsync(id, value, CancellationToken.None));
        }

        [Fact]
        public async Task PayForSale_Should_Throw_InactiveSaleException_When_PaymentDay_Is_Lower_Than_Contract_CreationDate()
        {
            var id = 2;

            var value = 2000M;

            await Should
                .ThrowAsync<InactiveSaleException>(_salesRepository.PayForSaleAsync(id, value, CancellationToken.None));
        }

        [Fact]
        public async Task PayForSale_Should_Throw_InactiveSaleException_When_PaymentDay_Is_Higher_Than_Contract_ExpirationDate()
        {
            var id = 3;

            var value = 2000M;

            await Should
                .ThrowAsync<InactiveSaleException>(_salesRepository.PayForSaleAsync(id, value, CancellationToken.None));
        }

        [Fact]
        public async Task PayForSale_Should_Throw_PaymentValueException_When_Sum_Of_Contract_Payments_Actual_Payment_Is_Higher_Than_Contract_Price()
        {
            var id = 1;

            var value = 501M;

            await Should
                .ThrowAsync<PaymentValueException>(_salesRepository.PayForSaleAsync(id, value, CancellationToken.None));
        }

        [Fact]
        public async Task PayForSale_Should_Add_New_Payment_Into_Database()
        {
            var id = 1;

            var value = 500M;

            var result = await _salesRepository.PayForSaleAsync(id, value, CancellationToken.None);

            result.ShouldBeEquivalentTo(1);
        }
    }
}
