using System;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// The provided information about embed author.
    /// </summary>
    /// <remarks>
    /// <para>Defines an author of the quoting message or anything else.</para>
    /// <para>The <see cref="EmbedAuthor"/> feature has following properties:</para>
    /// </remarks>
    /// <example>
    /// <para>An example of using <see cref="EmbedAuthor"/> to display content owner:</para>
    /// <code lang="csharp">
    /// // ... Getting information about a new post...
    /// EmbedAuthor author = new EmbedAuthor(post.Author.Username, post.Author.Avatar, post.Url);
    /// Embed embed = new Embed
    /// {
    ///     Author = author,
    ///     Description = post.Text.Length > 4000
    ///                 ? post.Text.Substring(0, 3997) + "..."
    ///                 : post.Text
    /// };
    /// await client.CreateMessageAsync(channelId, new ChatEmbed(embed));
    /// </code>
    /// </example>
    /// <seealso cref="EmbedFooter"/>
    /// <seealso cref="EmbedProvider"/>
    /// <seealso cref="EmbedField"/>
    /// <seealso cref="EmbedMedia"/>
    public class EmbedAuthor : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The name of an embed author.
        /// </summary>
        /// <remarks>
        /// <para>The name of the <see cref="EmbedAuthor"/> that will be displayed in the embed.</para>
        /// <para>Usually a nickname or username of a Guilded user or user from other platforms.</para>
        /// <para>Any provided Markdown will usually be ignored by the clients such as Guilded official app.</para>
        /// </remarks>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// The URL that author links.
        /// </summary>
        /// <remarks>
        /// <para>The URL that author's name will hyperlink to.</para>
        /// <para>Can be used to open up author's profile or link to the content of the embed.</para>
        /// </remarks>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// The URL to author's icon.
        /// </summary>
        /// <remarks>
        /// <para>The icon displayed at the left side of author's name.</para>
        /// <para>Used to display the icon of the content's author.</para>
        /// </remarks>
        /// <value>URL?</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri IconUrl
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EmbedAuthor"/> with optional URL <paramref name="url"/>.
        /// </summary>
        /// <param name="name">The name of the embed author</param>
        /// <param name="iconUrl">The URL to author's icon</param>
        /// <param name="url">The URL that author links</param>
        /// <exception cref="NullReferenceException"><paramref name="name"/> is null, empty or whitespace</exception>
        public EmbedAuthor(string name, Uri iconUrl = null, Uri url = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NullReferenceException($"Argument {nameof(name)} cannot be null, empty or whitespace.");

            (Name, IconUrl, Url) = (name, iconUrl, url);
        }
        #endregion
    }
}