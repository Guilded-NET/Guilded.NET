using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the type of user someone is.
/// </summary>
/// <remarks>
/// <para>This can be used to differentiate bots from users. Other types may be added later.</para>
/// </remarks>
[JsonConverter(typeof(StringEnumConverter), true)]
public enum UserType
{
    /// <summary>
    /// The user is a normal user.
    /// </summary>
    User,
    /// <summary>
    /// The user is an API bot.
    /// </summary>
    Bot
}