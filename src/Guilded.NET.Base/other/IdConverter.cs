using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Converts <see cref="GId"/> to string or vice versa in a JSON.
    /// </summary>
    public class IdConverter : JsonConverter
    {
        internal static readonly Type gid = typeof(GId), formId = typeof(FormId);
        /// <summary>
        /// Writes a <see cref="GId"/> value to JSON object.
        /// </summary>
        /// <param name="writer">The writer to write to</param>
        /// <param name="value">The value</param>
        /// <param name="serializer">The calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString());
        /// <summary>
        /// Reads the given value as <see cref="GId"/>.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns><see cref="GId"/> from JSON</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            new GId((string)reader.Value);
        /// <summary>
        /// Whether this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == gid;
    }
    /// <summary>
    /// Converts string to ID for property names.
    /// </summary>
    public class GIdConverter : TypeConverter
    {
        /// <summary>
        /// Whether it can convert to ID based on context and type.
        /// </summary>
        /// <param name="context">Descriptor context for converter</param>
        /// <param name="sourceType">Type found in the source</param>
        /// <returns>Can convert</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);
        /// <summary>
        /// Converts string to ID.
        /// </summary>
        /// <param name="context">Descriptor context for converter</param>
        /// <param name="culture">Date localization culture</param>
        /// <param name="value">Value to convert to ID</param>
        /// <returns>Guilded ID</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) =>
            new GId((string)value);
    }
}