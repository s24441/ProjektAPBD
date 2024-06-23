using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjektAPBD.Tests.DbSeed;
using ProjektAPBD.WebApi.Configuration;
using ProjektAPBD.WebApi.External.Clients;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Repositories;
using Shouldly;

namespace ProjektAPBD.Tests
{
    [Collection("TestsSequence")]
    public class IncomeManagementTests
    {
        private IIncomeManagemenRepository _incomeRepository = new IncomeManagemenRepository(new ManagementDbContext(new DbContextOptionsBuilder<ManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "Database")
                .Options)
            .TestSeed(), new NbpExchangeApiClient(Options.Create<ExchangeApiConfiguration>(new()
            {
                BaseAddress = "http://api.nbp.pl/api/",
                Endpoint = "exchangerates/rates/a/"
            })));

        [Fact]
        public async Task GetActualIncome_Should_Return_Income_For_All_Products_When_IdProduct_Is_NUll()
        {
            var result = await _incomeRepository.GetActualIncomeAsync();

            result.ShouldBe(12000M);
        }

        [Fact]
        public async Task GetActualIncome_Should_Return_Income_For_Product_When_IdProduct_Is_Not_NUll()
        {
            var result = await _incomeRepository.GetActualIncomeAsync(1);

            result.ShouldBeEquivalentTo(2000M);
        }

        [Fact]
        public async Task GetActualIncome_Should_Return_USD_Income_For_All_Products_When_IdProduct_Is_NUll_And_Currency_Is_USD()
        {
            var result = await _incomeRepository.GetActualIncomeAsync(currency: "USD");

            result.ShouldBeInRange(2900M, 3100M);
        }

        [Fact]
        public async Task GetActualIncome_Should_Return_USD_Income_For_Product_When_IdProduct_Is_Not_NUll_And_Currency_Is_USD()
        {
            var result = await _incomeRepository.GetActualIncomeAsync(1, "USD");

            result.ShouldBeInRange(450M, 550M);
        }
    }
}
