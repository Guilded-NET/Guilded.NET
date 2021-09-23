using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Content
{
    /// <summary>
    /// The type of the message created or updated.
    /// </summary>
    /// <remarks>
    /// <para>Defines the type of a <see cref="Message"/> instance. Currently only <see cref="Default"/> and <see cref="System"/> are available.</para>
    /// </remarks>
    /// <seealso cref="Message"/>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum MessageType
    {
        /// <summary>
        /// <para>A plain message that holds <see cref="Message.Content"/>.</para>
        /// <para>This can be created by anyone.</para>
        /// </summary>
        Default,
        /// <summary>
        /// <para>A system event that is created once some action is done.</para>
        /// <para>This can't be created by anyone and only occurs if certain actions happen.</para>
        /// </summary>
        System
    }
}