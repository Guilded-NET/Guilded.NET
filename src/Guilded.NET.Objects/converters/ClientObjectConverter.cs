using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Converters
{
    /// <summary>
    /// Assigns client to every client object.
    /// </summary>
    public class ClientObjectConverter : JsonConverter
    {
        static readonly Type clientObject = typeof(ClientObject);
        /// <summary>
        /// Client to assign to ClientObject instances.
        /// </summary>
        /// <value>Parent</value>
        public IGuildedClient Client
        {
            get; set;
        }
        /// <summary>
        /// If this converter can write.
        /// </summary>
        /// <value>False</value>
        public override bool CanWrite
        {
            get => false;
        }
        /// <summary>
        /// Assigns client to every client object.
        /// </summary>
        /// <param name="client">Client to assign to ClientObject instances</param>
        public ClientObjectConverter(IGuildedClient client) =>
            Client = client;
        /// <summary>
        /// Writes client object instance to JSON.
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value of the property</param>
        /// <param name="serializer">Serializer used</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteValue(JObject.FromObject(value));
        /// <summary>
        /// Assigns client to every client object.
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Type of the object</param>
        /// <param name="existingValue">Value which already exists in the property</param>
        /// <param name="serializer">Serializer used</param>
        /// <returns>ClientObject with client assigned</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Gets index of this converter to insert it back later
            int index = serializer.Converters.IndexOf(this);
            // Temporarily removes this serializer
            serializer.Converters.Remove(this);
            // Deserializes the object without this converter
            ClientObject clientObject = (ClientObject)serializer.Deserialize(reader, objectType);
            // Assigns client to the client object
            clientObject.ParentClient = Client;
            // Adds it back
            serializer.Converters.Insert(index, this);
            // Returns client object
            return clientObject;
        }
        /// <summary>
        /// Checks if this converter can convert given type.
        /// </summary>
        /// <param name="objectType">Any type</param>
        /// <returns>If type is subclass of ClientObject</returns>
        public override bool CanConvert(Type objectType) =>
            objectType.IsSubclassOf(clientObject);
    }
}