using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events {
    /// <summary>
    /// An object with an ID.
    /// </summary>
    public struct WithId<T> {
        /// <summary>
        /// ID of this object.
        /// </summary>
        /// <value>ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public T Id {
            get; set;
        }
    }
}