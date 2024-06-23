namespace ProjektAPBD.WebApi.Exceptions.SalesManagement
{
    public class PriceTooLowException : Exception
    {
        public PriceTooLowException() { }
        public PriceTooLowException(string message) : base(message) { }
    }
}
