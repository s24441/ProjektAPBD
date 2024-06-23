namespace ProjektAPBD.WebApi.Exceptions.ClientManagement
{
    public class ClientValidationException : Exception
    {
        public ClientValidationException() { }
        public ClientValidationException(string message) : base(message) { }
    }
}
