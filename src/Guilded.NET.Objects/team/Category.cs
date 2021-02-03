using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guilded.NET.Objects.Teams {
    using Guilded.NET.Objects.Permissions;
    /// <summary>
    /// A Guilded channel category.
    /// </summary>
    public class Category: TeamChannel<uint> {
        /// <summary>
        /// A Guilded channel category.
        /// </summary>
        public Category() =>
            ChannelCategoryId = null;
        /// <summary>
        /// ID of the category this channel is in.
        /// </summary>
        /// <value>Nullable Channel ID</value>
        [JsonProperty("channelCategoryId", Required = Required.AllowNull)]
        public uint? ChannelCategoryId {
            get; set;
        }


        
        /// <summary>
        /// Adds a role to a category.
        /// </summary>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated category</returns>
        public async Task<Category> AddCategoryRoleAsync(uint roleId) =>
            await ParentClient.AddCategoryRoleAsync(TeamId, Id, roleId);
        /// <summary>
        /// Adds a role to a category.
        /// </summary>
        /// <param name="roleId">ID of the role to add</param>
        /// <returns>Updated category</returns>
        public Category AddCategoryRole(uint roleId) =>
            ParentClient.AddCategoryRole(TeamId, Id, roleId);
        /// <summary>
        /// Removes a role from a category.
        /// </summary>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated category</returns>
        public async Task<Category> RemoveCategoryRoleAsync(uint roleId) =>
            await ParentClient.RemoveCategoryRoleAsync(TeamId, Id, roleId);
        /// <summary>
        /// Removes a role from a category.
        /// </summary>
        /// <param name="roleId">ID of the role to remove</param>
        /// <returns>Updated category</returns>
        public Category RemoveCategoryRole(uint roleId) =>
            ParentClient.RemoveCategoryRole(TeamId, Id, roleId);



        /// <summary>
        /// Turns channel to string.
        /// </summary>
        /// <returns>Channel as a string</returns>
        public override string ToString() => $"Category {Id}: {Name}";
        /// <summary>
        /// Gets category's hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => base.GetHashCode() - 35;
    }
}