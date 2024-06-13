namespace ProjektAPBD.WebApi.DTOs.SubscriptionsManagement
{
    public class BuySubscriptionDTO
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public int RenewalMonthsPeriod { get; set; }
        public decimal Price { get; set; }
    }
}
