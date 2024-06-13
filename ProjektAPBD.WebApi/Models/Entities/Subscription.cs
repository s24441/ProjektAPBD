using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class Subscription : ContractBase
    {
        public string Name { get; set; }
        public int RenewalPeriod { get; set; }
        public DateTime EndTime { get; set; }
    }
}
