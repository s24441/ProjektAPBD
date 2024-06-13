using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class SoftwareProduct
    {
        public int IdSoftwareProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ActualVersion { get; set; }
        public DateTime ActualVersionReleaseDate { get; set; }
        public string Category { get; set; }

        /* Navigations */
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public virtual ICollection<ContractBase> Contracts { get; set; } = new List<ContractBase>();
    }
}
