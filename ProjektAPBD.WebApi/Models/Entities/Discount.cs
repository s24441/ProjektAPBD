namespace ProjektAPBD.WebApi.Models.Entities
{
    public class Discount
    {
        public int IdDiscount { get; set; }
        public int IdSoftwareProduct { get; set; }

        /* 1 - 100 (Zawiera procent zniżki) */
        public int PercentageValue { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        /* Navigations */
        public virtual SoftwareProduct Product { get; set; } = null!;
    }
}
