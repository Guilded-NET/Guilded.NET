using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Servers;

/// <summary>
/// Represents a tier type of <see cref="SubscriptionTier">server subscriptions</see>.
/// </summary>
/// <seealso cref="SubscriptionTier" />
/// <seealso cref="Server" />
[JsonConverter(typeof(StringEnumConverter))]
public enum SubscriptionType
{
    /// <summary>
    /// The gold/highest server subscription tier type.
    /// </summary>
    /// <seealso cref="SubscriptionType" />
    /// <seealso cref="Silver" />
    /// <seealso cref="Copper" />
    Gold,

    /// <summary>
    /// The silver/medium server subscription tier type.
    /// </summary>
    /// <seealso cref="SubscriptionType" />
    /// <seealso cref="Gold" />
    /// <seealso cref="Copper" />
    Silver,

    /// <summary>
    /// The copper/lowest server subscription tier type.
    /// </summary>
    /// <seealso cref="SubscriptionType" />
    /// <seealso cref="Gold" />
    /// <seealso cref="Silver" />
    Copper
}