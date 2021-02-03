using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    using Chat;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class Channel: TeamChatChannel {
        /// <summary>
        /// Represents Guilded channel.
        /// </summary>
        public Channel() =>
            ChannelCategoryId = null;
        /// <summary>
        /// A description/topic of this channel.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.AllowNull)]
        public string Description {
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
        /// Settings of this channel.
        /// </summary>
        /// <value>Settings</value>
        [JsonProperty("settings", Required = Required.AllowNull)]
        public ChannelSettings Settings {
            get; set;
        }

        //=========================//
        //    Additional
        //=========================//

        /// <summary>
        /// Creates a channel mention based on a given channel.
        /// </summary>
        /// <returns>Channel mention</returns>
        public ChannelMention CreateMention() =>
            ChannelMention.Generate(this);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        public async Task DeleteChannelAsync() =>
            await ParentClient.DeleteChannelAsync(TeamId, GroupId, Id);
        /// <summary>
        /// Deletes a channel in a specific team and group.
        /// </summary>
        public void DeleteChannel() =>
            ParentClient.DeleteChannel(TeamId, GroupId, Id);
        /// <summary>
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        public async Task AssignToCategoryAsync(uint categoryId, bool shouldRoleSync = false) =>
            await ParentClient.AssignToCategoryAsync(TeamId, categoryId, Id, shouldRoleSync);
        /// <summary>
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        public void AssignToCategory(uint categoryId, bool shouldRoleSync) =>
            ParentClient.AssignToCategory(TeamId, categoryId, Id, shouldRoleSync);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        public async Task UnassignFromCategoryAsync() =>
            await ParentClient.UnassignFromCategoryAsync(TeamId, Id);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        public void UnassignFromCategory() =>
            ParentClient.UnassignFromCategory(TeamId, Id);
        /// <summary>
        /// Clears notifications in this channel.
        /// </summary>
        public async Task ClearNotificationsAsync() =>
            await ParentClient.ClearNotificationsAsync(Id);
        /// <summary>
        /// Clears notifications in this channel.
        /// </summary>
        public void ClearNotifications() =>
            ParentClient.ClearNotifications(Id);
        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated channel</returns>
        public async Task<Channel> AddChannelRoleAsync(uint roleId) =>
            await ParentClient.AddChannelRoleAsync(TeamId, Id, roleId);
        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated channel</returns>
        public Channel AddChannelRole(uint roleId) =>
            ParentClient.AddChannelRole(TeamId, Id, roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated channel</returns>
        public async Task<Channel> RemoveChannelRoleAsync(uint roleId) =>
            await ParentClient.RemoveChannelRoleAsync(TeamId, Id, roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated channel</returns>
        public Channel RemoveChannelRole(uint roleId) =>
            ParentClient.RemoveChannelRole(TeamId, Id, roleId);

        //=========================//
        //    Overrides
        //=========================//

        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"Channel {Id}: {Name}";
        /// <summary>
        /// Gets channel's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() - 50;
    }
}