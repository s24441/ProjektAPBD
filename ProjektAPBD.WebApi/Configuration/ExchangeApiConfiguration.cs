namespace ProjektAPBD.WebApi.Configuration
{
    public class ExchangeApiConfiguration
    {
        public const string Section = "ApiClients:Exchange";
        public string BaseAddress { get; set; }
        public string Endpoint { get; set; }
    }
}
