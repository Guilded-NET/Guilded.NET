using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Content
{
    using Chat;
    // /// <summary>
    // /// A document posted in the doc channel.
    // /// </summary>
    // public class GuildedDocument : ChannelContent<uint>
    // {
    //     /// <summary>
    //     /// Title of the post.
    //     /// </summary>
    //     /// <value>Title</value>
    //     [JsonProperty(Required = Required.Always)]
    //     public string Title
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// A list of tags this document has been tagged with.
    //     /// </summary>
    //     /// <value>List of tags</value>
    //     [JsonConverter(typeof(FlatListConverter))]
    //     public IList<string> Tags
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// Content of this forum post.
    //     /// </summary>
    //     /// <value>Forum post content</value>
    //     [JsonProperty(Required = Required.Always)]
    //     public MessageContent Content
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// When the content were updated.
    //     /// </summary>
    //     /// <value>Created at</value>
    //     public DateTime? ModifiedAt
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// Who updated the document.
    //     /// </summary>
    //     /// <value>Author ID</value>
    //     public GId ModifiedBy
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// If this document is public or not.
    //     /// </summary>
    //     /// <value>Public document</value>
    //     public bool IsPublic
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// If this document is a draft document(document that is not posted yet).
    //     /// </summary>
    //     /// <value>Draft document</value>
    //     public bool IsDraft
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// 
    //     /// </summary>
    //     /// <value></value>
    //     public bool IsCredentialed
    //     {
    //         get; set;
    //     }
    //     /// <summary>
    //     /// List of replies in this document. Null if there are 0.
    //     /// </summary>
    //     /// <value>List of document replies</value>
    //     public IList<ContentReply> Replies
    //     {
    //         get; set;
    //     }
    // }
}