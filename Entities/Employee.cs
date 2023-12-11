using System.Text.Json;
using ValueObjects;

namespace Entities;

partial class Employee
{
    public string Name { get; private set; }

    private Employee(string name)
        => Name = name;

    public Address Address { get; private set; } = default!;
    public Job? Job { get; private set; } = default!;

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
        private string Department { get; set; } = string.Empty;
        private decimal AnnualIncome { get; set; }


        public EmployeeJobBuilder(Employee e) : base(e) { }

        public EmployeeJobBuilder At(string department)
        {
            Department = department;
            result.Job = TryBuildJob();
            return this;
        }

        public EmployeeJobBuilder Earning(decimal annualIncome)
        {
            AnnualIncome = annualIncome;
            result.Job = TryBuildJob();
            return this;
        }

        private Job? TryBuildJob()
        {
            Job newJob = new()
            {
                Department = Department,
                AnnualIncome = AnnualIncome
            };

            return newJob.IsValid() ? newJob : null;
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
            Address newAddress = new()
            {
                StreetAddress = StreetAddress,
                City = City,
                PostalCode = PostalCode
            };

            return newAddress.IsValid() ? newAddress : null;
        }
    }
}