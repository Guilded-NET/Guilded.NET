using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A bot based on flows.
    /// </summary>
    public class Flowbot : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// ID of the bot.
        /// </summary>
        /// <value>Flowbot ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// A display name of this flowbot.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// If the bot is enabled and will respond.
        /// </summary>
        /// <value>Flowbot enabled</value>
        [JsonProperty(Required = Required.Always)]
        public bool Enabled
        {
            get; set;
        }
        /// <summary>
        /// ID of the team where the flowbot is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// Icon of the bot.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("iconUrl", Required = Required.AllowNull)]
        public Uri ProfilePicture
        {
            get; set;
        }
        /// <summary>
        /// When the flowbot was deleted.
        /// </summary>
        /// <value>Date?</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <summary>
        /// Whether this flowbot is deleted.
        /// </summary>
        /// <value>Is deleted</value>
        [JsonIgnore]
        public bool IsDeleted => !(DeletedAt is null);
        #endregion
    }
}