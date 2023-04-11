using System.Runtime.Serialization;
using Guilded.Base;
using Guilded.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Guilded.Users;

/// <summary>
/// Represents a reference to a <see cref="User">user</see> without knowing their <see cref="HashId">user ID</see>.
/// </summary>
/// <seealso cref="HashId" />
/// <seealso cref="UserSummary" />
/// <seealso cref="User" />
[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
public enum UserReference
{
    /// <summary>
    /// Reference to the <see cref="ClientUser">current user</see> of the <see cref="AbstractGuildedClient">client</see>.
    /// </summary>
    [EnumMember(Value = "@me")]
    Me
}