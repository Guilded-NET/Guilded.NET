using System;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Converts <see cref="Color"/> to integer.
    /// </summary>
    public class DecimalColorConverter : JsonConverter
    {
        internal static readonly Type colour = typeof(Color);
        /// <summary>
        /// Writes a <see cref="Color"/> value to JSON object.
        /// </summary>
        /// <param name="writer">The writer to write to</param>
        /// <param name="value">The value</param>
        /// <param name="serializer">The calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            // Converts it to ARGB and filters out Alpha channel, to leave out RGB only
            writer.WriteValue(((Color)value).ToArgb() & 0xFFFFFF);
        /// <summary>
        /// Reads the given value as <see cref="Color"/>.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns><see cref="Color"/> from JSON</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            // Makes sure it's an integer
            if(token.Type == JTokenType.Integer)
                return Color.FromArgb(token.ToObject<int>());
            // Otherwise return nothing
            else return default(Color);
        }
        /// <summary>
        /// Whether this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == colour;
    }
}