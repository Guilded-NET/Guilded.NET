using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base.Servers;
using Guilded.Base.Users;
using Newtonsoft.Json;

namespace Guilded.Base.Content;

/// <summary>
/// Represents a collection of mentions of certain element or user.
/// </summary>
/// <seealso cref="Embeds.Embed" />
public class Mentions
{
    #region Properties

    #region Properties JSON
    /// <summary>
    /// Gets whether <c>@everyone</c> has been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <value>Everyone was mentioned</value>
    public bool Everyone { get; }

    /// <summary>
    /// Gets whether <c>@here</c> has been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <value>Here was mentioned</value>
    public bool Here { get; }

    /// <summary>
    /// Gets the list of <see cref="User">users</see> that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <value>List of <see cref="User">user</see> mentions</value>
    public IList<UserMention>? Users { get; }

    /// <summary>
    /// Gets the list of roles that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <value>List of role mentions</value>
    public IList<RoleMention>? Roles { get; }

    /// <summary>
    /// Gets the list of <see cref="ServerChannel">channels</see> that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <value>List of <see cref="ServerChannel">channel</see> mentions</value>
    public IList<ChannelMention>? Channels { get; }
    #endregion

    /// <summary>
    /// Gets the identifiers of <see cref="User">the users</see> that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <returns><see cref="UserSummary.Id">User IDs</see></returns>
    public IEnumerable<HashId>? UserIds => Users?.Select(x => x.Id);

    /// <summary>
    /// Gets the identifiers of <see cref="ServerChannel">the channels</see> that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <returns><see cref="ServerChannel.Id">Channel IDs</see></returns>
    public IEnumerable<Guid>? ChannelIds => Channels?.Select(x => x.Id);

    /// <summary>
    /// Gets the identifiers of <see cref="ServerChannel">the channels</see> that have been <see cref="Mentions">mentioned</see>.
    /// </summary>
    /// <returns>Role IDs</returns>
    public IEnumerable<uint>? RoleIds => Roles?.Select(x => x.Id);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Mentions" /> from the specified JSON properties.
    /// </summary>
    /// <param name="everyone">Whether <c>@everyone</c> has been <see cref="Mentions">mentioned</see></param>
    /// <param name="here">Whether <c>@here</c> has been <see cref="Mentions">mentioned</see></param>
    /// <param name="users">The list of <see cref="User">users</see> that have been <see cref="Mentions">mentioned</see></param>
    /// <param name="channels">The list of <see cref="ServerChannel">channels</see> that have been <see cref="Mentions">mentioned</see></param>
    /// <param name="roles">The list of roles that have been <see cref="Mentions">mentioned</see></param>
    /// <returns>New <see cref="Mentions" /> JSON instance</returns>
    public Mentions(
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool everyone = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool here = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<UserMention>? users = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<ChannelMention>? channels = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<RoleMention>? roles = null
    ) =>
        (Everyone, Here, Users, Channels, Roles) = (everyone, here, users, channels, roles);
    #endregion

    #region Types
    /// <summary>
    /// Represents a mention of <see cref="User">a user</see>.
    /// </summary>
    public class UserMention : IModelHasId<HashId>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of <see cref="User">the mentioned user</see>.
        /// </summary>
        /// <value><see cref="UserSummary.Id">User ID</see></value>
        public HashId Id { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UserMention" />.
        /// </summary>
        /// <param name="id">The identifier of <see cref="User">the mentioned user</see></param>
        /// <returns>New <see cref="UserMention" /> JSON instance</returns>
        public UserMention(
            [JsonProperty(Required = Required.Always)]
            HashId id
        ) =>
            Id = id;
        #endregion
    }

    /// <summary>
    /// Represents a mention of <see cref="ServerChannel">a channel</see>.
    /// </summary>
    public class ChannelMention : IModelHasId<Guid>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of <see cref="ServerChannel">the mentioned channel</see>.
        /// </summary>
        /// <value><see cref="ServerChannel.Id">Channel ID</see></value>
        public Guid Id { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ServerChannel" />.
        /// </summary>
        /// <param name="id">The identifier of <see cref="ServerChannel">the mentioned channel</see></param>
        /// <returns>New <see cref="ChannelMention" /> JSON instance</returns>
        public ChannelMention(
            [JsonProperty(Required = Required.Always)]
            Guid id
        ) =>
            Id = id;
        #endregion
    }

    /// <summary>
    /// Represents a mention of a role.
    /// </summary>
    public class RoleMention : IModelHasId<uint>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of the mentioned role.
        /// </summary>
        /// <value>Role ID</value>
        public uint Id { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="RoleMention" /> .
        /// </summary>
        /// <param name="id">The identifier of the mentioned role channel</param>
        /// <returns>New <see cref="RoleMention" /> JSON instance</returns>
        public RoleMention(
            [JsonProperty(Required = Required.Always)]
            uint id
        ) =>
            Id = id;
        #endregion
    }
    #endregion
}