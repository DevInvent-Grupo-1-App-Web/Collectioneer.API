using Collectioneer.API.Shared.Application.Exceptions;

namespace Collectioneer.API.Shared.Domain.Exceptions
{
    public class UserModelException(string message) : ExposableException(message, 400)
    {
    }
}
