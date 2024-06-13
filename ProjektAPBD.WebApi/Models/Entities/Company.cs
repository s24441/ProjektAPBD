using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Entities
{
    public class Company : ClientBase
    {
        public string Name { get; set; }
        public string KrsNumber { get; set; }
    }
}
