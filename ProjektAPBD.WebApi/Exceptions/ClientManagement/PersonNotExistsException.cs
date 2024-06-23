namespace ProjektAPBD.WebApi.Exceptions.ClientManagement
{
    public class PersonNotExistsException : Exception
    {
        public PersonNotExistsException() { }
        public PersonNotExistsException(string message) : base(message) { }
    }
}
