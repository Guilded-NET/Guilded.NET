using System;
using Guilded.Base.Content;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Servers;

/// <summary>
/// The ban's information.
/// </summary>
/// <remarks>
/// <para>The information about the ban of a certain member.</para>
/// </remarks>
public class MemberBan : BaseObject, ICreatableContent
{
    #region JSON properties
    /// <summary>
    /// The banned user.
    /// </summary>
    /// <remarks>
    /// <para>The information about the user that has received a ban.</para>
    /// </remarks>
    /// <value>User's summary</value>
    public UserSummary User { get; set; }
    /// <summary>
    /// The reason why the user has been banned.
    /// </summary>
    /// <remarks>
    /// <para>The (un)specified reason why the user was banned by whom the member was banned.</para>
    /// </remarks>
    /// <value>Text?</value>
    public string? Reason { get; set; }
    /// <summary>
    /// The date of when the user was banned
    /// </summary>
    /// <value>Date</value>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// The identifier of the staff who banned.
    /// </summary>
    /// <value>User ID</value>
    public HashId CreatedBy { get; set; }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="MemberBan" /> with the provided details.
    /// </summary>
    /// <param name="user">The user that has been banned</param>
    /// <param name="createdBy">The author of the ban</param>
    /// <param name="createdAt">The date of when the member was banned</param>
    /// <param name="reason">The reason why the user has been banned</param>
    [JsonConstructor]
    public MemberBan(
        [JsonProperty(Required = Required.Always)]
        UserSummary user,

        [JsonProperty(Required = Required.Always)]
        HashId createdBy,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty]
        string? reason
    ) =>
        (User, CreatedBy, CreatedAt, Reason) = (user, createdBy, createdAt, reason);
}