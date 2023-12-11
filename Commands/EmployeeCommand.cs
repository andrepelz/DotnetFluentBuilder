namespace Commands;

public class EmployeeCommand
{
    public string Name { get; set; } = string.Empty;

    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Department { get; set; }
    public decimal AnnualIncome { get; set; }
}