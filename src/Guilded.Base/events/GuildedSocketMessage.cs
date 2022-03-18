using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.Base.Events;

/// <summary>
/// Message that was received from a WebSocket client.
/// </summary>
/// <remarks>
/// <para>Any message that can be received the Guilded WebSocket, including Guilded events.</para>
/// </remarks>
/// <seealso cref="MessageCreatedEvent"/>
/// <seealso cref="MessageUpdatedEvent"/>
/// <seealso cref="MessageDeletedEvent"/>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="MemberUpdatedEvent"/>
/// <seealso cref="XpAddedEvent"/>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="ResumeEvent"/>
public class GuildedSocketMessage : ClientObject
{
    #region JSON properties
    /// <summary>
    /// An operation code that tells about the message.
    /// </summary>
    /// <remarks>
    /// <para>Opcodes are defined as following:</para>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Opcode</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>0</term>
    ///         <description>An event with data associated.</description>
    ///     </item>
    ///     <item>
    ///         <term>1</term>
    ///         <description>An event that occurs once WebSocket connection is established.</description>
    ///     </item>
    ///     <item>
    ///         <term>2</term>
    ///         <description>An event that occurs once connection is re-established with passed last event.</description>
    ///     </item>
    ///     <item>
    ///         <term>8</term>
    ///         <description>Closing/error frame</description>
    ///     </item>
    /// </list>
    /// <para>If <see cref="Opcode"/> is received as <c>8</c>, <see cref="GuildedWebsocketException"/> will be received instead of a typical event.</para>
    /// </remarks>
    /// <value>Opcode</value>
    public byte Opcode { get; }
    /// <summary>
    /// The name of the event received.
    /// </summary>
    /// <remarks>
    /// <para>This only has a value if <see cref="Opcode"/> is <c>0</c>. It holds the name of the receiving Guilded event.</para>
    /// </remarks>
    /// <value>Name?</value>
    public string? EventName { get; }
    /// <summary>
    /// The data associated with the event.
    /// </summary>
    /// <remarks>
    /// <para>The data associated with the receiving event/message. Holds the data of most messages, including <see cref="WelcomeEvent"/>, <see cref="ResumeEvent"/> and <see cref="GuildedWebsocketException"/>. Only if <see cref="Opcode"/> is <c>9</c>, this will be <see langword="null"/>.</para>
    /// </remarks>
    /// <value>Data?</value>
    public JObject? RawData { get; }
    /// <summary>
    /// An identifier that allows the event to be replayed.
    /// </summary>
    /// <remarks>
    /// <para>The identifier of the event message. This can be passed to <see cref="BaseGuildedClient.LastMessageId"/> to receive any messages after this message.</para>
    /// <para>This property only holds the value if <see cref="Opcode"/> is <c>0</c>.</para>
    /// </remarks>
    /// <value>Event ID?</value>
    public string? MessageId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="GuildedSocketMessage"/> with given information.
    /// </summary>
    /// <param name="op">The opcode of the socket message</param>
    /// <param name="t">The name of the event</param>
    /// <param name="d">The data of the socket message</param>
    /// <param name="s">The identifier of the socket message</param>
    [JsonConstructor]
    public GuildedSocketMessage(
        [JsonProperty(Required = Required.Always)]
        byte op,

        [JsonProperty(Required = Required.Always)]
        string? t,

        [JsonProperty(Required = Required.Always)]
        JObject? d,

        [JsonProperty(Required = Required.Always)]
        string? s
    ) =>
        (Opcode, EventName, RawData, MessageId) = (op, t, d, s);
    #endregion
}