using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Content;

/// <summary>
/// Represents a collection of mentions of certain element or user.
/// </summary>
/// <seealso cref="Embed" />
public class Mentions
{
    #region Properties JSON
    /// <summary>
    /// Gets whether <c>@everyone</c> has been mentioned.
    /// </summary>
    /// <value>Whether <c>@everyone</c> has been mentioned</value>
    public bool Everyone { get; }

    /// <summary>
    /// Gets whether <c>@here</c> has been mentioned.
    /// </summary>
    /// <value>Whether <c>@here</c> has been mentioned</value>
    public bool Here { get; }

    /// <summary>
    /// Gets the list of <see cref="User">users</see> that have been mentioned.
    /// </summary>
    /// <value>The list of <see cref="User">users</see> that have been mentioned</value>
    public IList<UserMention>? Users { get; }

    /// <summary>
    /// Gets the list of <see cref="ServerChannel">channels</see> that have been mentioned.
    /// </summary>
    /// <value>The list of <see cref="ServerChannel">channels</see> that have been mentioned</value>
    public IList<ChannelMention>? Channels { get; }

    /// <summary>
    /// Gets the list of roles that have been mentioned.
    /// </summary>
    /// <value>The list of roles that have been mentioned</value>
    public IList<RoleMention>? Roles { get; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the identifiers of the <see cref="User">users</see> that have been mentioned.
    /// </summary>
    /// <returns>The identifiers of the <see cref="User">users</see> that have been mentioned</returns>
    public IEnumerable<HashId>? UserIds => Users?.Select(x => x.Id);

    /// <summary>
    /// Gets the identifiers of the <see cref="ServerChannel">channels</see> that have been mentioned.
    /// </summary>
    /// <returns>The identifiers of the <see cref="ServerChannel">channels</see> that have been mentioned</returns>
    public IEnumerable<Guid>? ChannelIds => Channels?.Select(x => x.Id);

    /// <summary>
    /// Gets the identifiers of the roles that have been mentioned.
    /// </summary>
    /// <returns>The identifiers of the roles that have been mentioned</returns>
    public IEnumerable<uint>? RoleIds => Roles?.Select(x => x.Id);
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Mentions" /> from the specified JSON properties.
    /// </summary>
    /// <param name="everyone">Whether <c>@everyone</c> have been mentioned</param>
    /// <param name="here">Whether <c>@here</c> have been mentioned</param>
    /// <param name="users">The list of <see cref="User">users</see> that have been mentioned</param>
    /// <param name="channels">The list of <see cref="ServerChannel">channels</see> that have been mentioned</param>
    /// <param name="roles">The list of roles that have been mentioned</param>
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
    /// Represents a mention of a <see cref="User">user</see>.
    /// </summary>
    public class UserMention : IModelHasId<HashId>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of the mentioned <see cref="User">user</see>.
        /// </summary>
        /// <value>The identifier of the mentioned <see cref="User">user</see></value>
        public HashId Id { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UserMention" />.
        /// </summary>
        /// <param name="id">The identifier of the mentioned <see cref="User">user</see></param>
        /// <returns>New <see cref="UserMention" /> JSON instance</returns>
        public UserMention(
            [JsonProperty(Required = Required.Always)]
            HashId id
        ) =>
            Id = id;
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the Markdown equivalent to the <see cref="UserMention">mention</see>.
        /// </summary>
        /// <returns>The Markdown equivalent to the <see cref="UserMention">mention</see></returns>
        public override string ToString() =>
            $"<@{Id}>";
        #endregion
    }

    /// <summary>
    /// Represents a mention of a <see cref="ServerChannel">channel</see>.
    /// </summary>
    public class ChannelMention : IModelHasId<Guid>
    {
        #region Properties
        /// <summary>
        /// Gets the identifier of the mentioned <see cref="ServerChannel">channel</see>.
        /// </summary>
        /// <value>The identifier of the mentioned <see cref="ServerChannel">channel</see></value>
        public Guid Id { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ServerChannel" />.
        /// </summary>
        /// <param name="id">The identifier of the mentioned <see cref="ServerChannel">channel</see></param>
        /// <returns>New <see cref="ChannelMention" /> JSON instance</returns>
        public ChannelMention(
            [JsonProperty(Required = Required.Always)]
            Guid id
        ) =>
            Id = id;
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the Markdown equivalent to the <see cref="ChannelMention">mention</see>.
        /// </summary>
        /// <returns>The Markdown equivalent to the <see cref="ChannelMention">mention</see></returns>
        public override string ToString() =>
            $"<#{Id}>";
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
        /// <value>The identifier of the mentioned role</value>
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

        #region Overrides
        /// <summary>
        /// Returns the Markdown equivalent to the <see cref="RoleMention">mention</see>.
        /// </summary>
        /// <returns>The Markdown equivalent to the <see cref="RoleMention">mention</see></returns>
        public override string ToString() =>
            $"<@{Id}>";
        #endregion
    }
    #endregion
}