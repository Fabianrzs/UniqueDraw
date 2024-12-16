namespace UniqueDraw.Domain.Exceptions;

public class ValidationException(string message) : DomainException(message)
{
}
