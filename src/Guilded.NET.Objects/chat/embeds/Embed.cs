using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Message embed data.
    /// </summary>
    public class Embed : BaseObject
    {
        /// <summary>
        /// Title of the embed.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title
        {
            get; set;
        } = null;
        /// <summary>
        /// Description of the embed.
        /// </summary>
        /// <value></value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description
        {
            get; set;
        } = null;
        /// <summary>
        /// URL of the title.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url
        {
            get; set;
        } = null;
        /// <summary>
        /// Colour of the embed.
        /// </summary>
        /// <value>Colour</value>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Color
        {
            get; set;
        } = null;
        /// <summary>
        /// Timestamp of the embed footer.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp
        {
            get; set;
        } = null;
        /// <summary>
        /// Thumbnail of the embed.
        /// </summary>
        /// <value>Image</value>
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public EmbedImage Image
        {
            get; set;
        } = null;
        /// <summary>
        /// Image of the embed.
        /// </summary>
        /// <value>Image</value>
        [JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public EmbedImage Thumbnail
        {
            get; set;
        } = null;
        /// <summary>
        /// Embed's footer.
        /// </summary>
        /// <value>Footer</value>
        [JsonProperty("footer", NullValueHandling = NullValueHandling.Ignore)]
        public EmbedFooter Footer
        {
            get; set;
        } = null;
        /// <summary>
        /// Author of the embed.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public EmbedAuthor Author
        {
            get; set;
        } = null;
        /// <summary>
        /// List of fields in this embed.
        /// </summary>
        /// <value>List of fields</value>
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public IList<EmbedField> Fields
        {
            get; set;
        } = null;
        /// <summary>
        /// Adds field to this embed.
        /// </summary>
        /// <param name="title">Title of the field</param>
        /// <param name="description">Description of the field</param>
        /// <param name="inline">If this field should be inline</param>
        /// <returns>This</returns>
        public Embed AddField(string title, string description, bool inline = false) => AddField(EmbedField.Generate(title, description, inline));
        /// <summary>
        /// Adds field to this embed.
        /// </summary>
        /// <param name="field">Field to be added</param>
        /// <returns>This</returns>
        public Embed AddField(EmbedField field)
        {
            // If fields list is null
            if (Fields == null) Fields = new List<EmbedField> {
                field
            };
            else Fields.Add(field);
            // Returns this embed
            return this;
        }
        /// <summary>
        /// Adds fields to this embed.
        /// </summary>
        /// <param name="fields">Fields to be added</param>
        /// <returns>This</returns>
        public Embed AddFields(params EmbedField[] fields)
        {
            // If fields list is null
            if (Fields == null) Fields = fields;
            // Because for some reason, IList has no AddRange, where as List has AddRange.
            else foreach (EmbedField field in fields) Fields.Add(field);
            // Returns this embed
            return this;
        }
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
        public Embed SetAuthor(string name, Uri iconUrl = null, Uri url = null) => SetAuthor(EmbedAuthor.Generate(name, iconUrl, url));
        /// <summary>
        /// Sets embed's title name and URL.
        /// </summary>
        /// <param name="name">Name of the title</param>
        /// <param name="url">URL of the title</param>
        /// <returns>This</returns>
        public Embed SetTitle(string name, Uri url = null)
        {
            Title = name;
            Url = url;
            return this;
        }
        /// <summary>
        /// Sets description of the embed.
        /// </summary>
        /// <param name="description">Embed's description</param>
        /// <returns>This</returns>
        public Embed SetDescription(string description)
        {
            Description = description;
            return this;
        }
        /// <summary>
        /// Sets the image of the embed.
        /// </summary>
        /// <param name="url">URL to the image</param>
        /// <returns>This</returns>
        public Embed SetImage(Uri url)
        {
            Image = new EmbedImage
            {
                Url = url
            };
            return this;
        }
        /// <summary>
        /// Sets the thumbnail of the embed.
        /// </summary>
        /// <param name="url">URL to the image</param>
        /// <returns>This</returns>
        public Embed SetThumbnail(Uri url)
        {
            Thumbnail = new EmbedImage
            {
                Url = url
            };
            return this;
        }
        /// <summary>
        /// Sets timestamp of the embed.
        /// </summary>
        /// <param name="time">Date to be set to</param>
        /// <returns>This</returns>
        public Embed SetTimestamp(DateTime time)
        {
            Timestamp = time;
            return this;
        }
        /// <summary>
        /// Sets timestamp of the embed to current time.
        /// </summary>
        /// <returns>This</returns>
        public Embed SetTimestamp() => SetTimestamp(DateTime.Now);
        /// <summary>
        /// Sets colour to given colour.
        /// </summary>
        /// <example>
        /// <code>embed.SetColor(0xFFFFFF)</code>
        /// </example>
        /// <param name="color">Colour</param>
        /// <returns></returns>
        public Embed SetColor(uint color)
        {
            Color = color;
            return this;
        }
        /// <summary>
        /// Adds footer to this embed.
        /// </summary>
        /// <param name="footer">Footer to add</param>
        /// <returns>This</returns>
        public Embed SetFooter(EmbedFooter footer)
        {
            Footer = footer;
            return this;
        }
        /// <summary>
        /// Adds footer to this embed.
        /// </summary>
        /// <param name="text">Footer text</param>
        /// <param name="iconUrl">Icon of the footer</param>
        /// <returns>This</returns>
        public Embed SetFooter(string text, Uri iconUrl = null) => SetFooter(EmbedFooter.Generate(text, iconUrl));
    }
}