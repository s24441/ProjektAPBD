using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class Sale : ContractBase
    {
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupportYearsAmount { get; set; }
    }
}
