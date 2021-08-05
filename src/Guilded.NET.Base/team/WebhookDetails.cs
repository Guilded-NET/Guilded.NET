using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Webhook details: token, createdBy, createdAt.
    /// </summary>
    public class WebhookDetails : ClientObject
    {
        /// <summary>
        /// Who created this webhook.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this webhook was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// A token of a webhook. Don't show this to others.<br/> This property will be hidden if you do not have manage webhooks permission.
        /// </summary>
        /// <value>Webhook token?</value>
        public string Token
        {
            get; set;
        }
    }
}