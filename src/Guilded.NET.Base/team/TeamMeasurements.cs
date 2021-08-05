using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Count of various things in a team.
    /// </summary>
    public class TeamMeasurements : BaseObject
    {
        /// <summary>
        /// How many members are in this server.
        /// </summary>
        /// <value>Member count</value>
        [JsonProperty("numMembers", Required = Required.Always)]
        public uint MemberCount
        {
            get; set;
        }
        /// <summary>
        /// How many people are following this server.
        /// </summary>
        /// <value>Follower count</value>
        [JsonProperty("numFollowers", Required = Required.Always)]
        public uint FollowerCount
        {
            get; set;
        }
        /// <summary>
        /// How many recent matches this team had.
        /// </summary>
        /// <value>Recent matches</value>
        [JsonProperty("numRecentMatches", Required = Required.Always)]
        public uint RecentMatchCount
        {
            get; set;
        }
        /// <summary>
        /// How many recent match wins this team has.
        /// </summary>
        /// <value>Recent match wins</value>
        [JsonProperty("numRecentMatchWins", Required = Required.Always)]
        public uint RecentWinCount
        {
            get; set;
        }
        /// <summary>
        /// How many members and followers there are combined.
        /// </summary>
        /// <value></value>
        [JsonProperty("numFollowersAndMembers", Required = Required.Always)]
        public uint FollowerAndMembers
        {
            get; set;
        }
        /// <summary>
        /// How many members joined yesterday.
        /// </summary>
        /// <value>Members per day</value>
        [JsonProperty("numMembersAddedInLastDay", Required = Required.Always)]
        public uint MembersAddedYesterday
        {
            get; set;
        }
        /// <summary>
        /// How many members joined last week.
        /// </summary>
        /// <value>Members per week</value>
        [JsonProperty("numMembersAddedInLastWeek", Required = Required.Always)]
        public uint MembersAddedLastWeek
        {
            get; set;
        }
        /// <summary>
        /// How many members joined last month.
        /// </summary>
        /// <value>Members per month</value>
        [JsonProperty("numMembersAddedInLastMonth", Required = Required.Always)]
        public uint MembersAddedLastMonth
        {
            get; set;
        }
    }
}