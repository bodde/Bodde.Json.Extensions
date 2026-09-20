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
        public string ToJson(JsonSerializerOptions? options = null)
        {
            if (me == null)
            {
                throw new ArgumentNullException(nameof(me));
            }

            return JsonSerializer.Serialize(me, options);
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
    }
}

