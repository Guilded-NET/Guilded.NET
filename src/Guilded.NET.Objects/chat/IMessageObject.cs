namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Interface for all message nodes, objects and other.
    /// </summary>
    public interface IMessageObject {
        /// <summary>
        /// Object type.
        /// </summary>
        /// <value>Object type</value>
        MsgObject Object {
            get; set;
        }
    }
}