using System;
using System.Collections.Generic;
using Guilded.NET.Objects.Chat;
using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    using Teams;
    /// <summary>
    /// Represents DMs and DM groups.
    /// </summary>
    public class DMChannel: ClientObject, IChannel {
        /// <summary>
        /// Represents DMs and DM groups.
        /// </summary>
        public DMChannel() =>
            (Priority, Name) = (null, null);
        /// <summary>
        /// Priority of this channel.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority")]
        public long? Priority {
            get; set;
        }
        /// <summary>
        /// ID of this channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id {
            get; set;
        }
        /// <summary>
        /// All users in this DM channel.
        /// </summary>
        /// <value>DM channel users</value>
        [JsonProperty("users")]
        public IList<DMUser> Users {
            get; set;
        }
        /// <summary>
        /// If this DM channel is a group or default.
        /// </summary>
        /// <value>Type</value>
        [JsonProperty("dmType")]
        public DMType? DMType {
            get; set;
        }
        /// <summary>
        /// Name of this channel.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name")]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Last message posted in this channel.
        /// </summary>
        /// <value>Last message</value>
        [JsonProperty("lastMessage")]
        public Message LastMessage {
            get; set;
        }
        /// <summary>
        /// When the channel was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// Who created the channel.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy")]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// When the channel was updated.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <summary>
        /// Type of the channel.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("contentType", Required = Required.Always)]
        public ChannelType Type {
            get; set;
        }
        /// <summary>
        /// Date when it was deleted.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("deletedAt", Required = Required.AllowNull)]
        public DateTime? DeletedAt {
            get; set;
        }
        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"DM Channel({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is DMChannel ch) return ch.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not channels are equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(DMChannel ch0, DMChannel ch1) => ch0.Id == ch1.Id;
        /// <summary>
        /// Whether or not channels are not equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(DMChannel ch0, DMChannel ch1) => !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (Id.GetHashCode() + 2000) / 2;
    }
}