namespace ProjektAPBD.WebApi.Exceptions.SalesManagement
{
    public class SaleNotExistsException : Exception
    {
        public SaleNotExistsException() { }
        public SaleNotExistsException(string message) : base(message) { }
    }
}
