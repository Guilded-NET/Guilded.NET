using System;
using System.Drawing;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Converters
{
    /// <summary>
    /// Converts colour to hex.
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
        /// Writes colour as RGB colour(hex) or as "transparent".
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">Colour to convert</param>
        /// <param name="serializer">Serializer</param>
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
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            throw new InvalidOperationException("This converter does not support reading.");
        /// <summary>
        /// Checks if objectType can be written.
        /// </summary>
        /// <param name="objectType">Type of the object to check</param>
        /// <returns>Can convert type</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == colour;
    }
}