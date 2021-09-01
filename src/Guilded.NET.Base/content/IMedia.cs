// using System;
// using System.Collections.Generic;

// namespace Guilded.NET.Base.Content
// {
//     /// <summary>
//     /// An interface for profile media and team media.
//     /// </summary>
//     public interface IMedia
//     {
//         /// <summary>
//         /// ID of this media.
//         /// </summary>
//         /// <value>Media ID</value>
//         uint Id
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A title of this media.
//         /// </summary>
//         /// <value>Title</value>
//         string Title
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Tags this media was tagged with.
//         /// </summary>
//         /// <value>List of tags</value>
//         IList<string> Tags
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A type of this media. If it's image or a video media.
//         /// </summary>
//         /// <value>Media type</value>
//         MediaType Type
//         {
//             get; set;
//         }
//         /// <summary>
//         /// URL to the media's source.
//         /// </summary>
//         /// <value>URL</value>
//         Uri MediaSource
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A description of this media describing it.
//         /// </summary>
//         /// <value>Description</value>
//         string Description
//         {
//             get; set;
//         }
//         /// <summary>
//         /// The reactions added on this media.
//         /// </summary>
//         /// <value>List of reactions</value>
//         IList<Reaction> Reactions
//         {
//             get; set;
//         }
//         /// <summary>
//         /// URL to the thumbnail image.
//         /// </summary>
//         /// <value>URL</value>
//         Uri ThumbnailSource
//         {
//             get; set;
//         }
//     }
// }