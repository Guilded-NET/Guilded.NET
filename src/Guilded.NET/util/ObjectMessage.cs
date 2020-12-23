using Newtonsoft.Json.Linq;

namespace Guilded.NET.Util {
    /// <summary>
    /// Websocket message given by 
    /// </summary>
    public class ObjectMessage: SocketMessage {
        /// <summary>
        /// Given object by Guilded Websocket client.
        /// </summary>
        /// <value>JSON Object</value>
        public JObject Object {
            get; set;
        }
        /// <param name="number">Number of the Socket Message</param>
        /// <param name="obj">Object of the Socket Message</param>
        public ObjectMessage(uint number, JObject obj): base(number) =>
            Object = obj;
    }
}