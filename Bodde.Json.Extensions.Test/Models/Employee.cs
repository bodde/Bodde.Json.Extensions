namespace Bodde.Json.Extensions.Test.Models;

internal class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public RoleType Role { get; set; }
    public Department? Department { get; set; }
}

internal enum RoleType
{
    Developer,
    Manager,
    Tester
}