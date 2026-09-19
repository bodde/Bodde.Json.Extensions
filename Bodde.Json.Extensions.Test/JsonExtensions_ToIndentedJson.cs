using System.Text.Json;
using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class JsonExtensions_ToIndentedJson
{
    [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        Employee? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.ToIndentedJson());
        Assert.True(true);
    }

    [Fact]
    public void NonNull_Returns_Indented_Valid_Json()
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

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var actual = sut.ToIndentedJson();

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NonNull_NotIndented_Returns_Indented_Valid_Json()
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

 
        var actual = sut.ToIndentedJson(new (){ WriteIndented = false });

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }
}
