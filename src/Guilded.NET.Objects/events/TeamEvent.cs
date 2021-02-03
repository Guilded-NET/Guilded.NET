using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Events {
    using Teams;
    /// <summary>
    /// A base for events in teams(reaction events, message events).
    /// </summary>
    public class TeamEvent: ClientEvent {
        /// <summary>
        /// A base for events in teams(reaction events, message events).
        /// </summary>
        public TeamEvent() =>
            CategoryId = null;
        /// <summary>
        /// ID of the channel this reaction's message is in.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId {
            get; set;
        }
        /// <summary>
        /// ID of the category this reaction's channel is.
        /// </summary>
        /// <value>Category ID</value>
        [JsonProperty("channelCategoryId", Required = Required.AllowNull)]
        public uint? CategoryId {
            get; set;
        }
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Team</value>
        [JsonProperty("channelType")]
        public ChatType ChannelType {
            get; set;
        }
        /// <summary>
        /// ID of the team this reaction is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
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
            await ParentClient.GetChannelAsync(TeamId, ChannelId);
        /// <summary>
        /// Gets parent channel of this message event.
        /// </summary>
        /// <returns>Parent channel</returns>
        public BaseChannel GetChannel() =>
            ParentClient.GetChannel(TeamId, ChannelId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public async Task<Team> GetTeamAsync() =>
            await ParentClient.GetTeamAsync(TeamId);
        /// <summary>
        /// Gets parent team of this message event.
        /// </summary>
        /// <returns>Parent team</returns>
        public Team GetTeam() =>
            ParentClient.GetTeam(TeamId);
    }
}