using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;

namespace Guilded.NET.Objects.Teams {
    using Permissions;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class ThreadChannel: ClientObject, ITeamChannel {
        /// <summary>
        /// Represents Guilded channel.
        /// </summary>
        public ThreadChannel() =>
            AddedAt = null;
        /// <summary>
        /// Priority of this channel.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority", Required = Required.AllowNull)]
        public long? Priority {
            get; set;
        }
        /// <summary>
        /// ID of this thread.
        /// </summary>
        /// <value>Thread ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id {
            get; set;
        }
        /// <summary>
        /// Name of this thread.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name", Required = Required.Always)]
        public string Name {
            get; set;
        }
        /// <summary>
        /// Description of this thread.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// Permissions of the roles in this channel.
        /// </summary>
        /// <value>Role Permissions</value>
        [JsonProperty("rolesById", Required = Required.AllowNull)]
        public IDictionary<string, ChannelPermission> RolePermissions {
            get; set;
        }
        /// <summary>
        /// Permissions of the users in this channel.
        /// </summary>
        /// <value>User Permissions</value>
        [JsonProperty("userPermissions", Required = Required.AllowNull)]
        public IList<UserPermission> UserPermissions {
            get; set;
        }
        /// <summary>
        /// ID of team this channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        [JsonProperty("channelCategoryId", Required = Required.AllowNull)]
        public uint? ChannelCategoryId {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("addedAt", Required = Required.AllowNull)]
        public DateTime? AddedAt {
            get; set;
        }
        /// <summary>
        /// If role permissions are synced with the category.
        /// </summary>
        /// <value></value>
        [JsonProperty("isRoleSynced", Required = Required.AllowNull)]
        public bool? IsRoleSynced {
            get; set;
        }
        /// <summary>
        /// Whether or not this channel is public.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPublic", Required = Required.AllowNull)]
        public bool? IsPublic {
            get; set;
        }
        /// <summary>
        /// ID of the group this channel is in.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId", Required = Required.AllowNull)]
        public GId GroupId {
            get; set;
        }
        /// <summary>
        /// When the thread was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// Who created the thread.
        /// </summary>
        /// <value>User ID</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// When the thread was updated.
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
        /// When the thread was archived.
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
        /// ID of the parent channel/thread.
        /// </summary>
        /// <value>Channel/Thread ID</value>
        [JsonProperty("parentChannelId", Required = Required.Always)]
        public Guid? ParentChannel {
            get; set;
        }
        /// <summary>
        /// Type of the parent of this thread.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("parentContentType", Required = Required.Always)]
        public ChannelType ParentType {
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
        /// ID of the message this thread is on.
        /// </summary>
        /// <value>Message ID</value>
        [JsonProperty("threadMessageId", Required = Required.Always)]
        public Guid ThreadMessageId {
            get; set;
        }
        /// <summary>
        /// ID of the channel this thread is originally in.
        /// </summary>
        /// <value>Original channel ID</value>
        [JsonProperty("originatingChannelId", Required = Required.Always)]
        public Guid OriginatingChannelId {
            get; set;
        }
        /// <summary>
        /// Type of the channel this thread is originally in.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("originatingChannelContentType", Required = Required.Always)]
        public ChannelType OriginatingChannelType {
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
            if(obj is ThreadChannel ch) return ch.TeamId == TeamId && ch.Id == Id;
            else return false;
        }
        /// <summary>
        /// Whether or not threads are equal.
        /// </summary>
        /// <param name="ch0">First thread to be compared</param>
        /// <param name="ch1">Second thread to be compared</param>
        /// <returns>If it's equal to other object</returns>
        public static bool operator ==(ThreadChannel ch0, ThreadChannel ch1) => ch0.TeamId == ch1.TeamId && ch0.Id == ch1.Id;
        /// <summary>
        /// Whether or not threads are not equal.
        /// </summary>
        /// <param name="ch0">First thread to be compared</param>
        /// <param name="ch1">Second thread to be compared</param>
        /// <returns>If it's not equal to other object</returns>
        public static bool operator !=(ThreadChannel ch0, ThreadChannel ch1) => !(ch0 == ch1);
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => (TeamId.GetHashCode() + Id.GetHashCode() + 2000) / 2;
    }
}