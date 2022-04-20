using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Base.Users;

/// <summary>
/// Represents the type of a <see cref="SocialLink">social link</see>.
/// </summary>
/// <seealso cref="SocialLink"/>
/// <seealso cref="User"/>
/// <seealso cref="UserSummary"/>
[JsonConverter(typeof(StringEnumConverter), true)]
public enum SocialLinkType
{
    /// <summary>
    /// User's Twitch streaming platform socials.
    /// </summary>
    Twitch,
    /// <summary>
    /// User's Battle.NET launcher profile socials.
    /// </summary>
    Bnet,
    /// <summary>
    /// User's PlayStation Network profile socials.
    /// </summary>
    Psn,
    /// <summary>
    /// User's XBOX One profile socials.
    /// </summary>
    Xbox,
    /// <summary>
    /// User's Steam game store profile socials.
    /// </summary>
    Steam,
    /// <summary>
    /// User's Origin profile socials.
    /// </summary>
    Origin,
    /// <summary>
    /// User's YouTube video sharing platform socials.
    /// </summary>
    [EnumMember(Value = "youtube")]
    YouTube,
    /// <summary>
    /// User's Twitter social media platform socials.
    /// </summary>
    Twitter,
    /// <summary>
    /// User's Facebook social media platform socials.
    /// </summary>
    Facebook,
    /// <summary>
    /// User's Nintendo Switch profile socials.
    /// </summary>
    Switch,
    /// <summary>
    /// User's Patreon profile socials.
    /// </summary>
    Patreon,
    /// <summary>
    /// User's Roblox profile socials.
    /// </summary>
    Roblox
}