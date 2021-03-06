using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams
{
    /// <summary>
    /// A room in a voice channel.
    /// </summary>
    public class VoiceRoom : ClientObject
    {
        /// <summary>
        /// ID of the voice room.
        /// </summary>
        /// <value>Voice room ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// A name of this voice group.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// Order of voice room.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority", Required = Required.AllowNull)]
        public long? Priority
        {
            get; set;
        }
        /// <summary>
        /// Parent channel's ID.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("channelId", Required = Required.Always)]
        public Guid ChannelId
        {
            get; set;
        }
        /// <summary>
        /// How many users can be in this voice room.
        /// </summary>
        /// <value>Voice room limit</value>
        [JsonProperty("userLimit", Required = Required.AllowNull)]
        public uint? UserLimit
        {
            get; set;
        }
        /// <summary>
        /// When the voice room was created.
        /// </summary>
        /// <value>Created at</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// ID of the parent voice room.
        /// </summary>
        /// <value>Voice room ID</value>
        [JsonProperty("parentGroupId", Required = Required.AllowNull)]
        public uint? ParentRoomId
        {
            get; set;
        }
    }
}