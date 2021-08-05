using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Embeds;
    /// <summary>
    /// A webhook in a specific channel which automates various things.
    /// </summary>
    public class Webhook : ClientObject
    {
        /// <summary>
        /// An ID of a webhook.
        /// </summary>
        /// <value>Webhook ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// A name of a webhook.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// ID of the channel this webhook is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty(Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this webhook is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// URL of icon this webhook has.
        /// </summary>
        /// <value>Nullable URL</value>
        [JsonProperty("iconUrl", Required = Required.AllowNull)]
        public Uri ProfilePicture
        {
            get; set;
        }
        /// <summary>
        /// When this webhook was deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// If this webhook was deleted.
        /// </summary>
        /// <value>Deleted</value>
        [JsonIgnore]
        public bool IsDeleted => !(DeletedAt is null);
        // /// <summary>
        // /// Posts a message using this webhook.
        // /// </summary>
        // /// <param name="token">Token of this webhook</param>
        // /// <param name="content">Message to send using the webhook</param>
        // /// <param name="embeds">An array of embeds to send</param>
        // public async Task ExecuteAsync(string token, string content = null, params Embed[] embeds) =>
        //     await ParentClient.ExecuteWebhookAsync(Id, token, content, embeds);
        /*/// <summary>
        /// Gets details of this webhook.
        /// </summary>
        /// <returns>Details</returns>
        public async Task<WebhookDetails> GetDetailsAsync() {
            // Gets details
            IDictionary<Guid, WebhookDetails> details = await ParentClient.GetWebhookDetailsAsync(TeamId, Id);
            // Tries to get the key
            details.TryGetValue(Id, out WebhookDetails value);
            // Returns the key it got
            return value;
        }*/
    }
}