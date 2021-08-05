using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A flow bot in a team.
    /// </summary>
    public class TeamBot : ClientObject
    {
        /// <summary>
        /// An ID of this bot.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// A name of this bot.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this bot is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// URL of icon this bot has.
        /// </summary>
        /// <value>Nullable URL</value>
        [JsonProperty("iconUrl", Required = Required.AllowNull)]
        public Uri ProfilePicture
        {
            get; set;
        }
        /// <summary>
        /// Who created this bot.
        /// </summary>
        /// <value>User ID?</value>
        public GId? CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this bot was created.
        /// </summary>
        /// <value>Created at?</value>
        public DateTime? CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When this bot was deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// If this bot was deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonIgnore]
        public bool IsDeleted => !(DeletedAt is null);
    }
}