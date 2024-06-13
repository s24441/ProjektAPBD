using ProjektAPBD.WebApi.DTOs.ExchangeApi;

namespace ProjektAPBD.WebApi.Interfaces.ApiClients
{
    public interface IExchangeApiClient
    {
        Task<ExchangeRatesDTO?> GetExchangeRatesAsync(string currency);
    }
}
