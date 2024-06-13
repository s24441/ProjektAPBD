using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class Payment
    {
        public int IdPayment { get; set; }
        public int IdClient { get; set; }
        public int IdContract { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        /* Navigations */

        public virtual ClientBase Client { get; set; } = null!;
        public virtual ContractBase Contract { get; set; } = null!;
    }
}
