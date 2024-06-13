using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class PhysicalPerson : ClientBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
    }
}
