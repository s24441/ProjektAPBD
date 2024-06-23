namespace ProjektAPBD.WebApi.Exceptions
{
    public class PaymentValueException : Exception
    {
        public PaymentValueException() : base() { }
        public PaymentValueException(string message) : base(message) { }
    }
}
