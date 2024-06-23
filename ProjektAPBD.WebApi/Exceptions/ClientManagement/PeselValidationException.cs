namespace ProjektAPBD.WebApi.Exceptions.ClientManagement
{
    public class PeselValidationException : Exception
    {
        public PeselValidationException() { }
        public PeselValidationException(string message) : base(message) { }
    }
}
