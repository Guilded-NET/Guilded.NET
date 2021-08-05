using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A team tournament.
    /// </summary>
    public class Tournament : Group
    {
        /// <summary>
        /// Minimum team member count.
        /// </summary>
        /// <value>Minimum member count</value>
        [JsonProperty(Required = Required.Always)]
        public uint MinRosterSize
        {
            get; set;
        }
        /// <summary>
        /// Maximum team member count.
        /// </summary>
        /// <value>Maximum member count</value>
        [JsonProperty(Required = Required.Always)]
        public uint MaxRosterSize
        {
            get; set;
        }
        /// <summary>
        /// When the tournament is starting.
        /// </summary>
        /// <value>Starts at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime StartDate
        {
            get; set;
        }
        /// <summary>
        /// When the tournament will end.
        /// </summary>
        /// <value>Ends at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime EndDate
        {
            get; set;
        }
    }
}