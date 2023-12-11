using System.Text.Json;

namespace ValueObjects;

public record Address : ValueObject
{
    public string StreetAddress { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StreetAddress;
        yield return City;
        yield return PostalCode;
    }
}