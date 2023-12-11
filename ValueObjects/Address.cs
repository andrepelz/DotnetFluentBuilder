namespace ValueObjects;

public record Address
{
    public string StreetAddress { get; private set; } = string.Empty;
    public string? City { get; private set; }
    public string? PostalCode { get; private set; }
}