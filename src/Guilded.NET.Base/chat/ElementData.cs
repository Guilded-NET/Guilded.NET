using System;
using System.Collections.Generic;
using Guilded.NET.Base.Teams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Base.Chat
{
    using Embeds;
    /// <summary>
    /// An additional data of a <see cref="ChatElement"/>.
    /// </summary>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ElementData : BaseObject
    {
        /// <summary>
        /// The type of this node.
        /// </summary>
        /// <value>Type?</value>
        public string Type
        {
            get; set;
        }
        /// <summary>
        /// The list of embeds the <see cref="Guilded.NET.Base.Chat.ChatEmbed"/> node has.
        /// </summary>
        /// <value>List of embeds?</value>
        public IList<Embed> Embeds
        {
            get; set;
        }
        /// <summary>
        /// The information about the emote used.
        /// </summary>
        /// <value>Emote info?</value>
        [JsonProperty("reaction")]
        public EmoteInfo Emote
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the form/poll.
        /// </summary>
        /// <value>Form ID?</value>
        [JsonProperty("customFormId")]
        public uint? FormId
        {
            get; set;
        }
        /// <summary>
        /// The information about the mention that was used.
        /// </summary>
        /// <value>Mention data?</value>
        public MemberMentionData Mention
        {
            get; set;
        }
        /// <summary>
        /// The information about the mention that was used.
        /// </summary>
        /// <value>Channel mention data?</value>
        public ChannelMentionData Channel
        {
            get; set;
        }
        /// <summary>
        /// The language this codeblock is highlighted in.
        /// </summary>
        /// <value>Highlighting language?</value>
        public string Language
        {
            get; set;
        }
        /// <summary>
        /// The link that hyperlink references.
        /// </summary>
        /// <value>URL?</value>
        public Uri Href
        {
            get; set;
        }
        /// <summary>
        /// The URL link to the image referenced.
        /// </summary>
        /// <value>URL?</value>
        public Uri Src
        {
            get; set;
        }
        /// <summary>
        /// Who created the post reply header is replying to or who did the action system message is referring to.
        /// </summary>
        /// <value>User ID?</value>
        public GId? CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// The owner of the action.
        /// </summary>
        /// <value>User ID?</value>
        public GId? OwnerId
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the user whom was affected by the action.
        /// </summary>
        /// <value>User ID?</value>
        public GId? UserId
        {
            get; set;
        }
        /// <summary>
        /// The new name of the channel.
        /// </summary>
        /// <value>Name?</value>
        public string NewName
        {
            get; set;
        }
        /// <summary>
        /// The old name of the channel.
        /// </summary>
        /// <value>Name?</value>
        public string OldName
        {
            get; set;
        }
        /// <summary>
        /// The type of the first ancestor channel of the thread.
        /// </summary>
        /// <value>Channel type?</value>
        public ChannelType? OriginatingContentType
        {
            get; set;
        }
        /// <summary>
        /// The link of the message thread originates from.
        /// </summary>
        /// <value>URL?</value>
        public Uri OriginatingUrl
        {
            get; set;
        }
        /// <summary>
        /// The identifier of the post reply header is replying to.
        /// </summary>
        /// <value>Post ID?</value>
        public uint? PostId
        {
            get; set;
        }
        /// <summary>
        /// The list of URLs that are shared in the message document.
        /// </summary>
        /// <value>List of URLs?</value>
        public IList<Uri> ShareUrls
        {
            get; set;
        }
        /// <summary>
        /// The URL that is referenced in content embed.
        /// </summary>
        /// <value>URL?</value>
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// Additional data that overflowed and weren't included as a property.
        /// </summary>
        /// <value>Data</value>
        [JsonExtensionData]
        public IDictionary<string, JToken> Additional
        {
            get; set;
        }
    }
}