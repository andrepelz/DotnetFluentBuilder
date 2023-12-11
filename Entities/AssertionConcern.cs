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
}