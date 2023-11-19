using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Guilded.Servers;

/// <summary>
/// Represents the visibility of <see cref="ServerChannel">server channels and threads</see>.
/// </summary>
/// <seealso cref="ServerChannel" />
/// <seealso cref="Member" />
/// <seealso cref="Webhook" />
[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
public enum ChannelVisibility
{
    /// <summary>
    /// <see cref="ServerChannel">Thread channel</see> that is only visible to the mentioned users in the <see cref="ServerChannel.MessageId">originating message</see>.
    /// </summary>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ChannelType" />
    /// <seealso cref="Public" />
    Private,

    /// <summary>
    /// Regular server <see cref="ServerChannel">channel</see> that is visible to everyone, including people that are not <see cref="Member">members</see> of a server.
    /// </summary>
    /// <seealso cref="ServerChannel" />
    /// <seealso cref="ChannelType" />
    /// <seealso cref="Public" />
    Public
}