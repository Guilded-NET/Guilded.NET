using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when <see cref="Users.UserStatus">user's status</see> gets added, updated or removed.
/// </summary>
/// <seealso cref="MemberSocialLinkEvent" />
/// <seealso cref="MemberUpdatedEvent" />
public class UserStatusEvent
{
    #region Properties
    /// <summary>
    /// Gets the new <see cref="Users.UserStatus">status</see> of the user.
    /// </summary>
    /// <seealso cref="UserStatusEvent" />
    public UserStatus UserStatus { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserStatusEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="userStatus">The new <see cref="Users.UserStatus">status</see> of the user</param>
    /// <returns>New <see cref="UserStatusEvent" /> JSON instance</returns>
    /// <seealso cref="UserStatusEvent" />
    [JsonConstructor]
    public UserStatusEvent(
        [JsonProperty(Required = Required.Always)]
        UserStatus userStatus
    ) =>
        UserStatus = userStatus;
    #endregion
}