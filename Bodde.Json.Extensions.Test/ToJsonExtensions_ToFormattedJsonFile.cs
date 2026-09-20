using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class ToJsonExtensions_ToFormattedJsonFile
{
    [Fact]
    public void NonNull_Writes_Formatted_Json_To_File()
    {
        var filename = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

        try
        {
            ModelBuilder.CreateJohnDoe().ToFormattedJsonFile(filename);

            var actual = File.ReadAllText(filename);

            Assert.NotEmpty(actual);
            Assert.Contains(Environment.NewLine, actual);
            Assert.Contains("\"Department\"", actual);
        }
        finally
        {
            File.Delete(filename);
        }
    }

    [Fact]
    public void Null_Object_Throws_ArgumentNullException()
    {
        Employee? sut = null;
        var filename = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

        Assert.Throws<ArgumentNullException>(() => sut!.ToFormattedJsonFile(filename));
    }

    [Fact]
    public void Null_Filename_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => ModelBuilder.CreateJohnDoe().ToFormattedJsonFile(null!));
    }

    [Fact]
    public void Empty_Filename_Throws_ArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => ModelBuilder.CreateJohnDoe().ToFormattedJsonFile(string.Empty));
    }
}