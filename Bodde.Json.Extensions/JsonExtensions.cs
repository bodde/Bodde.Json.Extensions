using System.Text.Json;

namespace Bodde.Json.Extensions
{
    public static class JsonExtensions
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
            /// Serializes the object to a JSON string using System.Text.Json.JsonSerializer with indentation.
            /// </summary>
            /// <param name="options">The options to use for serialization. Indentation will be always overridden.</param>
            /// <returns>The indented JSON string.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the object to serialize is null.</exception>
            public string ToIndentedJson(JsonSerializerOptions? options = null)
            {
                options ??= new ();
                options.WriteIndented = true;

                return me.ToJson(options);
            }
        }
    }
}
