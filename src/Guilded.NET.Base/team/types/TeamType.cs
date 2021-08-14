using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// A type of a team(clan, community, guild, etc.).
    /// </summary>
    /// <seealso cref="ChannelType"/>
    /// <seealso cref="SectionType"/>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum TeamType
    {
        /// <summary>
        /// 
        /// </summary>
        Team,
        /// <summary>
        /// 
        /// </summary>
        Organization,
        /// <summary>
        /// 
        /// </summary>
        Community,
        /// <summary>
        /// 
        /// </summary>
        Clan,
        /// <summary>
        /// 
        /// </summary>
        Guild,
        /// <summary>
        /// 
        /// </summary>
        Friends,
        /// <summary>
        /// 
        /// </summary>
        Streaming,
        /// <summary>
        /// 
        /// </summary>
        Other
    }
}