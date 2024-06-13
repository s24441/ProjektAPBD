namespace ProjektAPBD.WebApi.External.Clients.Exceptions
{
    public class NbpExchangeApiException : Exception
    {
        public int StatusCode { get; private set; }

        public string? Response { get; private set; }

        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public NbpExchangeApiException(string message, int statusCode, string? response, IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception? innerException)
            : base($"{message}\n\nStatus: {statusCode}\nResponse: \n {((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length))}", innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString() => string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}
