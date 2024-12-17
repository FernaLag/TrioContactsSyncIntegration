namespace Trio.ContactSync.Application.Exceptions
{
    public class BadRequestException(string message) : ApplicationException(message)
    {
    }
}