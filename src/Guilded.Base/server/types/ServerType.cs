using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Servers;

/// <summary>
/// Gets the type of <see cref="Server">the server</see>. This does not affect anything about the server.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ServerType
{
    /// <summary>
    /// <see cref="Server">The server</see> is for a team of a certain product or is gaming group.
    /// </summary>
    Team,

    /// <summary>
    /// <see cref="Server">The server</see> is for some kind of organization.
    /// </summary>
    Organization,

    /// <summary>
    /// <see cref="Server">The server</see> is any kind of gaming or non-gaming community for a game or anything else.
    /// </summary>
    Community,

    /// <summary>
    /// <see cref="Server">The server</see> is a small clan in a video game.
    /// </summary>
    Clan,

    /// <summary>
    /// <see cref="Server">The server</see> is a big guild in a video game.
    /// </summary>
    Guild,

    /// <summary>
    /// <see cref="Server">The server</see> is for a friends circle.
    /// </summary>
    Friends,

    /// <summary>
    /// <see cref="Server">The server</see> is dedicated to live content or streaming.
    /// </summary>
    Streaming,

    /// <summary>
    /// <see cref="Server">The server</see> is any other type of group.
    /// </summary>
    Other
}