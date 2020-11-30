using System;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Author of the embed.
    /// </summary>
    public class EmbedAuthor: BaseObject {
        /// <summary>
        /// Author icon URL.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("iconUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri IconUrl {
            get; set;
        } = null;
        /// <summary>
        /// Title of the embed author.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty("name", Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public string Name {
            get; set;
        } = null;
        /// <summary>
        /// URL of the author.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url {
            get; set;
        } = null;
        /// <summary>
        /// Generates embed author.
        /// </summary>
        /// <param name="name">Name of the author</param>
        /// <returns>Author</returns>
        public static EmbedAuthor Generate(string name) =>
            new EmbedAuthor {
                Name = name
            };
        /// <summary>
        /// Generates embed author.
        /// </summary>
        /// <param name="name">Name of the author</param>
        /// <param name="image">Image of the icon</param>
        /// <returns>Author</returns>
        public static EmbedAuthor Generate(string name, Uri image) =>
            new EmbedAuthor {
                Name = name,
                IconUrl = image
            };
        /// <summary>
        /// Generates embed author.
        /// </summary>
        /// <param name="name">Name of the author</param>
        /// <param name="image">Image of the icon</param>
        /// <param name="url">URL of the author</param>
        /// <returns>Author</returns>
        public static EmbedAuthor Generate(string name, Uri image, Uri url) =>
            new EmbedAuthor {
                Name = name,
                IconUrl = image,
                Url = url
            };
    }
}