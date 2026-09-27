using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Bodde.Json.Extensions;

public static class JsonTypeResolver
{
    private static readonly ConcurrentDictionary<Type, List<JsonDerivedType>> typeMappings = new();

    public static void Configure(Action<JsonTypeResolverOptions> configure)
    {
        List<(Type BaseType, Type DerivedType)> registrations = [];

        var options = new JsonTypeResolverOptions(registrations);
        configure(options);

        foreach (var (baseType, derivedType) in registrations)
        {
            var derivedTypes = typeMappings.GetOrAdd(baseType, _ => []);
            var existingDerivedType = derivedTypes.FirstOrDefault(dt => dt.DerivedType == derivedType);
            if (existingDerivedType.DerivedType != null)
            {
                throw new InvalidOperationException($"The derived type {derivedType.FullName} is already registered for the base type {baseType.FullName}");
            }

            var typeDiscriminator = GetTypeDiscriminator(baseType, derivedType);
            derivedTypes.Add(new JsonDerivedType(derivedType, typeDiscriminator));
        }
    }

    internal static IJsonTypeInfoResolver? CreateTypeInfoResolver()
    {
        if (typeMappings.IsEmpty)
        {
            return null;
        }

        var resolver = new DefaultJsonTypeInfoResolver();

        foreach (var baseTypeMappings in typeMappings)
        {
            var baseType = baseTypeMappings.Key;
            var derivedTypes = baseTypeMappings.Value;

            resolver.Modifiers.Add(typeInfo => ApplyTypeInfoModifiers(typeInfo, baseType, derivedTypes));
        }

        return resolver;
    }

    private static void ApplyTypeInfoModifiers(JsonTypeInfo typeInfo, Type baseType, List<JsonDerivedType> derivedTypes)
    {
        if (typeInfo.Type == baseType)
        {
            foreach (var derivedType in derivedTypes)
            {
                if (typeInfo.PolymorphismOptions == null)
                {
                    typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = "$type",
                        IgnoreUnrecognizedTypeDiscriminators = false,
                        UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                    };
                }

                typeInfo.PolymorphismOptions.DerivedTypes.Add(derivedType);
            }
        }
    }

    private static string GetTypeDiscriminator(Type baseType, Type derivedType)
    {
        return string.Concat(baseType.Name.ToLower(), ".", derivedType.Name.ToLower());
    }

    public class JsonTypeResolverOptions
    {
        readonly List<(Type BaseType, Type DerivedType)> registrations;

        internal JsonTypeResolverOptions(List<(Type BaseType, Type DerivedType)> registrations)
        {
            this.registrations = registrations;
        }

        public JsonTypeResolverOptions Register<TBase, TDerived>() where TDerived : TBase
        {
            return Register(typeof(TBase), typeof(TDerived));
        }

        public JsonTypeResolverOptions Register(Type baseType, Type derivedType)
        {
            if (!baseType.IsAssignableFrom(derivedType))
            {
                throw new ArgumentException($"The type {derivedType.FullName} is not assignable to {baseType.FullName}");
            }

            registrations.Add((baseType, derivedType));
            return this;
        }
    }
    
}

