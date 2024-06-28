namespace Collectioneer.API.Shared.Application.Exceptions
{
    public partial class ExposableException : Exception
    {
        public int StatusCode { get; set; }

        public ExposableException(string message, int statusCode, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}