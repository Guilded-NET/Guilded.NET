using Newtonsoft.Json.Linq;

namespace Guilded.NET.API
{
    /// <summary>
    /// Socket response by Guilded.
    /// </summary>
    public class ObjectMessage : SocketMessage
    {
        /// <summary>
        /// Given object by Guilded Websocket client.
        /// </summary>
        /// <value>JSON Object</value>
        public JObject Object
        {
            get; set;
        }
        /// <summary>
        /// Socket response by Guilded.
        /// </summary>
        /// <param name="number">Number of the Socket Message</param>
        /// <param name="obj">Object of the Socket Message</param>
        public ObjectMessage(uint number, JObject obj) : base(number) =>
            Object = obj;
    }
}