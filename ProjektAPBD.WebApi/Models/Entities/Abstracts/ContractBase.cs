namespace ProjektAPBD.WebApi.Models.Entities.Abstracts
{
    public abstract class ContractBase
    {
        public int IdContract { get; set; }
        public int IdClient { get; set; }
        public int IdSoftwareProduct { get; set; }

        /* uwzględnia najwyższą z aktualnych zniżek + ew. 5% dla stałego klienta */
        public decimal Price { get; set; }

        /* Navigations */
        public virtual ClientBase Client { get; set; } = null!;
        public virtual SoftwareProduct Product { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
