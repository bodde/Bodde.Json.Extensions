namespace Bodde.Json.Extensions.Test.Models;

public interface IAnimal
{
    string Name { get; }

    string Speak();
}

public class Dog : IAnimal
{
    public required string  Name { get; init; }

    public required DogBreedType Breed { get; init; }
    
    public string Speak() => "Woof!";
}

public class Cat : IAnimal
{
    public required string Name { get; init; } 

    public required ColorType Color { get; init; } 

    public string Speak() => "Meow!";
}

public enum DogBreedType
{
    Labrador,
    Poodle,
    Bulldog,
    Beagle,
    GermanShepherd
}

public enum ColorType
{
    Black,
    White,
    Orange,
    Gray,
    Calico
}