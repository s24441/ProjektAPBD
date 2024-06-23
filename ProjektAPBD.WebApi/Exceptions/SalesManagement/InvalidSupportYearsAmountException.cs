namespace ProjektAPBD.WebApi.Exceptions.SalesManagement
{
    public class InvalidSupportYearsAmountException : Exception
    {
        public InvalidSupportYearsAmountException() { }
        public InvalidSupportYearsAmountException(string message) : base(message) { }
    }
}
