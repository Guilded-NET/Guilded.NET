using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Embeds
{
    /// <summary>
    /// An embed that is part of embed container.
    /// </summary>
    /// <remarks>
    /// A custom/rich embed in <see cref="Chat.ChatEmbed"/> container node.
    /// </remarks>
    /// <example>
    /// <code>
    /// Embed embed = new Embed
    /// {
    ///     Title = "Title here",
    ///     Description = "Description here",
    ///     Footer = new EmbedFooter("Footer text")
    /// };
    /// </code>
    /// <code>
    /// Embed embed = new Embed(thumbnail: imageUrl)
    /// {
    ///     Title = "Title here",
    ///     Description = "Description here"
    /// };
    /// </code>
    /// <code>
    /// Embed embed = new Embed("Title here", "Description here", "Footer text here");
    /// </code>
    /// <code>
    /// Embed embed = new Embed()
    ///     .SetTitle("Title here")
    ///     .SetDescription("Description here")
    ///     .SetFooter("Footer text here");
    /// </code>
    /// </example>
    /// <seealso cref="Chat.ChatEmbed"/>
    /// <seealso cref="Chat.ContentEmbed"/>
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Embed : BaseObject
    {
        private const int fieldLimit = 25;

        #region JSON properties
        /// <summary>
        /// The title of the embed.
        /// </summary>
        /// <remarks>
        /// A short text that appears above description.
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
        /// Makes the embed's title as a hyperlink that links this URL.
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
        /// A text that appears in the middle of the embed.
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
        /// Represents user and appears at the top of the embed. Can hold name, URL and an icon.
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
        /// The colour of the left side of the embed.
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
        /// An image in the embed that appears at the right side of the embed.
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
        /// An image that will appear in the embed.
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
        /// A video that will appear in the embed.
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
        /// The list of fields that contain their own titles and descriptions.
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
        /// An area that appears at the bottom of the embed.
        /// </remarks>
        /// <value>Footer?</value>
        public EmbedFooter Footer
        {
            get; set;
        }
        /// <summary>
        /// The timestamp of the embed footer.
        /// </summary>
        /// <remarks>
        /// The time and date that appear after the footer's text in the footer.
        /// </remarks>
        /// <value>Date?</value>
        public DateTime? Timestamp
        {
            get; set;
        }
        /// <summary>
        /// The provider of the embed.
        /// </summary>
        /// <remarks>
        /// The name of the domain that appears above the title.
        /// </remarks>
        /// <value>Provider?</value>
        public EmbedProvider Provider
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The data of the message embed.
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
        /// Sets embed's title name and URL.
        /// </summary>
        /// <param name="title">The text of the title</param>
        /// <exception cref="NullReferenceException"><paramref name="title"/> is null, empty or whitespace</exception>
        /// <returns>This</returns>
        public Embed SetTitle(string title)
        {
            // If you try to set null title
            if (string.IsNullOrWhiteSpace(title))
                throw new NullReferenceException($"Argument {nameof(title)} cannot be null, empty or whitespace.");
            Title = title;
            return this;
        }
        /// <summary>
        /// Sets the URL of the title in the embed.
        /// </summary>
        /// <param name="url">The URL that title will link</param>
        /// <returns>This</returns>
        public Embed SetUrl(Uri url)
        {
            Url = url;
            return this;
        }
        /// <summary>
        /// Sets the description of the embed.
        /// </summary>
        /// <param name="description">Embed's description</param>
        /// <exception cref="NullReferenceException"><paramref name="description"/> is null, empty or whitespace</exception>
        /// <returns>This</returns>
        public Embed SetDescription(string description)
        {
            // If you try to set null title
            if (string.IsNullOrWhiteSpace(description))
                throw new NullReferenceException($"Argument {nameof(description)} cannot be null, empty or whitespace.");
            Description = description;
            return this;
        }
        /// <summary>
        /// Sets the description of the embed.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <returns>This</returns>
        public Embed SetDescription(string format, params object[] args) =>
            SetDescription(string.Format(format, args));
        /// <summary>
        /// Sets the description of the embed.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <returns>This</returns>
        public Embed SetDescription(IFormatProvider provider, string format, params object[] args) =>
            SetDescription(string.Format(provider, format, args));
        /// <summary>
        /// Sets the description of the embed.
        /// </summary>
        /// <param name="description">Embed's description</param>
        /// <returns>This</returns>
        public Embed SetDescription(object description) =>
            SetDescription(description.ToString());
        /// <summary>
        /// Sets author to this embed.
        /// </summary>
        /// <param name="author">Author to be set to</param>
        /// <returns>This</returns>
        public Embed SetAuthor(EmbedAuthor author)
        {
            Author = author;
            return this;
        }
        /// <summary>
        /// Sets author to this embed.
        /// </summary>
        /// <param name="name">Name of the author</param>
        /// <param name="iconUrl">URL of the image</param>
        /// <param name="url">URL of the author's name</param>
        /// <returns>This</returns>
        public Embed SetAuthor(string name, Uri iconUrl = null, Uri url = null) =>
            SetAuthor(new EmbedAuthor(name, iconUrl, url));
        /// <summary>
        /// Adds fields to this embed.
        /// </summary>
        /// <param name="fields">The list of fields to be added</param>
        /// <exception cref="OverflowException">Attempt at trying to add more than 25 fields</exception>
        /// <returns>This</returns>
        public Embed AddFields(IList<EmbedField> fields)
        {
            // Throw an error if you try to add more than 25 fields
            if ((Fields.Count + fields.Count) > fieldLimit || Fields.Count > 25)
                throw new OverflowException("Cannot add more than 25 fields to the embed");
            // If fields list is null, set it
            else if (Fields is null) Fields = fields;
            // Add all fiels to IList
            else Fields = Fields.Concat(fields).ToList();
            // Returns this embed
            return this;
        }
        /// <summary>
        /// Adds fields to this embed.
        /// </summary>
        /// <param name="fields">Fields to be added</param>
        /// <exception cref="OverflowException">Attempt at trying to add more than 25 fields</exception>
        /// <returns>This</returns>
        public Embed AddFields(params EmbedField[] fields) =>
            AddFields((IList<EmbedField>)fields);
        /// <summary>
        /// Adds a field to this embed.
        /// </summary>
        /// <param name="field">A field to be added</param>
        /// <returns>This</returns>
        public Embed AddField(EmbedField field) =>
            AddFields(field);
        /// <summary>
        /// Adds a field to this embed.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="description">The description of the field</param>
        /// <param name="inline">If this field should be inline</param>
        /// <returns>This</returns>
        public Embed AddField(string title, string description, bool inline = false) =>
            AddField(new EmbedField(title, description, inline));
        /// <summary>
        /// Adds a field to this embed.
        /// </summary>
        /// <param name="title">The title of the field</param>
        /// <param name="description">The description of the field</param>
        /// <param name="inline">If this field should be inline</param>
        /// <returns>This</returns>
        public Embed AddField(string title, object description, bool inline = false) =>
            AddField(title, description.ToString(), inline);
        /// <summary>
        /// Sets the image of the embed.
        /// </summary>
        /// <param name="image">The media to use as an image</param>
        /// <returns>This</returns>
        public Embed SetImage(EmbedMedia image)
        {
            Image = image;
            return this;
        }
        /// <summary>
        /// Sets the image of the embed.
        /// </summary>
        /// <param name="url">The source URL of the image</param>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <returns>This</returns>
        public Embed SetImage(Uri url, uint? width = null, uint? height = null) =>
            SetImage(new EmbedMedia(url, width, height));
        /// <summary>
        /// Adds the footer to this embed.
        /// </summary>
        /// <param name="footer">The footer to set</param>
        /// <returns>This</returns>
        public Embed SetFooter(EmbedFooter footer)
        {
            Footer = footer;
            return this;
        }
        /// <summary>
        /// Adds the footer to this embed.
        /// </summary>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon of the footer</param>
        /// <returns>This</returns>
        public Embed SetFooter(string text, Uri iconUrl = null) =>
            SetFooter(new EmbedFooter(text, iconUrl));
        /// <summary>
        /// Adds the footer to this embed.
        /// </summary>
        /// <param name="text">The text of the footer</param>
        /// <param name="iconUrl">The icon of the footer</param>
        /// <returns>This</returns>
        public Embed SetFooter(object text, Uri iconUrl = null) =>
            SetFooter(text.ToString(), iconUrl);
        /// <summary>
        /// Sets the thumbnail of the embed.
        /// </summary>
        /// <param name="media">The media to use as a thumbnail</param>
        /// <returns>This</returns>
        public Embed SetThumbnail(EmbedMedia media)
        {
            Thumbnail = media;
            return this;
        }
        /// <summary>
        /// Sets the thumbnail of the embed.
        /// </summary>
        /// <param name="url">The source URL of the thumbnail image</param>
        /// <param name="width">The width of the image</param>
        /// <param name="height">The height of the image</param>
        /// <returns>This</returns>
        public Embed SetThumbnail(Uri url, uint? width = null, uint? height = null)
        {
            Thumbnail = new EmbedMedia(url, height, width);
            return this;
        }
        /// <summary>
        /// Sets the timestamp of the embed.
        /// </summary>
        /// <param name="time">The date to be set to</param>
        /// <returns>This</returns>
        public Embed SetTimestamp(DateTime time)
        {
            Timestamp = time;
            return this;
        }
        /// <summary>
        /// Sets the timestamp of the embed to current time.
        /// </summary>
        /// <returns>This</returns>
        public Embed SetTimestamp() =>
            SetTimestamp(DateTime.Now);
        /// <summary>
        /// Sets the colour of the embed.
        /// </summary>
        /// <example>
        /// <code>
        /// embed.SetColor(Color.Red);
        /// </code>
        /// </example>
        /// <param name="color">The decimal value of the colour</param>
        /// <returns>This</returns>
        public Embed SetColor(Color color)
        {
            Color = color;
            return this;
        }
        /// <summary>
        /// Sets the colour of the embed.
        /// </summary>
        /// <example>
        /// <code>
        /// embed.SetColor(0xFFFFFF);
        /// </code>
        /// </example>
        /// <param name="rgba">The value of the colour</param>
        /// <returns>This</returns>
        public Embed SetColor(int rgba) =>
            SetColor(System.Drawing.Color.FromArgb(rgba));
        /// <summary>
        /// Sets the colour of the embed.
        /// </summary>
        /// <param name="red">The red channel value</param>
        /// <param name="green">The green channel value</param>
        /// <param name="blue">The blue channel value</param>
        /// <returns>This</returns>
        public Embed SetColor(int red, int green, int blue) =>
            SetColor(System.Drawing.Color.FromArgb(red, green, blue));
        #endregion
    }
}