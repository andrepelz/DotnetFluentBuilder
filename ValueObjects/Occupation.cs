using System.Text.Json;

namespace ValueObjects;

public record Job : ValueObject
{
    public string Department { get; init; } = string.Empty;
    public decimal AnnualIncome { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Department;
        yield return AnnualIncome;
    }

    public override bool IsValid()
    {
        if(string.IsNullOrWhiteSpace(Department)) return false;

        if(AnnualIncome <= 0) return false;

        return true;
    }
}