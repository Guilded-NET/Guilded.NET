using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The provided information about embed author.
    /// </summary>
    /// <remarks>
    /// <para>Defines an author of the quoting message or anything else. The <see cref="EmbedAuthor"/> feature has following properties:</para>
    /// </remarks>
    /// <example>
    /// <para>An example of using <see cref="EmbedAuthor"/> to display content owner:</para>
    /// <code language="csharp">
    /// // ... Getting information about a new post...
    /// EmbedAuthor author = new EmbedAuthor(post.Author.Username, post.Author.Avatar, post.Url);
    /// Embed embed = new Embed
    /// {
    ///     Author = author,
    ///     Description = post.Text.Length > 4000
    ///                 ? post.Text.Substring(0, 3997) + "..."
    ///                 : post.Text
    /// };
    /// await client.CreateMessageAsync(channelId, embed);
    /// </code>
    /// </example>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedAuthor : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The name of an embed author.
        /// </summary>
        /// <remarks>
        /// <para>The name of the <see cref="EmbedAuthor"/> that will be displayed in the embed. Usually a nickname or username of a Guilded user or user from other platforms.</para>
        /// <para>The provided Markdown is ignored.</para>
        /// </remarks>
        /// <value>Name</value>
        public string Name { get; set; }
        /// <summary>
        /// The URL that author links.
        /// </summary>
        /// <remarks>
        /// <para>The URL that author's name will hyperlink to.</para>
        /// <para>Can be used to open up author's profile or link to the content of the embed.</para>
        /// </remarks>
        /// <value>URL?</value>
        public Uri? Url { get; set; }
        /// <summary>
        /// The URL to author's icon.
        /// </summary>
        /// <remarks>
        /// <para>The icon displayed at the left side of author's name.</para>
        /// <para>Used to display the icon of the content's author.</para>
        /// </remarks>
        /// <value>URL?</value>
        public Uri? IconUrl { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EmbedAuthor"/> without an icon and without a URL.
        /// </summary>
        /// <param name="name">The name of the embed author</param>
        public EmbedAuthor(string name) =>
            Name = name;
        /// <summary>
        /// Creates a new instance of <see cref="EmbedAuthor"/> with optional URL <paramref name="url"/>.
        /// </summary>
        /// <param name="name">The name of the embed author</param>
        /// <param name="url">The URL that author links</param>
        /// <param name="iconUrl">The URL to author's icon</param>
        [JsonConstructor]
        public EmbedAuthor(
            [JsonProperty(Required = Required.Always)]
            string name,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            Uri? url = null,

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            Uri? iconUrl = null
        ) : this(name) =>
            (IconUrl, Url) = (iconUrl, url);
        /// <summary>
        /// Creates a new instance of <see cref="EmbedAuthor"/> with optional URL <paramref name="url"/>.
        /// </summary>
        /// <param name="name">The name of the embed author</param>
        /// <param name="url">The URL that author links</param>
        /// <param name="iconUrl">The URL to author's icon</param>
        /// <exception cref="UriFormatException"><paramref name="url"/> or <paramref name="iconUrl"/> have bad <see cref="Uri"/> formatting</exception>
        public EmbedAuthor(string name, string? url = null, string? iconUrl = null) : this(name, iconUrl is null ? null : new Uri(iconUrl), url is null ? null : new Uri(url)) { }
        #endregion
    }
}