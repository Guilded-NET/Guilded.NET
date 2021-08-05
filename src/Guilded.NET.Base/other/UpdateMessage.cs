using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    using Chat;
    /// <summary>
    /// An update announcement message.
    /// </summary>
    public class UpdateMessage : BaseObject
    {
        /// <summary>
        /// ID of the update post.
        /// </summary>
        /// <value>Update post ID</value>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// The content of the update; main body.
        /// </summary>
        /// <value>Content</value>
        [JsonProperty(Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// When the post was originally created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When is the last time post got updated.
        /// </summary>
        /// <value>Updated at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the post was published for everyone to see.
        /// </summary>
        /// <value>Published at</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime PublishedAt
        {
            get; set;
        }
        /// <summary>
        /// The text of the button in the update post.
        /// </summary>
        /// <value>Button text</value>
        [JsonProperty("ctaButtonText")]
        public string ButtonText
        {
            get; set;
        }
        /// <summary>
        /// The link which the button uses.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("ctaButtonLink")]
        public Uri ButtonLink
        {
            get; set;
        }
    }
}