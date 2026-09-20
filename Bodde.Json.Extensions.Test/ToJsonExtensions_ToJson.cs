
using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class ToJsonExtensions_ToJson
{
    [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        Employee? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.ToJson());
        Assert.True(true);
    }

    [Fact]
    public void NonNull_Returns_Valid_Json()
    {
        var sut = ModelBuilder.CreateJohnDoe();

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

        var actual = sut.ToJson();

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NonNull_Options_Indented_Returns_Indented_Valid_Json()
    {
        var sut = ModelBuilder.CreateJohnDoe();

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

        var actual = sut.ToJson(new () { WriteIndented = true });

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }
}
