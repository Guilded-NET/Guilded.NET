using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Objects.Events
{
    using Teams;
    /// <summary>
    /// An update which was applied to the channel.
    /// </summary>
    public class ChannelUpdate : ClientObject
    {
        /// <summary>
        /// ID of this channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("id")]
        public Guid? Id
        {
            get; set;
        } = null;
        /// <summary>
        /// Name of this channel.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("name")]
        public string Name
        {
            get; set;
        } = null;
        /// <summary>
        /// A description/topic of this channel.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description")]
        public string Description
        {
            get; set;
        } = null;
        /// <summary>
        /// Whether or not this channel is public.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty("isPublic")]
        public bool? IsPublic
        {
            get; set;
        } = null;
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        [JsonProperty("channelCategoryId")]
        [DefaultValue(null)]
        public uint? ChannelCategoryId
        {
            get; set;
        }
        /// <summary>
        /// Settings of this channel.
        /// </summary>
        /// <value>Settings</value>
        [JsonProperty("settings")]
        [DefaultValue(null)]
        public ChannelSettings Settings
        {
            get; set;
        }
        /// <summary>
        /// Who archived this channel.
        /// </summary>
        /// <value>Archived by</value>
        [JsonProperty("archivedBy")]
        [DefaultValue(null)]
        public GId ArchivedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel got archived.
        /// </summary>
        /// <value>Archived at</value>
        [JsonProperty("archivedAt")]
        [DefaultValue(null)]
        public DateTime? ArchivedAt
        {
            get; set;
        }
        /// <summary>
        /// Type of this channel.
        /// </summary>
        /// <value>Content Type</value>
        [JsonProperty("contentType")]
        [DefaultValue(null)]
        public ChannelType ContentType
        {
            get; set;
        }
        /// <summary>
        /// ID of the parent channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("parentChannelId")]
        [DefaultValue(null)]
        public Guid? ParentChannel
        {
            get; set;
        }
        /// <summary>
        /// When this channel got deleted.
        /// </summary>
        /// <value>Deleted at</value>
        [JsonProperty("deletedAt")]
        [DefaultValue(null)]
        public DateTime? DeletedAt
        {
            get; set;
        }
        /// <summary>
        /// Who created this channel.
        /// </summary>
        /// <value>Created by</value>
        [JsonProperty("createdBy")]
        [DefaultValue(null)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When this channel should get archived.
        /// </summary>
        /// <value>Auto archive at</value>
        [JsonProperty("autoArchiveAt")]
        [DefaultValue(null)]
        public DateTime? AutoArchiveAt
        {
            get; set;
        }
        /// <summary>
        /// Which webhook created this channel.
        /// </summary>
        /// <value>Created by webhook ID</value>
        [JsonProperty("createdByWebhookId")]
        [DefaultValue(null)]
        public Guid? CreatedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// Which webhook archived this channel.
        /// </summary>
        /// <value>Archived by webhook ID</value>
        [JsonProperty("archivedByWebhookId")]
        [DefaultValue(null)]
        public Guid? ArchivedByWebhook
        {
            get; set;
        }
        /// <summary>
        /// Priority/sort index of this channel.
        /// </summary>
        /// <value>Priority</value>
        [JsonProperty("priority")]
        [DefaultValue(null)]
        public long? Priority
        {
            get; set;
        }
        /// <summary>
        /// Permissions of the roles in this channel.
        /// </summary>
        /// <value>Role Permissions</value>
        [JsonProperty("rolesById")]
        [DefaultValue(null)]
        public IDictionary<string, ChannelPermission> RolePermissions
        {
            get; set;
        }
        /// <summary>
        /// Permissions of the users in this channel.
        /// </summary>
        /// <value>User Permissions</value>
        [JsonProperty("userPermissions")]
        [DefaultValue(null)]
        public IList<UserPermission> UserPermissions
        {
            get; set;
        }
        /// <summary>
        /// ID of team this channel is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId")]
        [DefaultValue(null)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// ID of the group this channel is in.
        /// </summary>
        /// <value>Group ID</value>
        [JsonProperty("groupId")]
        [DefaultValue(null)]
        public GId GroupId
        {
            get; set;
        }
    }
}