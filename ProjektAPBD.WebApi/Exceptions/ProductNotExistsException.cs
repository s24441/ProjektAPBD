namespace ProjektAPBD.WebApi.Exceptions
{
    public class ProductNotExistsException : Exception
    {
        public ProductNotExistsException() { }
        public ProductNotExistsException(string message) : base(message) { }
    }
}
