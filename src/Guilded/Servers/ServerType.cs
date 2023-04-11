using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Guilded.Servers;

/// <summary>
/// Gets the type of the <see cref="Server">server</see>. This does not affect anything about the server.
/// </summary>
/// <remarks>
/// <para>As of now, server types don't do anything, so you can use it in any way.</para>
/// </remarks>
/// <seealso cref="Server" />
/// <seealso cref="ChannelType" />
[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
public enum ServerType
{
    /// <summary>
    /// The <see cref="Server">server</see> is for a team of a certain product or is gaming group.
    /// </summary>
    Team,

    /// <summary>
    /// The <see cref="Server">server</see> is for some kind of organization.
    /// </summary>
    Organization,

    /// <summary>
    /// The <see cref="Server">server</see> is any kind of gaming or non-gaming community for a game or anything else.
    /// </summary>
    Community,

    /// <summary>
    /// The <see cref="Server">server</see> is a small clan in a video game.
    /// </summary>
    Clan,

    /// <summary>
    /// The <see cref="Server">server</see> is a big guild in a video game.
    /// </summary>
    Guild,

    /// <summary>
    /// The <see cref="Server">server</see> is for a friends circle.
    /// </summary>
    Friends,

    /// <summary>
    /// The <see cref="Server">server</see> is dedicated to live content or streaming.
    /// </summary>
    Streaming,

    /// <summary>
    /// The <see cref="Server">server</see> is any other type of group.
    /// </summary>
    Other
}