using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Emote used in a reaction.
    /// </summary>
    public class Reaction : ClientObject
    {
        #region JSON properties
        /// <summary>
        /// The identifier of the emote reacted with.
        /// </summary>
        /// <value>Emote ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// The date of when this reaction was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the author of this reaction.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the webhook that created this reaction.
        /// </summary>
        /// <value>Webhook ID?</value>
        [JsonProperty("createdByWebhookId")]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the bot that created this reaction.
        /// </summary>
        /// <value>Bot ID?</value>
        [JsonProperty("createdByBotId")]
        public Guid? CreatedByBot
        {
            get; set;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the reaction was created by a bot or webhook.
        /// </summary>
        /// <returns>Created by bot</returns>
        [JsonIgnore]
        public bool ByBot => !(CreatedByBot is null) && !(CreatedByWebhook is null);
        #endregion

        #region Overrides
        /// <summary>
        /// Checks if object is equal to this reaction.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Equal</returns>
        public override bool Equals(object obj) =>
            obj is Reaction reaction && Id == reaction.Id;
        /// <summary>
        /// Gets a hashcode of this reaction.
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, CreatedAt);
        #endregion
    }
}