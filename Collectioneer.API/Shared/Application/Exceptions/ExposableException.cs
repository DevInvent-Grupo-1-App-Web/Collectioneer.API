namespace Collectioneer.API.Shared.Application.Exceptions
{
    public partial class ExposableException(string message, int statusCode) : Exception(message)
    {
        public int StatusCode { get; set; } = statusCode;
    }
}
