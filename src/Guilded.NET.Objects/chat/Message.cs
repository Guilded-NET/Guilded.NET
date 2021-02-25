using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Message posted in chat.
    /// </summary>
    public class Message : ClientObject, IMessage
    {
        /// <summary>
        /// Message posted in chat.
        /// </summary>
        public Message() =>
            Reactions = new List<Reaction>();
        /// <summary>
        /// ID of the message.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id
        {
            get; set;
        }
        /// <summary>
        /// ID of the channel this message is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(null)]
        public Guid? ChannelId
        {
            get; set;
        }
        /// <summary>
        /// ID of the author.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId AuthorId
        {
            get; set;
        }
        /// <summary>
        /// ID of the webhook which posted this message.
        /// </summary>
        /// <value>Webhook ID</value>
        [JsonProperty("webhookId")]
        [DefaultValue(null)]
        public Guid? WebhookId
        {
            get; set;
        }
        /// <summary>
        /// ID of the bot which posted this message.
        /// </summary>
        /// <value>Bot ID</value>
        [JsonProperty("botId")]
        [DefaultValue(null)]
        public Guid? BotId
        {
            get; set;
        }
        /// <summary>
        /// Type of the message.
        /// </summary>
        /// <value>Message type</value>
        [JsonProperty("type")]
        [DefaultValue(MessageType.Default)]
        public MessageType Type
        {
            get; set;
        }
        /// <summary>
        /// Date of when the message was posted.
        /// </summary>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// Content of the message.
        /// </summary>
        /// <value>Message content</value>
        [JsonProperty("content", Required = Required.Always)]
        public MessageContent Content
        {
            get; set;
        }
        /// <summary>
        /// Type of the channel. Whether it was posted in DMs or in a Team.
        /// </summary>
        /// <value></value>
        [JsonProperty("channelType")]
        [DefaultValue(ChatType.Team)]
        public ChatType ChannelType
        {
            get; set;
        }
        /// <summary>
        /// Whether or not this message was pinned.
        /// </summary>
        /// <value>Pinned</value>
        [JsonProperty("isPinned")]
        [DefaultValue(false)]
        public bool IsPinned
        {
            get; set;
        }
        /// <summary>
        /// ID of the user who pinned this message.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("pinnedBy")]
        [DefaultValue(null)]
        public GId PinnedBy
        {
            get; set;
        }
        /// <summary>
        /// A list of all reactions on this message.
        /// </summary>
        /// <value>Reactions</value>
        [JsonProperty("reactions")]
        public IList<Reaction> Reactions
        {
            get; set;
        }
        /// <summary>
        /// Gets message content nodes.
        /// </summary>
        /// <value>List of Nodes</value>
        [JsonIgnore]
        public IList<Node> Nodes
        {
            get => Content.Nodes;
        }
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public async Task<User> GetAuthorAsync() =>
            await ParentClient.GetUserAsync(AuthorId);
        /// <summary>
        /// Gets owner or author of this message.
        /// </summary>
        /// <returns>Message owner</returns>
        public User GetAuthor() =>
            ParentClient.GetUser(AuthorId);
        /// <summary>
        /// If this message was posted by given user
        /// </summary>
        /// <param name="user">User to check if it's message author</param>
        /// <returns>Message by that user</returns>
        public bool IsMessageOf(User user) =>
            AuthorId == user?.Id;
        /// <summary>
        /// Turns a message into a string.
        /// </summary>
        /// <returns>Message as a string</returns>
        public override string ToString() => Content?.ToString();
        /// <summary>
        /// Generates a new message.
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(IList<Node> nodes) =>
            new NewMessage
            {
                // Generate random ID and give it to the message
                Id = Guid.NewGuid(),
                // Create content for the message
                Content = MessageContent.Generate(nodes)
            };
        /// <summary>
        /// Generates a new message using string.
        /// </summary>
        /// <param name="node">Content of the message</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(Node node) => Generate(new List<Node> { node });
        /// <summary>
        /// Generates a new message using string.
        /// </summary>
        /// <param name="content">Content of the message</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(string content) => Generate(MarkDownText.Generate(content));
        /// <summary>
        /// Generates a new message using embed.
        /// </summary>
        /// <param name="embed">Embed to send</param>
        /// <returns>NewMessage</returns>
        public static NewMessage Generate(Embed embed) => Generate(new List<Node> { EmbedNode.Generate(embed) });
    }
}