namespace ProjektAPBD.WebApi.Exceptions.SalesManagement
{
    public class InactiveSaleException : Exception
    {
        public InactiveSaleException() { }
        public InactiveSaleException(string message) : base(message) { }
    }
}
