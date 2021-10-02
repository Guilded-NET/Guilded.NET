using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// A custom content embed that represents any kind of information.
    /// </summary>
    /// <remarks>
    /// <para>Represents a custom/rich embed that represents some kind of information. This is usually used in Webhooks to provide an information about a new post or any event that occurred. It can also be used for displaying results from a bot or used as a content instead of plain Markdown. Embeds may look something like quote blocks, but with more content like fields, footers, etc.</para>
    /// </remarks>
    /// <example>
    /// <para>Embeds can be created using constructors, object initializers and fluent interface methods.</para>
    /// <para>This example showcases mixing object initializer along with constructor parameters:</para>
    /// <code language="csharp">
    /// Embed embed = new Embed(thumbnail: imageUrl)
    /// {
    ///     Title = "Title here",
    ///     Description = "Description here"
    /// };
    /// </code>
    /// <para>The example below showcases using fluent interface methods:</para>
    /// <code language="csharp">
    /// Embed embed = new Embed()
    ///     .SetTitle("Title here")
    ///     .SetDescription("Description here")
    ///     .SetFooter("Footer text here");
    /// </code>
    /// </example>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Embed : BaseObject
    {
        private const int fieldLimit = 25;

        #region JSON properties
        /// <summary>
        /// The title of the embed.
        /// </summary>
        /// <remarks>
        /// <para>A short text that appears above description.</para>
        /// <para>The name or the title of the content or the post. This text can be hyperlinked using <see cref="Url"/>. The provided Markdown will be ignored.</para>
        /// </remarks>
        /// <value>Title?</value>
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// The URL of the embed.
        /// </summary>
        /// <remarks>
        /// <para>Defines the URL to the content this embed displays.</para>
        /// <para>This is displayed as <see cref="Title"/> but as a hyperlink that links this URL by the Guilded official app.</para>
        /// </remarks>
        /// <value>URL?</value>
        public Uri Url
        {
            get; set;
        }
        /// <summary>
        /// The description text of the embed.
        /// </summary>
        /// <remarks>
        /// <para>A piece of text that appears in the middle of the embed.</para>
        /// <para>This can have formatting using Markdown.</para>
        /// </remarks>
        /// <value>Description?</value>
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// The author of the embed.
        /// </summary>
        /// <remarks>
        /// <para>Defines an author of the quoting message or anything else.</para>
        /// </remarks>
        /// <value>Author?</value>
        public EmbedAuthor Author
        {
            get; set;
        }
        /// <summary>
        /// The colour of the embed.
        /// </summary>
        /// <remarks>
        /// <para>The display colour of the embed.</para>
        /// <para>This is displayed as left-side border in the official Guilded client, but may be displayed differently in other clients.</para>
        /// </remarks>
        /// <value>Colour?</value>
        [JsonConverter(typeof(DecimalColorConverter))]
        public Color? Color
        {
            get; set;
        }
        /// <summary>
        /// The thumbnail image of the embed.
        /// </summary>
        /// <remarks>
        /// <para>An image that represents a thumbnail.</para>
        /// <para>This is displayed as image at the right of the embed and as square in the official Guilded app.</para>
        /// </remarks>
        /// <value>Media?</value>
        public EmbedMedia Thumbnail
        {
            get; set;
        }
        /// <summary>
        /// The image of the embed.
        /// </summary>
        /// <remarks>
        /// <para>An image that will appear in the embed.</para>
        /// <para>This is displayed as an image that appears at the bottom of the embed and above a footer in the official Guilded app.</para>
        /// </remarks>
        /// <value>Media?</value>
        public EmbedMedia Image
        {
            get; set;
        }
        /// <summary>
        /// The video of the embed.
        /// </summary>
        /// <remarks>
        /// <para>A video that will appear in the embed.</para>
        /// <para>This is displayed as a video that appears at the bottom of the embed and above a footer in the official Guilded app.</para>
        /// </remarks>
        /// <value>Media?</value>
        public EmbedMedia Video
        {
            get; set;
        }
        /// <summary>
        /// The list of fields in this embed.
        /// </summary>
        /// <remarks>
        /// <para>Displays the list of fields with their own descriptions/values and titles/names.</para>
        /// <para>Fields can be both inline and blocks.</para>
        /// </remarks>
        /// <value>List of fields?</value>
        public IList<EmbedField> Fields
        {
            get; set;
        }
        /// <summary>
        /// The footer of the embed.
        /// </summary>
        /// <remarks>
        /// <para>The bottom area of an embed that provides further information about anything.</para>
        /// <para>Footers can also have timestamps, but that can be used by setting <see cref="Timestamp"/> property. Timestamps are not officially part of footers, but that's the most common way they are displayed by the clients and official Guilded app.</para>
        /// </remarks>
        /// <value>Footer?</value>
        public EmbedFooter Footer
        {
            get; set;
        }
        /// <summary>
        /// The timestamp of the embed.
        /// </summary>
        /// <remarks>
        /// <para>The <see cref="DateTime"/> that will be shown in the embed.</para>
        /// <para>This is usually displayed in the <see cref="EmbedFooter"/> by clients like Guilded official client, but may be displayed elsewhere in other clients.</para>
        /// </remarks>
        /// <value>Date?</value>
        public DateTime? Timestamp
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new empty instance of <see cref="Embed"/>.
        /// </summary>
        public Embed() { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with given parameters converted to <see cref="EmbedMedia"/>.
        /// </summary>
        /// <param name="image">The image of the embed</param>
        /// <param name="video">The video of the embed</param>
        /// <param name="thumbnail">The thumbnail image of the embed</param>
        public Embed(Uri image = null, Uri video = null, Uri thumbnail = null) =>
            (Image, Video, Thumbnail) = (EmbedMedia.CreateOrNull(image), EmbedMedia.CreateOrNull(video), EmbedMedia.CreateOrNull(thumbnail));
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with URL <paramref name="url"/>.
        /// </summary>
        /// <param name="url">The URL of the embed</param>
        public Embed(Uri url) =>
            Url = url;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with colour <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The colour of the embed</param>
        public Embed(Color color) =>
            Color = color;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with colour <paramref name="argb"/>.
        /// </summary>
        /// <param name="argb">The colour of the embed in RGB format</param>
        public Embed(int argb) : this(System.Drawing.Color.FromArgb(argb)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with channels
        /// <paramref name="red"/>, <paramref name="green"/> and <paramref name="blue"/> of <see cref="Color"/> property.
        /// </summary>
        /// <param name="red">The red channel of the <see cref="Color"/></param>
        /// <param name="green">The green channel of <see cref="Color"/></param>
        /// <param name="blue">The blue channel of <see cref="Color"/></param>
        public Embed(int red, int green, int blue) : this(System.Drawing.Color.FromArgb(red, green, blue)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with description <paramref name="description"/>.
        /// </summary>
        /// <param name="description">The description text of the embed</param>
        public Embed(string description) =>
            Description = description;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        public Embed(string title, string description) : this(description) =>
            Title = title;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with title <paramref name="title"/> and URL <paramref name="url"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        public Embed(string title, Uri url, string description) : this(title, description) =>
            Url = url;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/>.
        /// </summary>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string description, EmbedMedia image) : this(description) =>
            Image = image;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/>.
        /// </summary>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string description, Uri image) : this(description, new EmbedMedia(image)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string title, string description, EmbedMedia image) : this(title, description) =>
            Image = image;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string title, Uri url, string description, EmbedMedia image) : this(title, description, image) =>
            Url = url;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string title, string description, Uri image) : this(title, description, new EmbedMedia(image)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with image <paramref name="image"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="image">The image of the embed</param>
        public Embed(string title, Uri url, string description, Uri image) : this(title, url, description, new EmbedMedia(image)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with list of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="fields">The list of fields in this embed</param>
        public Embed(IList<EmbedField> fields) =>
            Fields = fields;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with array of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="fields">The array of fields in this embed</param>
        public Embed(params EmbedField[] fields) : this(fields.ToList()) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with list of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="description">The description text of the embed</param>
        /// <param name="fields">The list of fields in this embed</param>
        public Embed(string description, IList<EmbedField> fields) : this(description) =>
            Fields = fields;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with array of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="description">The description text of the embed</param>
        /// <param name="fields">The array of fields in this embed</param>
        public Embed(string description, params EmbedField[] fields) : this(description, fields.ToList()) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with list of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="fields">The list of fields in this embed</param>
        public Embed(string title, string description, IList<EmbedField> fields) : this(title, description) =>
            Fields = fields;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with array of fields <paramref name="fields"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="fields">The array of fields in this embed</param>
        public Embed(string title, string description, params EmbedField[] fields) : this(title, description, fields.ToList()) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The footer of the embed</param>
        public Embed(string title, string description, EmbedFooter footer) : this(title, description) =>
            Footer = footer;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The text of the embed footer</param>
        public Embed(string title, string description, string footer) : this(title, description, new EmbedFooter(footer)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The footer of the embed</param>
        public Embed(string title, Uri url, string description, EmbedFooter footer) : this(title, description, footer) =>
            Url = url;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The text of the embed footer</param>
        public Embed(string title, Uri url, string description, string footer) : this(title, url, description, new EmbedFooter(footer)) { }
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The footer of the embed</param>
        /// <param name="timestamp">The timestamp of the embed footer</param>
        public Embed(string title, string description, EmbedFooter footer, DateTime timestamp) : this(title, description, footer) =>
            Timestamp = timestamp;
        /// <summary>
        /// Creates a new instance of <see cref="Embed"/> with footer <paramref name="footer"/> and title <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title of the embed</param>
        /// <param name="url">The URL of the embed</param>
        /// <param name="description">The description text of the embed</param>
        /// <param name="footer">The footer of the embed</param>
        /// <param name="timestamp">The timestamp of the embed footer</param>
        public Embed(string title, Uri url, string description, EmbedFooter footer, DateTime timestamp) : this(title, description, footer, timestamp) =>
            Url = url;
        #endregion

        #region Additional
        /// <summary>
        /// Sets the title as the given parameter.
        /// </summary>
        /// <param name="title">The text of the title</param>
        /// <exception cref="NullReferenceException"><paramref name="title"/> is <see langword="null"/>, empty or whitespace</exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new NullReferenceException($"Argument {nameof(title)} cannot be null, empty or whitespace.");
            Title = title;
            return this;
        }
        /// <summary>
        /// Sets the url as the given parameter.
        /// </summary>
        /// <param name="url">The URL that title will link</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetUrl(Uri url)
        {
            Url = url;
            return this;
        }
        /// <summary>
        /// Sets the url as a given parameter.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new <see cref="Uri"/> instance from <paramref name="url"/> parameter and sets it to <see cref="Url"/> property.</para>
        /// </remarks>
        /// <param name="url">The URL that title will link</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetUrl(string url) =>
            SetUrl(new Uri(url));
        /// <summary>
        /// Sets the description as the given parameter.
        /// </summary>
        /// <param name="description">Embed's description</param>
        /// <exception cref="NullReferenceException"><paramref name="description"/> is <see langword="null"/>, empty or whitespace</exception>
        /// <exception cref="OverflowException"><paramref name="description"/> exceeds 4000 character limit</exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new NullReferenceException($"Argument {nameof(description)} cannot be null, empty or whitespace.");
            else if (description.Length > 4000)
                throw new OverflowException($"Argument {nameof(description)} cannot be more than 4'000 characters.");
            Description = description;
            return this;
        }
        /// <summary>
        /// Sets the description as a given parameter.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Description"/> as a string equivalent to <paramref name="description"/> parameter.</para>
        /// </remarks>
        /// <param name="description">Embed's description</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetDescription(object description) =>
            SetDescription(description.ToString());
        /// <summary>
        /// Sets the author as the given parameter.
        /// </summary>
        /// <param name="author">Author to be set to</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetAuthor(EmbedAuthor author)
        {
            Author = author;
            return this;
        }
        /// <summary>
        /// Sets the author as a given parameter.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Author"/> as a new <see cref="EmbedAuthor"/> instance from given parameters.</para>
        /// </remarks>
        /// <param name="name">Name of the author</param>
        /// <param name="iconUrl">URL of the image</param>
        /// <param name="url">URL of the author's name</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetAuthor(string name, Uri iconUrl = null, Uri url = null) =>
            SetAuthor(new EmbedAuthor(name, iconUrl, url));
        /// <summary>
        /// Adds the given fields to the embed.
        /// </summary>
        /// <remarks>
        /// <para>Adds the <paramref name="fields"/> parameter to <see cref="Fields"/> property.</para>
        /// <para>The max field limit per embed is 25. If 25 field limit is exceeded, <see cref="OverflowException"/> will be thrown.</para>
        /// </remarks>
        /// <param name="fields">The list of fields to be added</param>
        /// <exception cref="OverflowException">When the combined field list exceeds max field limit of <c>25</c></exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed AddFields(IList<EmbedField> fields)
        {
            // Don't allow >25 fields
            if ((Fields?.Count + fields?.Count) > fieldLimit || Fields?.Count > 25)
                throw new OverflowException("Cannot add more than 25 fields to the embed");
            else if (Fields is null)
                Fields = fields;
            else
                Fields = Fields.Concat(fields).ToList();
            return this;
        }
        /// <summary>
        /// Adds the given fields to the embed.
        /// </summary>
        /// <remarks>
        /// <para>Adds the <paramref name="fields"/> parameter to <see cref="Fields"/> property.</para>
        /// <para>The max field limit per embed is 25. If 25 field limit is exceeded, <see cref="OverflowException"/> will be thrown.</para>
        /// </remarks>
        /// <param name="fields">The array of fields to be added</param>
        /// <exception cref="OverflowException">When the combined field list exceeds max field limit of <c>25</c></exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed AddFields(params EmbedField[] fields) =>
            AddFields((IList<EmbedField>)fields);
        /// <summary>
        /// Adds the given field to the embed.
        /// </summary>
        /// <remarks>
        /// <para>Adds the <paramref name="field"/> parameter to <see cref="Fields"/> property.</para>
        /// <para>The max field limit per embed is 25. If 25 field limit is exceeded, <see cref="OverflowException"/> will be thrown.</para>
        /// </remarks>
        /// <param name="field">A field to add</param>
        /// <exception cref="OverflowException">When the combined field list exceeds max field limit of <c>25</c></exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed AddField(EmbedField field) =>
            AddFields(field);
        /// <summary>
        /// Adds the given field to the embed.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="EmbedField"/> with given parameters and adds it to <see cref="Fields"/> property.</para>
        /// <para>The max field limit per embed is 25. If 25 field limit is exceeded, <see cref="OverflowException"/> will be thrown.</para>
        /// </remarks>
        /// <param name="name">The title of the field</param>
        /// <param name="value">The description of the field</param>
        /// <param name="inline">If this field should be inline</param>
        /// <exception cref="OverflowException">When the combined field list exceeds max field limit of <c>25</c></exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed AddField(string name, string value, bool inline = false) =>
            AddField(new EmbedField(name, value, inline));
        /// <summary>
        /// Adds the given field to the embed.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="EmbedField"/> with given parameters and adds it to <see cref="Fields"/> property.</para>
        /// <para>The max field limit per embed is 25. If 25 field limit is exceeded, <see cref="OverflowException"/> will be thrown.</para>
        /// </remarks>
        /// <param name="name">The title of the field</param>
        /// <param name="value">The description of the field</param>
        /// <param name="inline">If this field should be inline</param>
        /// <exception cref="OverflowException">When the combined field list exceeds max field limit of <c>25</c></exception>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed AddField(string name, object value, bool inline = false) =>
            AddField(name, value.ToString(), inline);
        /// <summary>
        /// Sets the embed image as the given parameter.
        /// </summary>
        /// <param name="media">The media to use as an image</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetImage(EmbedMedia media)
        {
            Image = media;
            return this;
        }
        /// <summary>
        /// Sets the embed image as the given parameters.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Image"/> as a new instance of <see cref="EmbedMedia"/> made from given parameter.</para>
        /// </remarks>
        /// <param name="url">The source URL of the image</param>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetImage(Uri url, uint? width = null, uint? height = null) =>
            SetImage(new EmbedMedia(url, width, height));
        /// <summary>
        /// Sets the footer as the given parameter.
        /// </summary>
        /// <param name="footer">The footer to set</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetFooter(EmbedFooter footer)
        {
            Footer = footer;
            return this;
        }
        /// <summary>
        /// Sets the footer as the given parameters.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Footer"/> as a new instance of <see cref="EmbedFooter"/> made from given parameters.</para>
        /// </remarks>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon of the footer</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetFooter(string text, Uri iconUrl = null) =>
            SetFooter(new EmbedFooter(text, iconUrl));
        /// <summary>
        /// Sets the footer as the given parameters.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Footer"/> as a new instance of <see cref="EmbedFooter"/> made from given parameters. The text will be set as string equivalent to <paramref name="text"/>.</para>
        /// </remarks>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon of the footer</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetFooter(object text, Uri iconUrl = null) =>
            SetFooter(text.ToString(), iconUrl);
        /// <summary>
        /// Sets the thumbnail as the given parameter.
        /// </summary>
        /// <param name="media">The media to use as a thumbnail</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetThumbnail(EmbedMedia media)
        {
            Thumbnail = media;
            return this;
        }
        /// <summary>
        /// Sets the thumbnail as the given parameters.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Thumbnail"/> as a new instance of <see cref="EmbedMedia"/> made from given parameters.</para>
        /// </remarks>
        /// <param name="url">The source URL of the thumbnail image</param>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetThumbnail(Uri url, uint? width = null, uint? height = null)
        {
            Thumbnail = new EmbedMedia(url, height, width);
            return this;
        }
        /// <summary>
        /// Sets the timestamp as the given parameter.
        /// </summary>
        /// <param name="time">The date to be set to</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetTimestamp(DateTime time)
        {
            Timestamp = time;
            return this;
        }
        /// <summary>
        /// Sets the timestamp as the current date.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Timestamp"/> as <see cref="DateTime.Now"/>.</para>
        /// </remarks>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetTimestamp() =>
            SetTimestamp(DateTime.Now);
        /// <summary>
        /// Sets the colour as the given parameter.
        /// </summary>
        /// <example>
        /// <code language="csharp">
        /// embed.SetColor(Color.Red);
        /// </code>
        /// </example>
        /// <param name="color">The decimal value of the colour</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetColor(Color color)
        {
            Color = color;
            return this;
        }
        /// <summary>
        /// Sets the colour as the given parameter.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Color"/> as a new instance of <see cref="System.Drawing.Color"/> from parameter <paramref name="argb"/> in RGB format.</para>
        /// </remarks>
        /// <example>
        /// <code language="csharp">
        /// // Alpha channel is ignored
        /// embed.SetColor(0xFFFFFF);
        /// </code>
        /// </example>
        /// <param name="argb">The value of the colour</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetColor(int argb) =>
            SetColor(System.Drawing.Color.FromArgb(argb));
        /// <summary>
        /// Sets the colour as the given parameters.
        /// </summary>
        /// <remarks>
        /// <para>Sets <see cref="Color"/> a new instance of <see cref="System.Drawing.Color"/> with <paramref name="red"/>, <paramref name="green"/> and <paramref name="blue"/> channels.</para>
        /// </remarks>
        /// <param name="red">The value of the red channel</param>
        /// <param name="green">The value of the blue channel</param>
        /// <param name="blue">The value of the green channel</param>
        /// <returns>Current <see cref="Embed"/> instance</returns>
        public Embed SetColor(int red, int green, int blue) =>
            SetColor(System.Drawing.Color.FromArgb(red, green, blue));
        #endregion
    }
}