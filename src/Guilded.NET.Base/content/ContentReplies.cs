// using System.Threading.Tasks;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Content
// {
//     using Chat;
//     using Teams;
//     using Users;
//     /// <summary>
//     /// A reply to a Guilded document or media.
//     /// </summary>
//     public class ContentReply<T> : ChannelReply
//     {
//         /// <summary>
//         /// ID of the document/media this reply was posted under.
//         /// </summary>
//         /// <value>Document/media ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public T ContentId
//         {
//             get; set;
//         }
//     }
//     /// <summary>
//     /// A reply to a Guilded document or media.
//     /// </summary>
//     public class ContentReply : ContentReply<uint>
//     {
//         /*/// <summary>
//         /// Deletes this reply.
//         /// </summary>
//         /// <param name="type">A type of the channel this reply is in.</param>
//         public async Task DeleteAsync(ChannelType type) =>
//             await ParentClient.DeleteContentReplyAsync(TeamId, ContentId.ToString(), Id, type);
//         /// <summary>
//         /// Edits this reply.
//         /// </summary>
//         /// <param name="type">A type of the channel this reply is in.</param>
//         /// <param name="message">A new content for this reply</param>
//         public async Task EditAsync(ChannelType type, MessageContent message) =>
//             await ParentClient.EditContentReplyAsync(ContentId.ToString(), Id, type, message);*/
//     }
// }