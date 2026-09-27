using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bodde.Json.Extensions;

public static class ToJsonExtensions
{
    extension<T>(T me)
    {
        /// <summary>
        /// Serializes the object to a JSON string using System.Text.Json.JsonSerializer.
        /// </summary>
        /// <param name="options">The options to use for serialization.</param>
        /// <returns>The JSON string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
        /// <exception cref="JsonException">Thrown when serialization fails due to invalid JSON or other serialization issues.</exception>
        /// <exception cref="NotSupportedException">Thrown when the type of the object to serialize is not supported by the serializer.</exception>
        public string ToJson(JsonSerializerOptions? options = null)
        {
            if (me == null)
            {
                throw new ArgumentNullException(nameof(me));
            }

            return JsonSerializer.Serialize(me, options);
        }

        /// <summary>
        /// Serializes the object to a JSON string using System.Text.Json.JsonSerializer 
        /// with options for object reference handling and derived type information.
        /// </summary>
        /// <param name="objectReference">Whether to enable object reference handling. Defaults to true.</param>
        /// <param name="derivedTypeInformation">Whether to include derived type information. Defaults to true.</param>
        /// <param name="options">The options to use for serialization.</param>
        /// <returns>The JSON string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a derived type is already registered for a base type.</exception>
        /// <exception cref="JsonException">Thrown when serialization fails due to invalid JSON or other serialization issues.</exception>
        /// <exception cref="NotSupportedException">Thrown when the type of the object to serialize is not supported by the serializer.</exception>
        public string ToJsonWithMetadata(
            bool objectReference = true, 
            bool derivedTypeInformation = true, 
            JsonSerializerOptions? options = null
            )
        {
            if (me == null)
            {
                throw new ArgumentNullException(nameof(me));
            }

            options ??= new JsonSerializerOptions();
            if(objectReference == true)
            {
                options.ReferenceHandler = ReferenceHandler.Preserve;
            }

            if(derivedTypeInformation == true)
            {
                options.TypeInfoResolver = JsonTypeResolver.CreateTypeInfoResolver();
            }

            return me.ToJson(options);
        }

        /// <summary>
        /// Serializes the object to a JSON string using formatted options.
        /// </summary>
        /// <param name="indented">Whether to format the JSON with indentation. Defaults to true.</param>
        /// <param name="enumsAsStrings">Whether to serialize enums as strings. Defaults to true.</param>
        /// <param name="skipDefaultValues">Whether to skip properties with default values. Defaults to true.</param>
        /// <returns>The JSON string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
        public string ToFormattedJson(
            bool indented = true,
            bool enumsAsStrings = true,
            bool skipDefaultValues = true
            )
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented,
                DefaultIgnoreCondition = skipDefaultValues ? JsonIgnoreCondition.WhenWritingDefault : JsonIgnoreCondition.Never
            };

            if (enumsAsStrings)
            {
                options.Converters.Add(new JsonStringEnumConverter());
            }

            return me.ToJson(options);
        }

        /// <summary>
        /// Serializes the object to a JSON string and writes it to a file.
        /// </summary>
        /// <param name="filename">The path of the file to write the JSON string to.</param>
        /// <param name="options">The options to use for serialization.</param>
        /// <exception cref="ArgumentNullException">Thrown when the file name is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
        /// <exception cref="IOException">Thrown when an I/O error occurs while writing to the file.</exception>
        public void ToJsonFile(string filename, JsonSerializerOptions? options = null)
            => File.WriteAllText(filename, me.ToJson(options));

        /// <summary>
        /// Serializes the object to a formatted JSON string and writes it to a file.
        /// </summary>
        /// <param name="filename">The path of the file to write the JSON string to.</param>
        /// <param name="indented">Whether to format the JSON with indentation. Defaults to true.</param>
        /// <param name="enumsAsStrings">Whether to serialize enums as strings. Defaults to true.</param>
        /// <param name="skipDefaultValues">Whether to skip properties with default values. Defaults to true.</param>
        /// <exception cref="ArgumentNullException">Thrown when the file name is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
        /// <exception cref="IOException">Thrown when an I/O error occurs while writing to the file.</exception>
        public void ToFormattedJsonFile(string filename, bool indented = true, bool enumsAsStrings = true, bool skipDefaultValues = true)
            => File.WriteAllText(filename, me.ToFormattedJson(indented, enumsAsStrings, skipDefaultValues));
    }
}

