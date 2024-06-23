namespace ProjektAPBD.WebApi.Exceptions
{
    public class ClientNotExistsException : Exception
    {
        public ClientNotExistsException() { }
        public ClientNotExistsException(string message) : base(message) { }
    }
}
