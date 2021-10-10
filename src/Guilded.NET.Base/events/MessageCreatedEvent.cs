namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once someone creates a message.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>ChatMessageCreated</c> and opcode <c>0</c> that occurs once someone creates/posts a message in the chat. When receiving this event, <see cref="Content.Message.UpdatedAt"/> will never hold a value.</para>
    /// </remarks>
    /// <seealso cref="MessageUpdatedEvent"/>
    /// <seealso cref="MessageDeletedEvent"/>
    /// <seealso cref="Content.Message"/>
    public class MessageCreatedEvent : MessageEvent { }
}