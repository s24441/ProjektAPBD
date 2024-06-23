namespace ProjektAPBD.WebApi.Exceptions.SalesManagement
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException() { }
        public InvalidDateException(string message) : base(message) { }
    }
}
