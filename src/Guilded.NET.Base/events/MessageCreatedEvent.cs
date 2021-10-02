namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone creates a message.
    /// </summary>
    /// <remarks>
    /// <para>An event that occurs once someone creates/posts a message in the chat. When receiving this event, <see cref="Content.Message.UpdatedAt"/> will never hold a value.</para>
    /// <para>In API, this event is called <c>ChatMessageCreated</c>.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    public class MessageCreatedEvent : MessageEvent { }
}