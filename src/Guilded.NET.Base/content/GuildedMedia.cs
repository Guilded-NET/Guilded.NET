using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    // /// <summary>
    // /// Media that was posted in the profile or in a media channel.
    // /// </summary>
    // public class GuildedMedia : ChannelPost<uint>, IMedia
    // {
    //     /// <inheritdoc/>
    //     [JsonProperty(Required = Required.Always)]
    //     public string Title
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     [JsonConverter(typeof(FlatListConverter))]
    //     public IList<string> Tags
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     [JsonProperty(Required = Required.Always)]
    //     public MediaType Type
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     [JsonProperty("src", Required = Required.Always)]
    //     public Uri MediaSource
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     public string Description
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// When this media was updated last time.
    //     /// </summary>
    //     /// <value>Updated at</value>
    //     public DateTime? UpdatedAt
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     [JsonProperty(Required = Required.AllowNull)]
    //     public IList<Reaction> Reactions
    //     {
    //         get; set;
    //     }
    //     /// <inheritdoc/>
    //     [JsonProperty("srcThumbnail", Required = Required.AllowNull)]
    //     public Uri ThumbnailSource
    //     {
    //         get; set;
    //     }
    // }
}