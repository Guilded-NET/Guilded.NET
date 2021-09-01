using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class ThreadChannel : TeamChannel
    {
        /// <summary>
        /// ID of the channel which thread was created in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid OriginatingChannelId
        {
            get; set;
        }
        /// <summary>
        /// Type of channel where thread is originating in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("originatingChannelContentType", Required = Required.Always)]
        public ChannelType OriginatingContentType
        {
            get; set;
        }
        /// <summary>
        /// ID of the message from which thread is originating.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid ThreadMessageId
        {
            get; set;
        }
        /// <summary>
        /// Turns a thread into string.
        /// </summary>
        /// <returns>Thread as a string</returns>
        public override string ToString() =>
            $"Thread {Id}: {Name}";
        /// <summary>
        /// Gets thread's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            base.GetHashCode() + 10;
    }
}