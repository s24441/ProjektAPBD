namespace ProjektAPBD.WebApi.DTOs.ExchangeApi
{
    public class ExchangeRatesDTO
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public IEnumerable<RateDTO> Rates { get; set; }
    }
}
