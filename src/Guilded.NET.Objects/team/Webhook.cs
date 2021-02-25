using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams
{
    /// <summary>
    /// A webhook in a specific channel which automates various things.
    /// </summary>
    public class Webhook : ClientObject
    {
        /// <summary>
        /// A webhook in a specific channel which automates various things.
        /// </summary>
        public Webhook() =>
            Token = null;
        /// <summary>
        /// An ID of a webhook.
        /// </summary>
        /// <value>Webhook ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// A name of a webhook.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// A token of a webhook. Don't show this to others.<br/> This property will be hidden if you do not have manage webhooks permission.
        /// </summary>
        /// <value>Webhook token</value>
        [JsonProperty("token")]
        public string Token
        {
            get; set;
        }
        /// <summary>
        /// ID of the channel this webhook is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this webhook is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// URL of icon this webhook has.
        /// </summary>
        /// <value>Nullable URL</value>
        [JsonProperty("iconUrl", Required = Required.AllowNull)]
        public Uri Avatar
        {
            get; set;
        }
        /// <summary>
        /// Who created this webhook.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this webhook was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When this webhook was deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty("deletedAt", Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// If this webhook was deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonIgnore]
        public bool IsDeleted
        {
            get => DeletedAt != null;
        }
        /// <summary>
        /// A link to this webhook.
        /// </summary>
        /// <value>Webhook link</value>
        [JsonIgnore]
        public string Link
        {
            get => $"https://media.guilded.gg/webhooks/{Id}/{Token}";
        }
    }
}