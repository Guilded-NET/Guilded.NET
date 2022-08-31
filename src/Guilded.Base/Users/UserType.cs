using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the type of user someone is.
/// </summary>
/// <remarks>
/// <para>This can be used to differentiate bots from users. Other types may be added later.</para>
/// </remarks>
/// <seealso cref="Servers.Member" />
/// <seealso cref="Users.User" />
/// <seealso cref="UserSummary" />
/// <seealso cref="Me" />
[JsonConverter(typeof(StringEnumConverter), true)]
public enum UserType
{
    /// <summary>
    /// <see cref="Users.User">The user</see> is a normal user.
    /// </summary>
    /// <seealso cref="Bot" />
    /// <seealso cref="UserType" />
    User,

    /// <summary>
    /// <see cref="Users.User">The user</see> is an API bot.
    /// </summary>
    /// <seealso cref="User" />
    /// <seealso cref="UserType" />
    Bot
}