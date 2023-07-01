using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.Connection;

/// <summary>
/// Message that was received from a WebSocket client.
/// </summary>
/// <remarks>
/// <para>Any message that can be received the Guilded WebSocket, including Guilded events.</para>
/// </remarks>
/// <seealso cref="GuildedSocketException" />
public class GuildedSocketMessage
{
    #region Properties
    /// <summary>
    /// Gets the operation code that identifies the type of <see cref="GuildedSocketMessage">WebSocket message</see> it is.
    /// </summary>
    /// <remarks>
    /// <para>If <see cref="Opcode" /> is received as <c>8</c>, <see cref="GuildedSocketException" /> will be received instead of a typical event.</para>
    /// </remarks>
    /// <value>The operation code that identifies the type of <see cref="GuildedSocketMessage">WebSocket message</see> it is</value>
    /// <seealso cref="GuildedSocketMessage" />
    /// <seealso cref="EventName" />
    /// <seealso cref="MessageId" />
    /// <seealso cref="RawData" />
    public SocketOpcode Opcode { get; }

    /// <summary>
    /// Gets the name of the <see cref="GuildedSocketMessage">event</see> received.
    /// </summary>
    /// <remarks>
    /// <para>This only has a value if <see cref="Opcode" /> is <c>0</c>.</para>
    /// </remarks>
    /// <value>The name of the <see cref="GuildedSocketMessage">event</see> received</value>
    /// <seealso cref="GuildedSocketMessage" />
    /// <seealso cref="MessageId" />
    /// <seealso cref="Opcode" />
    /// <seealso cref="RawData" />
    public string? EventName { get; }

    /// <summary>
    /// Gets the data associated with the <see cref="GuildedSocketMessage">event</see>.
    /// </summary>
    /// <remarks>
    /// <para>Holds the data of most messages. Only if <see cref="Opcode" /> is <c>9</c>, this will be <see langword="null" />.</para>
    /// </remarks>
    /// <value>The data associated with the <see cref="GuildedSocketMessage">event</see></value>
    /// <seealso cref="GuildedSocketMessage" />
    /// <seealso cref="MessageId" />
    /// <seealso cref="EventName" />
    /// <seealso cref="Opcode" />
    public JObject? RawData { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="GuildedSocketMessage">event</see>.
    /// </summary>
    /// <remarks>
    /// <para>This can be passed to <see cref="BaseGuildedConnection.LastMessageId" /> to receive any messages after this message.</para>
    /// <para>This property only holds the value if <see cref="Opcode" /> is <c>0</c>.</para>
    /// </remarks>
    /// <value>The identifier of the <see cref="GuildedSocketMessage">event</see></value>
    /// <seealso cref="GuildedSocketMessage" />
    /// <seealso cref="EventName" />
    /// <seealso cref="RawData" />
    /// <seealso cref="Opcode" />
    public string? MessageId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="GuildedSocketMessage" /> from the specified JSON properties.
    /// </summary>
    /// <param name="op">The operation code that identifies the type of <see cref="GuildedSocketMessage">WebSocket message</see> it is</param>
    /// <param name="t">The name of the <see cref="GuildedSocketMessage">event</see> received</param>
    /// <param name="d">The data associated with the <see cref="GuildedSocketMessage">event</see></param>
    /// <param name="s">The identifier of the <see cref="GuildedSocketMessage">event</see></param>
    [JsonConstructor]
    public GuildedSocketMessage(
        [JsonProperty(Required = Required.Always)]
        SocketOpcode op,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? t = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        JObject? d = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? s = null
    ) =>
        (Opcode, EventName, RawData, MessageId) = (op, t, d, s);
    #endregion

    #region Methods
    /// <summary>
    /// Returns the string representation of the <see cref="GuildedSocketMessage">socket message</see>.
    /// </summary>
    /// <returns><see cref="GuildedSocketMessage" /> as a <see cref="string" /></returns>
    public override string ToString() =>
        ToString(Formatting.Indented);

    /// <summary>
    /// Returns the string representation of the <see cref="GuildedSocketMessage">socket message</see>.
    /// </summary>
    /// <returns><see cref="GuildedSocketMessage" /> as a <see cref="string" /></returns>
    public string ToString(Formatting formatting)
    {
        var (indent, final) = formatting == Formatting.Indented ? ("\n  ", '\n') : (" ", ' ');

        // Might have to make it a field instead
        StringBuilder builder =
            new StringBuilder("GuildedSocketMessage {")
                .Append(indent)
                // Opcode(op) = Welcome (1)
                .Append("Opcode(op) = ")
                .Append(Opcode)
                .Append(" (")
                .Append(Opcode.ToString("d"))
                .Append(')');

        // , EventName(t) = "...",
        if (EventName is not null)
            builder
                .Append(',')
                .Append(indent)
                .Append("EventName(t) = \"")
                .Append(EventName)
                .Append('"');

        // , MessageId(s) = "..."
        if (MessageId is not null)
            builder
                .Append(',')
                .Append(indent)
                .Append("MessageId(s) = \"")
                .Append(MessageId)
                .Append('"');

        // , Data(d) = { ... }
        if (RawData is not null)
            builder
                .Append(',')
                .Append(indent)
                .Append("Data(d) = ")
                .Append(RawData?.ToString(formatting));

        builder.Append(final).Append('}');

        return builder.ToString();
    }
    #endregion
}