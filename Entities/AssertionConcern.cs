using ValueObjects;

namespace Entities;

public class AssertionConcern
{
    public static void AssertArgumentNotEmpty(string argument, string message)
    {
        if (argument.Equals(string.Empty))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentNotEmpty(ValueObject argument, string message)
    {
        if (argument is null)
        {
            throw new DomainException(message);
        }
    }
}