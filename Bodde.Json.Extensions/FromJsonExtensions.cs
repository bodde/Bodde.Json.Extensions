using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bodde.Json.Extensions;

public static class FromJsonExtensions
{
    extension<T>(string me)
    {
        /// <summary>
        /// Deserializes the JSON string to an object of type T using System.Text.Json.JsonSerializer.
        /// </summary>
        /// <param name="options">The options to use for deserialization.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the JSON string is null or empty.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON deserialization.</exception>
        public T FromJson(JsonSerializerOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(me))
            {
                throw new ArgumentNullException(nameof(me));
            }

            return JsonSerializer.Deserialize<T>(me, options)
                ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        /// <summary>
        /// Deserializes the JSON string to an object of type T using formatted options.
        /// </summary>
        /// <param name="enumsAsStrings">Indicates whether to convert enums to strings. Defaults to true.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the JSON string is null or empty.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON deserialization.</exception>
        public T FromFormattedJson(bool enumsAsStrings = true, bool ignoreCase = true)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = ignoreCase
            };

            if (enumsAsStrings)
            {
                options.Converters.Add(new JsonStringEnumConverter());
            }

            return me.FromJson<T>(options);
        }

        /// <summary>
        /// Reads the file with the specified name and deserializes its content to an object of type T using System.Text.Json.JsonSerializer.
        /// </summary>
        /// <param name="options">The options to use for deserialization.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the file name is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON deserialization.</exception>
        public T FromJsonFile(JsonSerializerOptions? options = null)
            => File.ReadAllText(me).FromJson<T>(options);

        /// <summary>
        /// Reads the file with the specified name and deserializes its content to an object of type T using formatted options.
        /// </summary>
        /// <param name="enumsAsStrings">Indicates whether to convert enums to strings.</param>
        /// <param name="ignoreCase">Indicates whether to ignore case when deserializing properties.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the file name is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON deserialization.</exception>
        /// <exception cref="IOException">Thrown when an I/O error occurs while reading the file.</exception>
        public T FromFormattedJsonFile(bool enumsAsStrings = true, bool ignoreCase = true)
            => File.ReadAllText(me).FromFormattedJson<T>(enumsAsStrings, ignoreCase);
    }
}
