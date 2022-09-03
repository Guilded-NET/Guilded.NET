using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Client;
using Guilded.Base.Servers;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event that occurs when <see cref="Member">a member</see> receives <see cref="Amount">XP</see>.
/// </summary>
/// <seealso cref="RolesUpdatedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="Member" />
public class XpAddedEvent
{
    #region Properties
    /// <summary>
    /// Gets the identifiers of <see cref="Users.User">the users</see> who received <see cref="Amount">XP</see>.
    /// </summary>
    /// <value>List of user IDs</value>
    /// <seealso cref="XpAddedEvent" />
    /// <seealso cref="Amount" />
    /// <seealso cref="FirstUser" />
    /// <seealso cref="LastUser" />
    public IList<HashId> Users { get; }

    /// <summary>
    /// Gets the amount of XP given to the <see cref="Users">users</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be between <c>1000</c> and <c>-1000</c> amount of XP.</para>
    /// </remarks>
    /// <value>XP</value>
    /// <seealso cref="XpAddedEvent" />
    /// <seealso cref="Users" />
    /// <seealso cref="FirstUser" />
    /// <seealso cref="LastUser" />
    public long Amount { get; }

    /// <summary>
    /// Gets the first XP receiving <see cref="Users">user</see>.
    /// </summary>
    /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
    /// <seealso cref="XpAddedEvent" />
    /// <seealso cref="Amount" />
    /// <seealso cref="Users" />
    /// <seealso cref="LastUser" />
    public HashId FirstUser => Users.First();

    /// <summary>
    /// Gets the last XP receiving <see cref="Users">user</see>.
    /// </summary>
    /// <value><see cref="Users.UserSummary.Id">User ID</see></value>
    /// <seealso cref="XpAddedEvent" />
    /// <seealso cref="Amount" />
    /// <seealso cref="Users" />
    /// <seealso cref="FirstUser" />
    public HashId LastUser => Users.Last();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="XpAddedEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="userIds">The identifiers of <see cref="Users.User">the users</see> who received <see cref="Amount">XP</see></param>
    /// <param name="amount">The amount of XP given to the <see cref="Users">users</see></param>
    /// <returns>New <see cref="XpAddedEvent" /> JSON instance</returns>
    /// <seealso cref="XpAddedEvent" />
    [JsonConstructor]
    public XpAddedEvent(
        [JsonProperty(Required = Required.Always)]
        IList<HashId> userIds,

        [JsonProperty(Required = Required.Always)]
        long amount
    ) =>
        (Users, Amount) = (userIds, amount);
    #endregion
}