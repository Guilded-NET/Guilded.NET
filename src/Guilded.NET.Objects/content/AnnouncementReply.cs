using System.Threading.Tasks;

namespace Guilded.NET.Objects.Content {
    using Chat;
    /// <summary>
    /// A reply to an announcement.
    /// </summary>
    public class AnnouncementReply: ContentReply<GId> {
        /// <summary>
        /// Deletes this reply.
        /// </summary>
        public async Task DeleteAsync() =>
            await ParentClient.DeleteAnnouncementReplyAsync(TeamId, ContentId, Id);
        /// <summary>
        /// Deletes this reply.
        /// </summary>
        public void Delete() =>
            ParentClient.DeleteAnnouncementReply(TeamId, ContentId, Id);
        /// <summary>
        /// Edits this reply.
        /// </summary>
        /// <param name="message">A new content for this reply</param>
        public async Task EditAsync(MessageContent message) =>
            await ParentClient.EditAnnouncementReplyAsync(ContentId, Id, message);
        /// <summary>
        /// Edits this reply.
        /// </summary>
        /// <param name="message">A new content for this reply</param>
        public void Edit(MessageContent message) =>
            ParentClient.EditAnnouncementReply(ContentId, Id, message);
    }
}