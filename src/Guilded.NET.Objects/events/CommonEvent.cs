using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Guilded.NET.Objects.Chat;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// A base for events in both teams(reaction events, message events) and DMs.
    /// </summary>
    public class CommonEvent : ClientEvent
    {
        /// <summary>
        /// ID of the channel this event appeared. Can be a DM channel or a team channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// ID of the category this reaction's channel is.
        /// </summary>
        /// <value>Category ID</value>
        [JsonProperty("channelCategoryId")]
        [DefaultValue(null)]
        public uint? CategoryId
        {
            get; set;
        }
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty("channelType", Required = Required.Always)]
        public ChatType ChannelType
        {
            get; set;
        }
        /// <summary>
        /// ID of the team this reaction is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId")]
        [DefaultValue(null)]
        public GId TeamId
        {
            get; set;
        }

        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="response">Message response</param>
        /// <returns>Async task</returns>
        public async Task<object> RespondAsync(NewMessage response) =>
            await ParentClient.SendMessageAsync(ChannelId, response);
        /// <summary>
        /// Sends a message in the same channel as the given message.
        /// </summary>
        /// <param name="response">Message response</param>
        public void Respond(NewMessage response) =>
            ParentClient.SendMessage(ChannelId, response);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <returns>Parent channel</returns>
        public async Task<BaseChannel> GetChannelAsync() =>
            ChannelType == ChatType.Team
            ? (BaseChannel)await ParentClient.GetChannelAsync(TeamId, ChannelId)
            : (await ParentClient.GetDMChannelsAsync()).FirstOrDefault(x => x.Id == ChannelId);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <returns>Parent channel</returns>
        public BaseChannel GetChannel() =>
            ChannelType == ChatType.Team
            ? (BaseChannel)ParentClient.GetChannel(TeamId, ChannelId)
            : ParentClient.GetDMChannels().FirstOrDefault(x => x.Id == ChannelId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public async Task<Team> GetTeamAsync() =>
            ChannelType == ChatType.Team
            ? await ParentClient.GetTeamAsync(TeamId)
            : null;
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public Team GetTeam() =>
            ChannelType == ChatType.Team
            ? ParentClient.GetTeam(TeamId)
            : null;
    }
}