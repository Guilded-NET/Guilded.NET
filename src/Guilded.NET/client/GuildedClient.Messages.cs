using Guilded.NET.Base.Events;

namespace Guilded.NET
{
    /// <summary>
    /// A base for all Guilded clients.
    /// </summary>
    /// <seealso cref="GuildedBotClient"/>
    /// <seealso cref="Base.BaseGuildedClient"/>
    public abstract partial class GuildedClient
    {
        /// <summary>
        /// When the socket message event is invoked.
        /// </summary>
        /// <param name="o">The object that invoked the event</param>
        /// <param name="message">A message received from a WebSocket</param>
        protected void HandleSocketMessages(object o, GuildedEvent message)
        {
            // Makes sure that the event received is not null
            if (message is null) return;
            GuildedLogger.Debug("Received socket event [{Opcode}] {EventName}", message?.Opcode, message?.EventName);

            string eventName = message?.EventName ?? "";

            // Checks if this event is supported by Guilded.NET
            if (GuildedEvents.ContainsKey(eventName))
            {
                // Gets event based on its name
                var ev = GuildedEvents?[eventName];
                // Deserialized data from the event
                object data = message.RawData.ToObject(ev.ArgumentType, GuildedSerializer);

                // Makes sure it exists and then invokes the event
                if (ev != default)
                    ev.Invokable?.DynamicInvoke(this, data);
            }

            GuildedLogger.Debug("Socket event '{EventName}' handled", message.EventName);
        }
    }
}