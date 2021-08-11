using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once XP is given to a set of users.
    /// </summary>
    public class XpAddedEvent : BaseObject
    {
        /// <summary>
        /// The identifiers of all users that received that amount of XP.
        /// </summary>
        /// <value>List of user IDs</value>
        [JsonProperty("userIds", Required = Required.Always)]
        public ISet<GId> Users
        {
            get; set;
        }
        /// <summary>
        /// The amount of XP given to the users.
        /// </summary>
        /// <value>XP</value>
        [JsonProperty(Required = Required.Always)]
        public long Amount
        {
            get; set;
        }
    }
}