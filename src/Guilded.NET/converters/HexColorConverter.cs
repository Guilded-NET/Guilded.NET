using System;
using System.Drawing;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Converters
{
    /// <summary>
    /// Converts colour to hex for roles.
    /// </summary>
    public class HexColorConverter : JsonConverter
    {
        private static readonly Type colour = typeof(Color);
        /// <summary>
        /// If this converter can read JSON value.
        /// </summary>
        /// <value>False</value>
        public override bool CanRead => false;
        /// <summary>
        /// Writes given object as JSON.
        /// </summary>
        /// <param name="writer">The writer to use to write to JSON</param>
        /// <param name="value">The object to write to JSON</param>
        /// <param name="serializer">The serializer that is serializing the object</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Color col = (Color)value;
            writer.WriteValue(JToken.FromObject(
                col.A == 0
                ? "transparent"
                : $"#{col.R:X2}{col.G:X2}{col.B:X2}"
            ));
        }
        /// <summary>
        /// Reads the given JSON object as <see cref="Color"/>.
        /// </summary>
        /// <param name="reader">The reader that was used to read JSON</param>
        /// <param name="objectType">The type of the object to convert</param>
        /// <param name="existingValue">The previous value of the property being converted</param>
        /// <param name="serializer">The serializer that is deserializing the object</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            throw new NotSupportedException("This converter does not support reading.");
        /// <summary>
        /// Returns whether the converter supports converting the given type.
        /// </summary>
        /// <param name="objectType">The type of object that potentially can be converted</param>
        /// <returns>Type can be converted</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == colour;
    }
}