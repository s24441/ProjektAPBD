namespace ProjektAPBD.WebApi.DTOs.ClientsManagement
{
    public class UpdateClientDTO
    {
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UpdatePhysicalPersonDTO? PhysicalPerson { get; set; }
        public UpdateComapnyDTO? Comapny { get; set; }
    }
}
