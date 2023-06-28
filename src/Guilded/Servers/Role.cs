using System;
using System.Collections.Generic;
using System.Drawing;
using Guilded.Base;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents a <see cref="Server">server</see> role.
/// </summary>
/// <seealso cref="Member" />
/// <seealso cref="MemberBan" />
/// <seealso cref="Permission" />
public class Role : ContentModel, IModelHasId<uint>, ICreationDated, IUpdatableContent, IServerBased
{
    #region Properties Identification
    /// <summary>
    /// Gets the identifier of the <see cref="Role">role</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="BotUserId" />
    /// <seealso cref="Name" />
    public uint Id { get; }

    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> is</value>
    /// <seealso cref="Role" />
    /// <seealso cref="Id" />
    /// <seealso cref="BotUserId" />
    /// <seealso cref="Name" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the identifier of the bot that owns and gets represented by the <see cref="Role">role</see> in the role list.
    /// </summary>
    /// <remarks>
    /// <para>If this property has a value, the associated <see cref="Role">role</see> can only be deleted by removing the bot with this identifier.</para>
    /// </remarks>
    /// <value>The identifier of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="Id" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Name" />
    public Guid? BotUserId { get; }

    /// <summary>
    /// Gets the displayed name of the <see cref="Role">role</see>.
    /// </summary>
    /// <value>The displayed name of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="Icon" />
    /// <seealso cref="Colors" />
    /// <seealso cref="Id" />
    public string Name { get; }

    /// <summary>
    /// Gets the <see cref="Uri">URL</see> to the icon image of the <see cref="Role">role</see>.
    /// </summary>
    /// <value>The <see cref="Uri">URL</see> to the icon image of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="Name" />
    /// <seealso cref="Colors" />
    /// <seealso cref="Id" />
    public Uri? Icon { get; }

    /// <summary>
    /// Gets the displayed colours of the <see cref="Role">role</see>.
    /// </summary>
    /// <value>The displayed colours of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="Name" />
    /// <seealso cref="Icon" />
    /// <seealso cref="Id" />
    public IList<Color>? Colors { get; }

    /// <summary>
    /// Gets the position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list.
    /// </summary>
    /// <remarks>
    /// <para>The position is in the descending order, which means that the lower the number is, the lower/later the role appears in the role list. The lowest/last role will always be the <see cref="IsBase">base role</see>.</para>
    /// <para>It is not a guarantee that the base/lowest/last role will be <c>1</c> or even a <c>0</c>. It appears that the lowest role can even be <c>2</c>.</para>
    /// </remarks>
    /// <value>The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</value>
    /// <seealso cref="Role" />
    /// <seealso cref="Name" />
    /// <seealso cref="Icon" />
    /// <seealso cref="Id" />
    public uint Position { get; }
    #endregion

    #region Properties Attributes
    /// <summary>
    /// Gets whether the <see cref="Role">role</see> is a default <see cref="Member">member</see> <see cref="Role">role</see>.
    /// </summary>
    /// <value>Whether the <see cref="Role">role</see> is a default <see cref="Member">member</see> <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="IsDisplayedSeparately" />
    /// <seealso cref="IsSelfAssignable" />
    /// <seealso cref="IsMentionable" />
    /// <seealso cref="Permissions" />
    public bool IsBase { get; }

    /// <summary>
    /// Gets whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others.
    /// </summary>
    /// <value>Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</value>
    /// <seealso cref="Role" />
    /// <seealso cref="IsBase" />
    /// <seealso cref="IsSelfAssignable" />
    /// <seealso cref="IsMentionable" />
    /// <seealso cref="Permissions" />
    public bool IsDisplayedSeparately { get; }

    /// <summary>
    /// Gets whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see>.
    /// </summary>
    /// <value>Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="IsBase" />
    /// <seealso cref="IsDisplayedSeparately" />
    /// <seealso cref="IsMentionable" />
    /// <seealso cref="Permissions" />
    public bool IsSelfAssignable { get; }

    /// <summary>
    /// Gets whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged.
    /// </summary>
    /// <value>Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</value>
    /// <seealso cref="Role" />
    /// <seealso cref="IsBase" />
    /// <seealso cref="IsDisplayedSeparately" />
    /// <seealso cref="IsSelfAssignable" />
    /// <seealso cref="Permissions" />
    public bool IsMentionable { get; }

    /// <summary>
    /// Gets the permissions of the <see cref="Role">role</see>.
    /// </summary>
    /// <value>The permissions of the <see cref="Role">role</see></value>
    /// <seealso cref="Role" />
    /// <seealso cref="IsBase" />
    /// <seealso cref="IsDisplayedSeparately" />
    /// <seealso cref="IsSelfAssignable" />
    /// <seealso cref="IsMentionable" />
    public IList<Permission> Permissions { get; }
    #endregion

    #region Properties When
    /// <summary>
    /// Gets the date when the <see cref="Role">role</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="Role">role</see> was created</value>
    /// <seealso cref="Role" />
    /// <seealso cref="UpdatedAt" />
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date when the <see cref="Role">role</see> was last updated.
    /// </summary>
    /// <value>The date when the <see cref="Role">role</see> was last updated</value>
    /// <seealso cref="Role" />
    /// <seealso cref="CreatedAt" />
    public DateTime? UpdatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Role" /> from specified JSON properties.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Role">role</see></param>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> where the <see cref="Role">role</see> is</param>
    /// <param name="name">The displayed name of the <see cref="Role">role</see></param>
    /// <param name="position">The position of the <see cref="Role">role</see> in the <see cref="Server">server's</see> role list</param>
    /// <param name="permissions">The permissions of the <see cref="Role">role</see></param>
    /// <param name="createdAt">The date when the <see cref="Role">role</see> was created</param>
    /// <param name="icon">The <see cref="Uri">URL</see> to the icon image of the <see cref="Role">role</see></param>
    /// <param name="colors">The displayed colours of the <see cref="Role">role</see></param>
    /// <param name="isBase">Whether the <see cref="Role">role</see> is a default <see cref="Member">member</see> <see cref="Role">role</see></param>
    /// <param name="isDisplayedSeparately">Whether the <see cref="Role">role</see> displays its <see cref="Member">members</see> separately from others</param>
    /// <param name="isSelfAssignable">Whether <see cref="Member">members</see> are allowed to assign themselves the <see cref="Role">role</see></param>
    /// <param name="isMentionable">Whether the <see cref="Role">role</see> can be mentioned and its <see cref="Member">members</see> get pinged</param>
    /// <param name="updatedAt">The date when the <see cref="Role">role</see> was last updated</param>
    /// <returns><see cref="Role" /> from JSON</returns>
    [JsonConstructor]
    public Role(
        [JsonProperty(Required = Required.Always)]
        uint id,

        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        string name,

        [JsonProperty(Required = Required.Always)]
        uint position,

        [JsonProperty(Required = Required.Always)]
        IList<Permission> permissions,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        Uri? icon = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IList<Color>? colors = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isBase = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isDisplayedSeparately = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isSelfAssignable = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        bool isMentionable = false,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        DateTime? updatedAt = null
    ) =>
        (Id, ServerId, Name, Icon, Colors, Position, IsBase, IsDisplayedSeparately, IsSelfAssignable, IsMentionable, Permissions, CreatedAt, UpdatedAt) = (id, serverId, name, icon, colors, position, isBase, isDisplayedSeparately, isSelfAssignable, isMentionable, permissions, createdAt, updatedAt);
    #endregion
}