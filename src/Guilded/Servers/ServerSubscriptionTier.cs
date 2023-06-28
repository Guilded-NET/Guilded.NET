using System;
using Guilded.Base;
using Newtonsoft.Json;

namespace Guilded.Servers;

/// <summary>
/// Represents <see cref="Server">server's</see> created subscription's tier.
/// </summary>
/// <seealso cref="SubscriptionType" />
/// <seealso cref="Server" />
/// <seealso cref="Role" />
public class SubscriptionTier : ContentModel, IServerBased, ICreationDated
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the <see cref="Server">server</see> of which the <see cref="SubscriptionTier">subscription tier</see> is.
    /// </summary>
    /// <value>The identifier of the <see cref="Server">server</see> of which the <see cref="SubscriptionTier">subscription tier</see> is</value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="Type" />
    /// <seealso cref="RoleId" />
    /// <seealso cref="Cost" />
    /// <seealso cref="CreatedAt" />
    public HashId ServerId { get; }

    /// <summary>
    /// Gets the <see cref="SubscriptionType">tier type</see> of the <see cref="SubscriptionTier">subscription tier</see>.
    /// </summary>
    /// <value>The <see cref="SubscriptionType">tier type</see> of the <see cref="SubscriptionTier">subscription tier</see></value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="RoleId" />
    /// <seealso cref="Cost" />
    /// <seealso cref="CreatedAt" />
    public SubscriptionType Type { get; }

    /// <summary>
    /// Gets the <see cref="Role">role</see> given when subscribing with the <see cref="SubscriptionTier">subscription tier</see>.
    /// </summary>
    /// <value>The <see cref="Role">role</see> given when subscribing with the <see cref="SubscriptionTier">subscription tier</see></value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="Cost" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="CreatedAt" />
    public uint? RoleId { get; }

    /// <summary>
    /// Gets the cost of the <see cref="SubscriptionTier">subscription tier</see> in USD cents.
    /// </summary>
    /// <value>The cost of the <see cref="SubscriptionTier">subscription tier</see> in USD cents</value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="RoleId" />
    /// <seealso cref="Type" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="CreatedAt" />
    public ushort Cost { get; }

    /// <summary>
    /// Gets the given description of the <see cref="SubscriptionTier">subscription tier</see>.
    /// </summary>
    /// <value>The given description of the <see cref="SubscriptionTier">subscription tier</see></value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="CreatedAt" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Type" />
    /// <seealso cref="RoleId" />
    /// <seealso cref="Cost" />
    public string? Description { get; }

    /// <summary>
    /// Gets the date when the <see cref="SubscriptionTier">subscription tier</see> was created.
    /// </summary>
    /// <value>The date when the <see cref="SubscriptionTier">subscription tier</see> was created</value>
    /// <seealso cref="SubscriptionTier" />
    /// <seealso cref="Description" />
    /// <seealso cref="ServerId" />
    /// <seealso cref="Type" />
    /// <seealso cref="RoleId" />
    /// <seealso cref="Cost" />
    public DateTime CreatedAt { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="Group" /> from specified JSON properties.
    /// </summary>
    /// <param name="serverId">The identifier of the <see cref="Server">server</see> of which the <see cref="SubscriptionTier">subscription tier</see> is</param>
    /// <param name="type">The <see cref="SubscriptionType">tier type</see> of the <see cref="SubscriptionTier">subscription tier</see></param>
    /// <param name="cost">The cost of the <see cref="SubscriptionTier">subscription tier</see> in USD cents</param>
    /// <param name="createdAt">The date when the <see cref="SubscriptionTier">subscription tier</see> was created</param>
    /// <param name="roleId">The <see cref="Role">role</see> given when subscribing with the <see cref="SubscriptionTier">subscription tier</see></param>
    /// <param name="description">The given description of the <see cref="SubscriptionTier">subscription tier</see></param>
    /// <returns><see cref="Group" /> from JSON</returns>
    [JsonConstructor]
    public SubscriptionTier(
        [JsonProperty(Required = Required.Always)]
        HashId serverId,

        [JsonProperty(Required = Required.Always)]
        SubscriptionType type,

        [JsonProperty(Required = Required.Always)]
        ushort cost,

        [JsonProperty(Required = Required.Always)]
        DateTime createdAt,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? roleId = null,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string? description = null
    ) =>
        (ServerId, Type, RoleId, Cost, Description, CreatedAt) = (serverId, type, roleId, cost, description, createdAt);
    #endregion
}