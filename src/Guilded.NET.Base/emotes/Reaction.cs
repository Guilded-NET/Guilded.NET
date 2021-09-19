using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Emote used in a reaction.
    /// </summary>
    /// <seealso cref="Emote"/>
    /// <seealso cref="Chat.Message"/>
    /// <seealso cref="Chat.ChatEmote"/>
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
        /// Gets whether the message was created by a bot or webhook.
        /// </summary>
        /// <remarks>
        /// <para>Whether the message was automatically posted by a bot or a webhook.</para>
        /// <para>This relies on <see cref="CreatedByBot"/> and <see cref="CreatedByWebhook"/> properties.
        /// If one of them is not <see langword="null"/>, <see langword="true"/> will be returned. Otherwise,
        /// <see langword="false"/> will be returned.</para>
        /// </remarks>
        /// <returns>Created by bot or webhook</returns>
        [JsonIgnore]
        public bool ByBot => !(CreatedByBot is null) || !(CreatedByWebhook is null);
        #endregion

        #region Overrides
        /// <summary>
        /// Returns whether this and <paramref name="obj"/> are equal to each other.
        /// </summary>
        /// <param name="obj">Another object to compare</param>
        /// <returns>Are equal</returns>
        public override bool Equals(object obj) =>
            obj is Reaction reaction && Id == reaction.Id;
        /// <summary>
        /// Gets a hashcode of this object.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() =>
            HashCode.Combine(Id, CreatedAt);
        #endregion
    }
}