using Entities;
using Commands;

var command = new EmployeeCommand
{
    Name = "Odmilson",
    StreetAddress = "Av. Marcos Konder, 1350",
    City = "Itajaí",
    PostalCode = "88300000",
    Department = "Allog",
    AnnualIncome = 1000.00M

};


Employee e = Employee.Create(command.Name)
    .Lives
        .At(command.StreetAddress)
        .In(command.City)
        .WithPostalCode(command.PostalCode)
     .Works
        .At(command.Department)
        .Earning(command.AnnualIncome);


Console.WriteLine(e.ToString());

