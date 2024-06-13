using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjektAPBD.WebApi.Configuration;
using ProjektAPBD.WebApi.DTOs.ExchangeApi;
using ProjektAPBD.WebApi.External.Clients.Exceptions;
using ProjektAPBD.WebApi.Interfaces.ApiClients;

namespace ProjektAPBD.WebApi.External.Clients
{
    public class NbpExchangeApiClient : IExchangeApiClient
    {
        private readonly ExchangeApiConfiguration _config;

        public NbpExchangeApiClient(IOptions<ExchangeApiConfiguration> configOptions) 
        { 
            _config = configOptions.Value;
        }

        public async Task<ExchangeRatesDTO?> GetExchangeRatesAsync(string currency)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_config.BaseAddress);

            var response = await client.GetAsync($"{_config.Endpoint}/{currency}/?format=json");

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();

                var exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesDTO>(content);

                return exchangeRates;
            }
            else {
                var responseData_ = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                throw new NbpExchangeApiException($"The HTTP status code of the response was not expected ({response.StatusCode}).", (int)response.StatusCode, responseData_, response.Headers.ToDictionary(x => x.Key, x => x.Value), null);
            }
        }
    }
}
