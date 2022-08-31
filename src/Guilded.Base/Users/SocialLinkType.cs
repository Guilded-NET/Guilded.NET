using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the type of a <see cref="SocialLink">social link</see>.
/// </summary>
/// <seealso cref="SocialLink" />
/// <seealso cref="User" />
/// <seealso cref="UserSummary" />
[JsonConverter(typeof(StringEnumConverter), true)]
public enum SocialLinkType
{
    /// <summary>
    /// User's Twitch streaming platform socials.
    /// </summary>
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Twitch,

    /// <summary>
    /// User's Battle.NET launcher profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Bnet,

    /// <summary>
    /// User's PlayStation Network profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Psn,

    /// <summary>
    /// User's XBOX One profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Xbox,

    /// <summary>
    /// User's Steam game store profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Steam,

    /// <summary>
    /// User's Origin profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Origin,

    /// <summary>
    /// User's YouTube video sharing platform socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    [EnumMember(Value = "youtube")]
    YouTube,

    /// <summary>
    /// User's Twitter social media platform socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Twitter,

    /// <summary>
    /// User's Facebook social media platform socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Facebook,

    /// <summary>
    /// User's Nintendo Switch profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Switch,

    /// <summary>
    /// User's Patreon profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Roblox" />
    /// <seealso cref="SocialLinkType" />
    Patreon,

    /// <summary>
    /// User's Roblox profile socials.
    /// </summary>
    /// <seealso cref="Twitch" />
    /// <seealso cref="Bnet" />
    /// <seealso cref="Psn" />
    /// <seealso cref="Xbox" />
    /// <seealso cref="Steam" />
    /// <seealso cref="Origin" />
    /// <seealso cref="YouTube" />
    /// <seealso cref="Twitter" />
    /// <seealso cref="Facebook" />
    /// <seealso cref="Switch" />
    /// <seealso cref="Patreon" />
    /// <seealso cref="SocialLinkType" />
    Roblox
}