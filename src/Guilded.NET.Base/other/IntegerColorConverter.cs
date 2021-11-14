using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Converts <see cref="Color"/> to an integer in RGB format.
    /// </summary>
    public class DecimalColorConverter : JsonConverter
    {
        internal static readonly Type colour = typeof(Color);
        /// <summary>
        /// Writes given object as JSON.
        /// </summary>
        /// <param name="writer">The writer to use to write to JSON</param>
        /// <param name="value">The object to write to JSON</param>
        /// <param name="serializer">The serializer that is serializing the object</param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            // Converts it to ARGB and filters out Alpha channel, to leave out RGB only
            if (value is not null)
                writer.WriteValue(((Color)value!).ToArgb() & 0xFFFFFF);
            else
                writer.WriteNull();
        }
        /// <summary>
        /// Reads the given JSON object as <see cref="Color"/>.
        /// </summary>
        /// <param name="reader">The reader that was used to read JSON</param>
        /// <param name="objectType">The type of the object to convert</param>
        /// <param name="existingValue">The previous value of the property being converted</param>
        /// <param name="serializer">The serializer that is deserializing the object</param>
        /// <returns><see cref="Color"/></returns>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            // Make sure some object isn't passed instead of integer
            if(token.Type == JTokenType.Integer)
                return Color.FromArgb(token.ToObject<int>());
            else return default(Color);
        }
        /// <summary>
        /// Returns whether the converter supports converting the given type.
        /// </summary>
        /// <param name="objectType">The type of object that potentially can be converted</param>
        /// <returns>Type can be converted</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == colour;
    }
}