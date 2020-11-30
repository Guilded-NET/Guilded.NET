namespace Guilded.NET.Util {
    /// <summary>
    /// Socket response by Guilded.
    /// </summary>
    public class SocketMessage {
        /// <summary>
        /// Number at the start of Guilded message body.
        /// </summary>
        /// <value>Unsigned Integer</value>
        public uint Number {
            get; set;
        }
        /// <param name="number">Number of the Socket Message</param>
        public SocketMessage(uint number) =>
            Number = number;
    }
}