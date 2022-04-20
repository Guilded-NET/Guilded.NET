using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.Base.Events;

/// <summary>
/// Represents an event with the name <c>TeamXpAdded</c> and opcode <c>0</c> that occurs once <see cref="Amount"/> XP is given to <see cref="Users"/>. This can be given to a couple users, instead of it being restricted to one user.
/// </summary>
/// <seealso cref="WelcomeEvent"/>
/// <seealso cref="RolesUpdatedEvent"/>
/// <seealso cref="Servers.Member"/>
public class XpAddedEvent : BaseObject
{
    #region JSON properties
    /// <summary>
    /// Gets the identifiers of the users who received <see cref="Amount">XP</see>.
    /// </summary>
    /// <value>list of user IDs</value>
    public IList<HashId> Users { get; }
    /// <summary>
    /// Gets the amount of XP given to the <see cref="Users">users</see>.
    /// </summary>
    /// <remarks>
    /// <para>This should usually be between <c>1000</c> and <c>-1000</c> amount of XP.</para>
    /// </remarks>
    /// <value>XP</value>
    public long Amount { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the first XP receiving <see cref="Users">user</see>.
    /// </summary>
    /// <returns>User ID</returns>
    public HashId FirstUser => Users.First();
    /// <summary>
    /// Gets the last XP receiving <see cref="Users">user</see>.
    /// </summary>
    /// <returns>User ID</returns>
    public HashId LastUser => Users.Last();
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="XpAddedEvent"/> from the specified JSON properties.
    /// </summary>
    /// <param name="userIds">The identifiers of the users who received <see cref="Amount">XP</see></param>
    /// <param name="amount">The amount of XP given to the <see cref="Users">users</see></param>
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