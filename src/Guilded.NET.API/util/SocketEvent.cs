using Newtonsoft.Json.Linq;

namespace Guilded.NET.API
{
    /// <summary>
    /// Socket response by Guilded.
    /// </summary>
    public class SocketEvent : ObjectMessage
    {
        /// <summary>
        /// Message type.
        /// </summary>
        /// <value>Type</value>
        public string MessageType
        {
            get; set;
        }
        /// <summary>
        /// Socket response by Guilded.
        /// </summary>
        /// <param name="number">Number of the Socket Message</param>
        /// <param name="obj">Object of the Socket Message</param>
        /// <param name="type">Type of the Guilded event</param>
        public SocketEvent(uint number, JObject obj, string type) : base(number, obj) =>
            MessageType = type;
    }
}