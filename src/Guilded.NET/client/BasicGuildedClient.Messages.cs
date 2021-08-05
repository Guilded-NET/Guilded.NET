using Guilded.NET.Base.Events;

namespace Guilded.NET
{
    /// <summary>
    /// A base for user bot clients and normal bot clients.
    /// </summary>
    public abstract partial class BasicGuildedClient
    {
        /// <summary>
        /// When socket message event is invoked.
        /// </summary>
        /// <param name="o">Object which invoked the event</param>
        /// <param name="message">Message received from a websocket</param>
        protected void HandleSocketMessages(object o, GuildedEvent message)
        {
            // Makes sure that the event received is not null
            if(message is null) return;
            GuildedLogger.Debug("Received socket event [{Opcode}] {EventName}", message?.Opcode, message?.EventName);
            
            string eventName = message?.EventName ?? "";

            // Checks if this event is supported by Guilded.NET
            if(GuildedEvents.ContainsKey(eventName)) {
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