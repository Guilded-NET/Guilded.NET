using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Content
{
    /// <summary>
    /// A schedule when a user is available. 
    /// </summary>
    public class Availability : ChannelContent<uint>
    {
        /// <summary>
        /// ID of the user which have set their availability.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("userId", Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// When the availability was updated.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty("updatedAt", Required = Required.Always)]
        public DateTime UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the schedule availability starts.
        /// </summary>
        /// <value>Schedule start</value>
        [JsonProperty("startDate", Required = Required.Always)]
        public DateTime StartDate
        {
            get; set;
        }
        /// <summary>
        /// When the schedule availability ends.
        /// </summary>
        /// <value>Schedule end</value>
        [JsonProperty("endDate", Required = Required.Always)]
        public DateTime EndDate
        {
            get; set;
        }
    }
}