using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class Channel: ClientObject, ITeamChannel {
        /// <summary>
        /// Represents Guilded channel.
        /// </summary>
        public Channel() =>
            (ParentChannel, ChannelCategoryId, AddedAt) = (null, null, null);
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        [JsonProperty("name")]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Description of this channel.
        /// </summary>
        /// <value>Channel Description</value>
        [JsonProperty("description")]
        public string Description {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("rolesById")]
        public IDictionary<string, ChannelPermission> RolePermissions {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("userPermissions")]
        public IList<UserPermission> UserPermissions {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("teamId")]
        public GId TeamId {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("channelCategoryId")]
        public uint? ChannelCategoryId {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("addedAt")]
        public DateTime? AddedAt {
            get; set;
        }
        /// <summary>
        /// If role permissions are synced with the category.
        /// </summary>
        /// <value></value>
        [JsonProperty("isRoleSynced")]
        public bool? IsRoleSynced {
            get; set;
        }
        /// <summary>
        /// Whether or not this channel is public.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPublic")]
        public bool IsPublic {
            get; set;
        }
        /// <inheritdoc/>
        [JsonProperty("groupId")]
        public GId GroupId {
            get; set;
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// When the channel was archived.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("archivedAt", Required = Required.AllowNull)]
        public DateTime? ArchivedAt {
            get; set;
        }
        /// <summary>
        /// User who archived it.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("archivedBy", Required = Required.AllowNull)]
        public GId ArchivedBy {
            get; set;
        }
        /// <summary>
        /// ID of the parent channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("parentChannelId")]
        public Guid? ParentChannel {
            get; set;
        }
        /// <summary>
        /// Auto archive date.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("autoArchiveAt", Required = Required.AllowNull)]
        public DateTime? AutoArchiveAt {
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
        public override string ToString() => $"Channel({Id})";
        /// <summary>
        /// Whether or not objects are equal.
        /// </summary>
        /// <param name="obj">Equals to</param>
        /// <returns>If it's equal to other object</returns>
        public override bool Equals(object obj) {
            if(obj is Channel ch) return ch.TeamId == TeamId && ch.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not channels are equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(Channel ch0, Channel ch1) => ch0.TeamId == ch1.TeamId && ch0.Id == ch1.Id;
        /// <summary>
        /// Whether or not channels are not equal.
        /// </summary>
        /// <param name="ch0">First channel to be compared</param>
        /// <param name="ch1">Second channel to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(Channel ch0, Channel ch1) => !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (TeamId.GetHashCode() + Id.GetHashCode() + 2000) / 2;
    }
}