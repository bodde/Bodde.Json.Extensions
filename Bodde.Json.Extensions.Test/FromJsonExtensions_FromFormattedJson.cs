using System.Text.Json;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class FromJsonExtensions_FromFormattedJson
{
        [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        string? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.FromFormattedJson<Employee>());
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

        var actual = sut.FromFormattedJson<Employee>();

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

        var actual = sut.FromFormattedJson<Employee>();

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
    public void NonNull_RoleType_As_String_Returns_Employee_Object()
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

        var actual = sut.FromFormattedJson<Employee>();

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
    public void NonNull_Wrong_Case_Returns_Employee_Object()
    {
        var sut = string.Join(Environment.NewLine, [
            "{",
            "  \"id\": 1,",
            "  \"firstName\": \"John\",",
            "  \"lastName\": \"Doe\",",
            "  \"birthDate\": \"1980-01-01T00:00:00\",",
            "  \"role\": \"Developer\",",
            "  \"department\": {",
            "    \"id\": 1,",
            "    \"Name\": \"Engineering\"",
            "  }",
            "}"
        ]);

        var actual = sut.FromFormattedJson<Employee>();

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
    public void NonNull_RoleType_As_String_EnumAsStrings_JsonException()
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

        Assert.Throws<JsonException>(() => sut.FromFormattedJson<Employee>(enumsAsStrings: false));
        Assert.True(true);
    }
      
    [Fact]
    public void NonNull_Wrong_Case_IgnoreCase_False_JsonException()
    {
        var sut = string.Join(Environment.NewLine, [
            "{",
            "  \"id\": 1,",
            "  \"firstName\": \"John\",",
            "  \"lastName\": \"Doe\",",
            "  \"birthDate\": \"1980-01-01T00:00:00\",",
            "  \"role\": \"Developer\",",
            "  \"department\": {",
            "    \"id\": 1,",
            "    \"Name\": \"Engineering\"",
            "  }",
            "}"
        ]);

        Assert.Throws<JsonException>(() => sut.FromFormattedJson<Employee>(ignoreCase: false));
        Assert.True(true);
    }

}
