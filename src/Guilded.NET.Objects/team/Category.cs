using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Guilded.NET.Objects.Teams {
    using Guilded.NET.Objects.Permissions;
    /// <summary>
    /// A Guilded channel category.
    /// </summary>
    public class Category: TeamChannel<uint> {
        /// <summary>
        /// A Guilded channel category.
        /// </summary>
        public Category() =>
            ChannelCategoryId = null;
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        [JsonProperty("channelCategoryId", Required = Required.AllowNull)]
        public uint? ChannelCategoryId {
            get; set;
        }
        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"Category({Id})";
    }
}