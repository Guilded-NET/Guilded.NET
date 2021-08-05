using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// List of channels in a team.
    /// </summary>
    public class ChannelList : BaseObject
    {
        /// <summary>
        /// List of channel categories.
        /// </summary>
        /// <value>List of categories</value>
        public IList<Category> Categories
        {
            get; set;
        }
        /// <summary>
        /// List of temporal channels.
        /// </summary>
        /// <value>List of temporal channels</value>
        public IList<ThreadChannel> TemporalChannels
        {
            get; set;
        }
        /// <summary>
        /// List of channels in group or team.
        /// </summary>
        /// <value>List of channels</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Channel> Channels
        {
            get; set;
        }
    }
}