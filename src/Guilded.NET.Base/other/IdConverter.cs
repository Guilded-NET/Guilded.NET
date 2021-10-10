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
        /// Writes given object as JSON.
        /// </summary>
        /// <param name="writer">The writer to use to write to JSON</param>
        /// <param name="value">The object to write to JSON</param>
        /// <param name="serializer">The serializer that is serializing the object</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(value.ToString());
        /// <summary>
        /// Reads the given JSON object as <see cref="GId"/> or <see cref="FormId"/>.
        /// </summary>
        /// <param name="reader">The reader that was used to read JSON</param>
        /// <param name="objectType">The type of the object to convert</param>
        /// <param name="existingValue">The previous value of the property being converted</param>
        /// <param name="serializer">The serializer that is deserializing the object</param>
        /// <returns><see cref="GId"/> or <see cref="FormId"/></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            objectType == gid ? (object)new GId((string)reader.Value) : new FormId((string)reader.Value);
        /// <summary>
        /// Returns whether the converter supports converting the given type.
        /// </summary>
        /// <param name="objectType">The type of object that potentially can be converted</param>
        /// <returns>Type can be converted</returns>
        public override bool CanConvert(Type objectType) =>
            objectType == gid;
    }
    /// <summary>
    /// Converts a value to <see cref="GId"/> where it is expected.
    /// </summary>
    public class GIdConverter : TypeConverter
    {
        /// <summary>
        /// Whether the type can be converted to <see cref="GId"/>.
        /// </summary>
        /// <param name="context">The descriptor context for converter</param>
        /// <param name="sourceType">The type found in the source</param>
        /// <returns>Can convert</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);
        /// <summary>
        /// Converts string to <see cref="GId"/>.
        /// </summary>
        /// <param name="context">The descriptor context for converter</param>
        /// <param name="culture">The current date localization culture</param>
        /// <param name="value">The string to convert</param>
        /// <returns><see cref="GId"/> from string</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) =>
            new GId((string)value);
    }
    /// <summary>
    /// Converts a value to <see cref="FormId"/> where it is expected.
    /// </summary>
    public class FormIdConverter : TypeConverter
    {
        /// <summary>
        /// Whether the type can be converted to <see cref="FormId"/>.
        /// </summary>
        /// <param name="context">The descriptor context for converter</param>
        /// <param name="sourceType">The type found in the source</param>
        /// <returns>Can convert</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);
        /// <summary>
        /// Converts string to <see cref="FormId"/>.
        /// </summary>
        /// <param name="context">The descriptor context for converter</param>
        /// <param name="culture">The current date localization culture</param>
        /// <param name="value">The string to convert</param>
        /// <returns><see cref="FormId"/> from string</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) =>
            new FormId((string)value);
    }
}