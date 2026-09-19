using System.Text.Json;
using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class JsonExtensions_ToFormattedJson
{
    [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        Employee? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.ToFormattedJson());
        Assert.True(true);
    }

    [Fact]
    public void NonNull_Returns_Indented_EnumsAsStrings_Json()
    {
        Employee sut = ModelBuilder.CreateJohnDoe();
        
        var expected = string.Join(Environment.NewLine, [
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

        var actual = sut.ToFormattedJson();

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NonNull_Indented_False_Returns_NotIndented_EnumsAsStrings_Json()
    {
        Employee sut = ModelBuilder.CreateJohnDoe();
        
        var expected = string.Concat(
            "{",
            "\"Id\":1,",
            "\"FirstName\":\"John\",",
            "\"LastName\":\"Doe\",",
            "\"BirthDate\":\"1980-01-01T00:00:00\",",
            "\"Role\":\"Developer\",",
            "\"Department\":{",
            "\"Id\":1,",
            "\"Name\":\"Engineering\"",
            "}",
            "}");

        var actual = sut.ToFormattedJson(indented: false);

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumsAsStrings_False_Returns_Indented_Json()
    {
        Employee sut = ModelBuilder.CreateJohnDoe();
        
        var expected = string.Join(Environment.NewLine, [
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

        var actual = sut.ToFormattedJson(enumsAsStrings: false);

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NonNull_Indented_False_EnumsAsStrings_False_Returns_NotIndented_EnumsAsValues_Json()
    {
        Employee sut = ModelBuilder.CreateJohnDoe();
        
        var expected = string.Concat(
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

        var actual = sut.ToFormattedJson(indented: false, enumsAsStrings: false);

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }
}
