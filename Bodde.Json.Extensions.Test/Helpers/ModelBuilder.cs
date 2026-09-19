using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test.Helpers;

internal static class ModelBuilder
{
    public static Employee CreateJohnDoe(int id = 1)
    {
        return new Employee
        {
            Id = id,
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(1980, 1, 1),
            Role = RoleType.Developer,
            Department = CreateEngineeringDepartment()
        };
    }

    public static Department CreateEngineeringDepartment(int id = 1)
    {
        return new Department { Id = id, Name = "Engineering" };
    }
}
