using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Events
{
    /// <summary>
    /// An event that occurs once XP is given to a set of users.
    /// </summary>
    /// <remarks>
    /// <para>An event of the name <c>TeamXpAdded</c> and opcode <c>0</c> that occurs once <see cref="Amount"/> XP is given to <see cref="Users"/>. This can be given to a couple users, instead of it being restricted to one user. <see cref="XpAddedEvent"/> can only occur in teams and tournaments.</para>
    /// </remarks>
    /// <seealso cref="WelcomeEvent"/>
    /// <seealso cref="RolesUpdatedEvent"/>
    /// <seealso cref="Teams.Member"/>
    public class XpAddedEvent : BaseObject
    {
        #region JSON properties
        /// <summary>
        /// The identifiers of all users that received that amount of XP.
        /// </summary>
        /// <remarks>
        /// <para>This contains a set of users that were given <see cref="Amount"/> amount of XP.</para>
        /// <para>The set can contain more than one member.</para>
        /// </remarks>
        /// <value>List of user IDs</value>
        [JsonProperty("userIds", Required = Required.Always)]
        public ISet<GId> Users
        {
            get; set;
        }
        /// <summary>
        /// The amount of XP given to the users.
        /// </summary>
        /// <remarks>
        /// <para>The amount of XP that was given to all users in <see cref="Users"/> set.</para>
        /// <para>This should usually be between 1000 and -1000 amount of XP.</para>
        /// </remarks>
        /// <value>XP</value>
        [JsonProperty(Required = Required.Always)]
        public long Amount
        {
            get; set;
        }
        #endregion

        #region Additional
        /// <summary>
        /// Gets the first XP receiving user.
        /// </summary>
        /// <remarks>
        /// <para>Gets the first user in <see cref="Users"/> set.</para>
        /// </remarks>
        /// <returns>User ID</returns>
        [JsonIgnore]
        public GId FirstUser => Users.First();
        /// <summary>
        /// Gets the last XP receiving user.
        /// </summary>
        /// <remarks>
        /// <para>Gets the last user in <see cref="Users"/> set.</para>
        /// </remarks>
        /// <returns>User ID</returns>
        [JsonIgnore]
        public GId LastUser => Users.Last();
        #endregion
    }
}