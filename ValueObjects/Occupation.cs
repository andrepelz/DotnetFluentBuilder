using System.Text.Json;

namespace ValueObjects;

public record Occupation : ValueObject
{
    public string Department { get; init; } = string.Empty;
    public decimal AnnualIncome { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Department;
        yield return AnnualIncome;
    }
}