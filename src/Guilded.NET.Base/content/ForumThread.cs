namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// Forum post posted in a forum channel.
    /// </summary>
    public class ForumThread : ChannelContent<uint>
    {
        // /// <summary>
        // /// Title of the post.
        // /// </summary>
        // /// <value>Title</value>
        // [JsonProperty(Required = Required.Always)]
        // public string Title
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// Content of this forum post.
        // /// </summary>
        // /// <value>Forum post content</value>
        // public MessageContent Message
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// When the forum post was bumped.
        // /// </summary>
        // /// <value>Bumped at</value>
        // [JsonProperty(Required = Required.Always)]
        // public DateTime BumpedAt
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// When the forum post was edited.
        // /// </summary>
        // /// <value>Edited at</value>
        // [JsonProperty(Required = Required.AllowNull)]
        // public DateTime? EditedAt
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// If this forum post is sticky/pinned.
        // /// </summary>
        // /// <value>Pinned/Sticky</value>
        // [JsonProperty(Required = Required.Always)]
        // public bool IsSticky
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <value></value>
        // [JsonProperty(Required = Required.Always)]
        // public bool IsShare
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// If this forum post was deleted.
        // /// </summary>
        // /// <value>Deleted</value>
        // [JsonProperty(Required = Required.Always)]
        // public bool IsDeleted
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// If this forum was locked.
        // /// </summary>
        // /// <value>Locked</value>
        // [JsonProperty(Required = Required.Always)]
        // public bool IsLocked
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// ID of the category this forum post is in.
        // /// </summary>
        // /// <value>Category ID</value>
        // public uint? CategoryId
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// Count of how many replies are in the forum post.
        // /// </summary>
        // /// <value>Reply count</value>
        // [JsonProperty(Required = Required.Always)]
        // public uint ReplyCount
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// When the content were updated.
        // /// </summary>
        // /// <value>Created at</value>
        // public DateTime UpdatedAt
        // {
        //     get; set;
        // }
        /*/// <summary>
        /// Edits a forum post in a specific channel.
        /// </summary>
        /// <param name="title">New title of this post</param>
        /// <param name="message">New content of the post</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task EditAsync(string title, MessageContent message) =>
            await ParentClient.EditForumPostAsync(ChannelId, Id, title, message);
        /// <summary>
        /// Deletes a forum post in a specific channel.
        /// </summary>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task DeleteAsync() =>
            await ParentClient.DeleteForumPostAsync(ChannelId, Id);
        /// <summary>
        /// Replies to a forum post.
        /// </summary>
        /// <param name="content">Content to reply with</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        public async Task ReplyAsync(MessageContent content) =>
            await ParentClient.CreateForumReplyAsync(ChannelId, Id, content);
        /// <summary>
        /// Gets replies of a specific forum post.
        /// </summary>
        /// <param name="maxItems">Max amount of replies it should get</param>
        /// <param name="afterDate">After which date should it get replies</param>
        /// <exception cref="GuildedException">Exception thrown by Guilded API</exception>
        /// <returns>List of replies</returns>
        public async Task<IList<ForumReply>> GetRepliesAsync(uint maxItems = 250, DateTime? afterDate = null) =>
            await ParentClient.GetForumRepliesAsync(ChannelId, Id, maxItems, afterDate);*/
    }
}