
using Bodde.Json.Extensions.Test.Helpers;
using Bodde.Json.Extensions.Test.Models;

namespace Bodde.Json.Extensions.Test;

public class ToJsonExtensions_ToJsonWithMetadata
{
    [Fact]
    public void Null_Throws_ArgumentNullException()
    {
        Employee? sut = null;
        Assert.Throws<ArgumentNullException>(() => sut!.ToJsonWithMetadata());
        Assert.True(true);
    }

    [Fact]
    public void Polymorphic_Animals_Returns_Valid_Json()
    {
        JsonTypeResolver.Configure(cfg => cfg
            .Register<IAnimal,Dog>()
            .Register<IAnimal,Cat>()
            );

        var sut = new IAnimal[]
        {
            new Dog { Name = "Buddy", Breed = DogBreedType.Beagle },
            new Cat { Name = "Whiskers", Color = ColorType.Orange }
        };

        var expected = String.Concat(
            "[",
            "{",
            "\"$type\":\"ianimal.dog\",",
            "\"Name\":\"Buddy\",",
            "\"Breed\":3",
            "},",
            "{",
            "\"$type\":\"ianimal.cat\",",
            "\"Name\":\"Whiskers\",",
            "\"Color\":2",
            "}",
            "]"
        );

        var actual = sut.ToJsonWithMetadata(objectReference: false);

        Assert.NotEmpty(actual);
        Assert.Equal(expected, actual);
    }

}
