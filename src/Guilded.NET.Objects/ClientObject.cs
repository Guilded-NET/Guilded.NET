using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// BaseObject which has a client as a parent.
    /// </summary>
    public abstract class ClientObject: BaseObject {
        /// <summary>
        /// Client this object was created by.
        /// </summary>
        /// <value>Parent</value>
        [JsonIgnore]
        public IGuildedClient ParentClient {
            get; set;
        }
    }
}