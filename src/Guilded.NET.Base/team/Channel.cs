using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Chat;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class Channel : TeamChannel
    {
        #region JSON properties
        /// <summary>
        /// A description/topic of this channel.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// Settings of this channel.
        /// </summary>
        /// <value>Settings</value>
        [JsonProperty(Required = Required.AllowNull)]
        public ChannelSettings Settings
        {
            get; set;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() =>
            $"Channel {Id}: {Name}";
        /// <summary>
        /// Gets channel's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            base.GetHashCode() - 50;
        #endregion
    }
}