using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Content;

/// <summary>
/// Represents the type of a <see cref="Message">message</see> that was created or updated.
/// </summary>
/// <seealso cref="Message"/>
[JsonConverter(typeof(StringEnumConverter), true)]
public enum MessageType
{
    /// <summary>
    /// <para>A plain message that holds <see cref="Message.Content">normal content</see>.</para>
    /// <para>This can be created by anyone.</para>
    /// </summary>
    Default,
    /// <summary>
    /// <para>A system event that is created once an action is done.</para>
    /// <para>This can't be created by anyone and only occurs if certain actions happen.</para>
    /// </summary>
    System
}