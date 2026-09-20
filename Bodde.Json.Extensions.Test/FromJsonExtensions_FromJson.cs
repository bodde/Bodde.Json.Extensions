
using System.Text.Json.Serialization;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class FromJsonExtensions_FromJson
{
    [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        string? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.FromJson<Employee>());
        Assert.True(true);
    }

    [Fact]
    public void NonNull_Returns_Employee_Object()
    {
        var sut = string.Concat(
            "{",
            "\"Id\":1,",
            "\"FirstName\":\"John\",",
            "\"LastName\":\"Doe\",",
            "\"BirthDate\":\"1980-01-01T00:00:00\",",
            "\"Role\":0,",
            "\"Department\":{",
            "\"Id\":1,",
            "\"Name\":\"Engineering\"",
            "}",
            "}");

        var actual = sut.FromJson<Employee>();

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Id);
        Assert.Equal("John", actual.FirstName);
        Assert.Equal("Doe", actual.LastName);
        Assert.Equal(new DateTime(1980, 1, 1), actual.BirthDate);
        Assert.Equal(RoleType.Developer, actual.Role);
        Assert.NotNull(actual.Department);
        Assert.Equal(1, actual.Department.Id);
        Assert.Equal("Engineering", actual.Department.Name);
    }

    
    [Fact]
    public void NonNull_Indented_Returns_Employee_Object()
    {
        var sut = string.Join(Environment.NewLine, [
            "{",
            "  \"Id\": 1,",
            "  \"FirstName\": \"John\",",
            "  \"LastName\": \"Doe\",",
            "  \"BirthDate\": \"1980-01-01T00:00:00\",",
            "  \"Role\": 0,",
            "  \"Department\": {",
            "    \"Id\": 1,",
            "    \"Name\": \"Engineering\"",
            "  }",
            "}"
        ]);

        var actual = sut.FromJson<Employee>();

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Id);
        Assert.Equal("John", actual.FirstName);
        Assert.Equal("Doe", actual.LastName);
        Assert.Equal(new DateTime(1980, 1, 1), actual.BirthDate);
        Assert.Equal(RoleType.Developer, actual.Role);
        Assert.NotNull(actual.Department);
        Assert.Equal(1, actual.Department.Id);
        Assert.Equal("Engineering", actual.Department.Name);
    }

     
    [Fact]
    public void NonNull_RoleType_As_String_Options_JsonStringEnumConverter_Returns_Employee_Object()
    {
        var sut = string.Join(Environment.NewLine, [
            "{",
            "  \"Id\": 1,",
            "  \"FirstName\": \"John\",",
            "  \"LastName\": \"Doe\",",
            "  \"BirthDate\": \"1980-01-01T00:00:00\",",
            "  \"Role\": \"Developer\",",
            "  \"Department\": {",
            "    \"Id\": 1,",
            "    \"Name\": \"Engineering\"",
            "  }",
            "}"
        ]);

        var actual = sut.FromJson<Employee>(new ()
        {
            Converters = { new JsonStringEnumConverter() }
        });

        Assert.NotNull(actual);
        Assert.Equal(1, actual.Id);
        Assert.Equal("John", actual.FirstName);
        Assert.Equal("Doe", actual.LastName);
        Assert.Equal(new DateTime(1980, 1, 1), actual.BirthDate);
        Assert.Equal(RoleType.Developer, actual.Role);
        Assert.NotNull(actual.Department);
        Assert.Equal(1, actual.Department.Id);
        Assert.Equal("Engineering", actual.Department.Name);
    }
}
