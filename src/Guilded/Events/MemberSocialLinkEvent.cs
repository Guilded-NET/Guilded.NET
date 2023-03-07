using System;
using Guilded.Base;
using Guilded.Client;
using Guilded.Servers;
using Guilded.Users;
using Newtonsoft.Json;

namespace Guilded.Events;

/// <summary>
/// Represents an event that occurs when a <see cref="Member">member</see> gets their <see cref="Users.SocialLink">social link</see> created, updated or deleted.
/// </summary>
/// <seealso cref="MemberBanEvent" />
/// <seealso cref="MemberJoinedEvent" />
/// <seealso cref="MemberUpdatedEvent" />
/// <seealso cref="MemberRemovedEvent" />
/// <seealso cref="Member" />
public class MemberSocialLinkEvent : IServerBased
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="Users.SocialLink">social link</see> received from the <see cref="MemberSocialLinkEvent">event</see>.
    /// </summary>
    /// <value>The <see cref="Users.SocialLink">social link</see> received from the <see cref="MemberSocialLinkEvent">event</see></value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="User" />
    /// <seealso cref="ServerId" />
    public SocialLink SocialLink { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Users.SocialLink">social link</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Users.SocialLink">social link</see></value>
    /// <seealso cref="MemberBanEvent" />
    /// <seealso cref="MemberBan" />
    public HashId ServerId { get; }
    #endregion

    #region Properties Additional
    /// <inheritdoc cref="SocialLink.UserId" />
    public HashId UserId => SocialLink.UserId;

    /// <inheritdoc cref="SocialLink.Type" />
    public SocialLinkType Type => SocialLink.Type;

    /// <inheritdoc cref="SocialLink.Handle" />
    public string? Handle => SocialLink.Handle;

    /// <inheritdoc cref="SocialLink.ServiceId" />
    public string? ServiceId => SocialLink.ServiceId;

    /// <inheritdoc cref="IHasParentClient.ParentClient" />
    public AbstractGuildedClient ParentClient => SocialLink.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="MemberBanEvent" /> from the specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where member got banned/unbanned</param>
    /// <param name="socialLink">The information about the member's ban</param>
    /// <returns>New <see cref="MemberBanEvent" /> JSON instance</returns>
    /// <seealso cref="MemberBanEvent" />
    [JsonConstructor]
    public MemberSocialLinkEvent(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        SocialLink socialLink
    ) =>
        (ServerId, SocialLink) = (serverId, socialLink);
    #endregion
}