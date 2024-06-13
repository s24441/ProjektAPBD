namespace ProjektAPBD.WebApi.Models.Entities.Abstracts
{
    public abstract class ClientBase
    {
        public int IdClient { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        /* Navigations */
        public virtual ICollection<ContractBase> Contracts { get; set; } = new List<ContractBase>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
