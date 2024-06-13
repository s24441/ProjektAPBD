namespace ProjektAPBD.WebApi.DTOs.ClientsManagement
{
    public class AddClientDTO
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AddPhysicalPersonDTO? PhysicalPerson { get; set; }
        public AddCompanyDTO? Company { get; set; } 
    }
}
