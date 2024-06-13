namespace ProjektAPBD.WebApi.DTOs.SalesManagement
{
    public class AddSaleDTO
    {
        public int IdClient { get; set; }
        public DateTime? CreationDate { get; set; }
        public int ExpirationDaysRange { get; set; }
        public int? AdditionalSupportYearsAmount { get; set; }
        public decimal Price { get; set; }
    }
}
