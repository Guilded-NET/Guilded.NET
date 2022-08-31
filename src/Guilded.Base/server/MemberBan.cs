using System;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// Represents the information of <see cref="User">user's</see> ban.
/// </summary>
/// <seealso cref="Member" />
/// <seealso cref="MemberSummary{T}" />
/// <seealso cref="Users.User" />
public class MemberBan : ICreatableContent
{
    #region Properties
    /// <summary>
    /// Gets the banned user.
    /// </summary>
    /// <value>User's summary</value>
    public UserSummary User { get; }

    /// <summary>
    /// Gets the reason why the <see cref="User">user</see> has been banned, if the reason was specified.
    /// </summary>
    /// <value>Text?</value>
    public string? Reason { get; }

    /// <summary>
    /// Gets the date when the <see cref="User">user</see> was banned
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the identifier of the staff who banned.
    /// </summary>
    /// <value><see cref="UserSummary.Id">User ID</see></value>
    public HashId CreatedBy { get; }
    #endregion

    /// <summary>
    /// Initializes a new instance of <see cref="MemberBan" /> with the provided details.
    /// </summary>
    /// <param name="user">The user who has been banned</param>
    /// <param name="createdBy">The author of the ban</param>
    /// <param name="createdAt">the date when the member was banned</param>
    /// <param name="reason">The reason why the user has been banned</param>
    /// <returns>New <see cref="MemberBan" /> JSON instance</returns>
    /// <seealso cref="MemberBan" />
    [JsonConstructor]
    public MemberBan(
        [JsonProperty(Required = Required.Always)]
        UserSummary user,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? reason = null
    ) =>
        (User, CreatedBy, CreatedAt, Reason) = (user, createdBy, createdAt, reason);
}