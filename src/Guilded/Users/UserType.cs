using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Guilded.Users;

/// <summary>
/// Represents the type of user someone is.
/// </summary>
/// <remarks>
/// <para>This can be used to differentiate bots from users. Other types may be added later.</para>
/// </remarks>
/// <seealso cref="Servers.Member" />
/// <seealso cref="Users.User" />
/// <seealso cref="UserSummary" />
/// <seealso cref="ClientUser" />
[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
public enum UserType
{
    /// <summary>
    /// The <see cref="Users.User">user</see> is a normal user.
    /// </summary>
    /// <seealso cref="Bot" />
    /// <seealso cref="UserType" />
    User,

    /// <summary>
    /// The <see cref="Users.User">user</see> is an API bot.
    /// </summary>
    /// <seealso cref="User" />
    /// <seealso cref="UserType" />
    Bot
}