using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Servers;

/// <summary>
/// Gets the type of the <see cref="Server">server</see>. This does not affect anything about the server.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ServerType
{
    /// <summary>
    /// the <see cref="Server">server</see> is for a team of a certain product or is gaming group.
    /// </summary>
    Team,

    /// <summary>
    /// the <see cref="Server">server</see> is for some kind of organization.
    /// </summary>
    Organization,

    /// <summary>
    /// the <see cref="Server">server</see> is any kind of gaming or non-gaming community for a game or anything else.
    /// </summary>
    Community,

    /// <summary>
    /// the <see cref="Server">server</see> is a small clan in a video game.
    /// </summary>
    Clan,

    /// <summary>
    /// the <see cref="Server">server</see> is a big guild in a video game.
    /// </summary>
    Guild,

    /// <summary>
    /// the <see cref="Server">server</see> is for a friends circle.
    /// </summary>
    Friends,

    /// <summary>
    /// the <see cref="Server">server</see> is dedicated to live content or streaming.
    /// </summary>
    Streaming,

    /// <summary>
    /// the <see cref="Server">server</see> is any other type of group.
    /// </summary>
    Other
}