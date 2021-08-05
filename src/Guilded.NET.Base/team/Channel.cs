using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Teams
{
    using Chat;
    /// <summary>
    /// Represents Guilded channel.
    /// </summary>
    public class Channel : TeamChannel
    {
        #region JSON properties
        /// <summary>
        /// A description/topic of this channel.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.AllowNull)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// Settings of this channel.
        /// </summary>
        /// <value>Settings</value>
        [JsonProperty(Required = Required.AllowNull)]
        public ChannelSettings Settings
        {
            get; set;
        }
        #endregion


        /*#region Additional
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
        /// Assigns a channel to a specific category.
        /// </summary>
        /// <param name="categoryId">Category where channel should be in</param>
        /// <param name="shouldRoleSync">If role permissions should be synced with category</param>
        public async Task AssignToCategoryAsync(uint categoryId, bool shouldRoleSync = false) =>
            await ParentClient.AssignToCategoryAsync(TeamId, categoryId, Id, shouldRoleSync);
        /// <summary>
        /// Unassigns a channel from a category.
        /// </summary>
        public async Task UnassignFromCategoryAsync() =>
            await ParentClient.UnassignFromCategoryAsync(TeamId, Id);
        /// <summary>
        /// Clears notifications in this channel.
        /// </summary>
        public async Task ClearNotificationsAsync() =>
            await ParentClient.ClearNotificationsAsync(Id);
        /// <summary>
        /// Adds a role to a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated channel</returns>
        public async Task<Channel> AddChannelRoleAsync(uint roleId) =>
            await ParentClient.AddChannelRoleAsync(TeamId, Id, roleId);
        /// <summary>
        /// Removes a role from a channel.
        /// </summary>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated channel</returns>
        public async Task<Channel> RemoveChannelRoleAsync(uint roleId) =>
            await ParentClient.RemoveChannelRoleAsync(TeamId, Id, roleId);
        #endregion*/


        #region Overrides
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
        #endregion
    }
}