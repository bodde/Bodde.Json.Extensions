using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class FromJsonExtensions_FromJsonFile
{
    [Fact]
    public void Valid_File_Returns_Employee()
    {
        var filename = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

        try
        {
            File.WriteAllText(filename, ModelBuilder.CreateJohnDoe().ToJson());

            var actual = filename.FromJsonFile<Employee>();

            Assert.Equal(1, actual.Id);
            Assert.Equal("John", actual.FirstName);
            Assert.Equal(RoleType.Developer, actual.Role);
        }
        finally
        {
            File.Delete(filename);
        }
    }

    [Fact]
    public void Null_Filename_Throws_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => ((string)null!).FromJsonFile<Employee>());
    }

    [Fact]
    public void Empty_Filename_Throws_ArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => string.Empty.FromJsonFile<Employee>());
    }

    [Fact]
    public void Missing_File_Throws_FileNotFoundException()
    {
        var filename = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

        Assert.Throws<FileNotFoundException>(
            () => filename.FromJsonFile<Employee>());
    }

    [Fact]
    public void Empty_File_Throws_ArgumentNullException()
    {
        var filename = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

        try
        {
            File.WriteAllText(filename, string.Empty);

            Assert.Throws<ArgumentNullException>(
                () => filename.FromJsonFile<Employee>());
        }
        finally
        {
            File.Delete(filename);
        }
    }
}