using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// A media post posted in a profile.
    /// </summary>
    public class ProfileMedia : ClientObject, IMedia
    {
        /// <inheritdoc/>
        [JsonProperty(Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty(Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonConverter(typeof(FlatListConverter))]
        public IList<string> Tags
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty(Required = Required.Always)]
        public MediaType Type
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("src", Required = Required.Always)]
        public Uri MediaSource
        {
            get; set;
        }
        /// <inheritdoc/>
        public string Description
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty(Required = Required.Always)]
        public IList<Reaction> Reactions
        {
            get; set;
        }
        /// <summary>
        /// ID of the profile user this media was posted in.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty(Required = Required.Always)]
        public GId UserId
        {
            get; set;
        }
        /// <summary>
        /// No idea.
        /// </summary>
        /// <value>Not used at all</value>
        [JsonProperty(Required = Required.Always)]
        public bool ShowInBanner
        {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("srcThumbnail", Required = Required.AllowNull)]
        public Uri ThumbnailSource
        {
            get; set;
        }
    }
}