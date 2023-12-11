using System.Text.Json;
using ValueObjects;

namespace Entities;

partial class Employee
{
    public string Name { get; private set; }

    private Employee(string name)
        => Name = name;

    // public string StreetAddress { get; private set; } = string.Empty;
    // public string? City { get; private set; }
    // public string? PostalCode { get; private set; }
    public Address Address { get; private set; } = default!;
    public string? Department { get; private set; }
    public decimal AnnualIncome { get; private set; }

    public void Validate()
    {
        AssertionConcern.AssertArgumentNotEmpty(
            Name,
            "The Name cannot be empty");

        AssertionConcern.AssertArgumentNotEmpty(
            Address,
            "The Address cannot be empty");
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { 
            WriteIndented = true 
        });
    }
}

partial class Employee
{
    public static EmployeeBuilder Create(string name)
        => new EmployeeBuilder(name);
    
    public class EmployeeBuilder
    {
        protected Employee result;

        public EmployeeBuilder(string name)
            => result = new Employee(name);

        protected EmployeeBuilder(Employee e)
            => result = e;

        public EmployeeAddressBuilder Lives =>
            new EmployeeAddressBuilder(result);

        public EmployeeJobBuilder Works =>
            new EmployeeJobBuilder(result);

        // Inserir todas as validações dependentes
        // Por exemplo se x não pode ter valor igual a y
        public Employee Build()
        {
            result.Validate();
            return result;
        }
     

        public static implicit operator Employee(EmployeeBuilder b)
            => b.Build();
    }
}

partial class Employee
{
    public class EmployeeJobBuilder : EmployeeBuilder
    {
        public EmployeeJobBuilder(Employee e) : base(e) { }

        public EmployeeJobBuilder At(string department)
        {
            result.Department = department;
            return this;
        }

        public EmployeeJobBuilder Earning(decimal annualIncome)
        {
            result.AnnualIncome = annualIncome;
            return this;
        }
    }
}


partial class Employee
{
    public class EmployeeAddressBuilder : EmployeeBuilder
    {
        private string StreetAddress { get; set; } = string.Empty;
        private string City { get; set; } = string.Empty;
        private string PostalCode { get; set; } = string.Empty;

        public EmployeeAddressBuilder(Employee e) : base(e) { }

        public EmployeeAddressBuilder At(string streetAddress) 
        {
            StreetAddress = streetAddress;
            result.Address = TryBuildAddress()!;
            return this;
        }

        public EmployeeAddressBuilder In(string city)
        {
            City = city;
            result.Address = TryBuildAddress()!;
            return this;
        }

        public EmployeeAddressBuilder WithPostalCode(string postalCode)
        {
            PostalCode = postalCode;
            result.Address = TryBuildAddress()!;
            return this;
        }

        private Address? TryBuildAddress()
        {
            Address? newAddress = null;

            if(
                StreetAddress != string.Empty
                && City != string.Empty
                && PostalCode != string.Empty)
            {
                newAddress = new()
                {
                    StreetAddress = StreetAddress,
                    City = City,
                    PostalCode = PostalCode
                };
            }

            return newAddress;
        }
    }
}