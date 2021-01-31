using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Guilded.NET.Objects.Converters {
    /// <summary>
    /// Converts enum to string and vice versa.
    /// </summary>
    public class EnumConverter: JsonConverter {
        static readonly Type flags = typeof(FlagsAttribute);
        static readonly Type enumMember = typeof(EnumMemberAttribute);
        /// <summary>
        /// Writes enum to the string.
        /// </summary>
        /// <param name="writer">JsonWriter</param>
        /// <param name="value">Enum</param>
        /// <param name="serializer">Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            // Gets value's type
            Type type = value.GetType();
            // Gets value as enum
            Enum val = (Enum)value;
            // If this value is a value of flags enum
            bool isFlag = val.GetType().CustomAttributes.FirstOrDefault(x => x.AttributeType == flags) != null;
            // Writes enum as integer if it's value of flags enum
            if(isFlag) writer.WriteValue(JToken.FromObject(Convert.ToUInt32(val)));
            // Else, writes enum as string
            else {
                // Value as string
                string valStr = value.ToString();
                // Gets that enum member
                MemberInfo member = type.GetMember(valStr).FirstOrDefault(x => x.DeclaringType == type);
                // Gets EnumMember attribute
                EnumMemberAttribute attr = (EnumMemberAttribute)member.GetCustomAttributes(enumMember, false).FirstOrDefault();
                // If attribute and value aren't null
                if(attr != null && attr?.Value != null) writer.WriteValue(JToken.FromObject(attr.Value));
                // If one of them is null
                else writer.WriteValue(JToken.FromObject(valStr));
            }
        }
        /// <summary>
        /// Converts string to enum.
        /// </summary>
        /// <param name="reader">Reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Previous property value</param>
        /// <param name="serializer">Serializer</param>
        /// <returns>GLongId or GId</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            // Gets value
            JToken token = JToken.Load(reader);
            // If it's integer, get it as integer and turn it into enum
            if(token.Type == JTokenType.Integer) return Enum.ToObject(objectType, token.Value<int>());
            // If it's string, then...
            else if(token.Type == JTokenType.String) {
                // Gets enum value's name
                string name = token.Value<string>();
                // Gets values
                IEnumerable<Enum> vals = Enum.GetValues(objectType).OfType<Enum>();
                // First enum value by that name
                Enum first = vals.FirstOrDefault(x => x.ToString() == name);
                // If enum has that value
                if(first != default) return first;
                // If it doesn't
                else {
                    // Gets all members in the enum
                    MemberInfo[] members = objectType.GetMembers();
                    // Gets first member with specific name
                    MemberInfo member = members.FirstOrDefault(x =>
                        x.GetCustomAttributes(enumMember, false).FirstOrDefault(y => ((EnumMemberAttribute)y).Value == name) != null
                    );
                    // If member isn't null, get its value
                    if(member != null) return objectType.GetEnumValues().OfType<Enum>().FirstOrDefault(x => x.ToString() == member.Name);
                    // If it is, get first enum's value
                    else return vals.First();    
                }
            } else return Enum.GetValues(objectType).OfType<Enum>().FirstOrDefault();
        }
        /// <summary>
        /// Whether or not this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Type of the object</param>
        /// <returns>Can convert the type</returns>
        public override bool CanConvert(Type objectType) => objectType.IsEnum;
    }
}