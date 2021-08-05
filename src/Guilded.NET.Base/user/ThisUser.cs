using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// Current user account being controlled by this client.
    /// </summary>
    public class ThisUser : User
    {
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        [JsonProperty(Required = Required.Always)]
        public IList<SocialLink> SocialLinks
        {
            get; set;
        }
        /// <summary>
        /// Users which have been blocked by this client's user.
        /// </summary>
        /// <value>List of user IDs</value>
        [JsonProperty(Required = Required.Always)]
        public IList<GId> BlockedUsers
        {
            get; set;
        }
        /// <summary>
        /// If simplified navigation is turned on.
        /// </summary>
        /// <value>Simplified navigation</value>
        [JsonProperty("useMinimalNav")]
        public bool MinimalNav
        {
            get; set;
        } = true;
        /*/// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public async Task ChangeNameAsync(string name) =>
            await ParentClient.ChangeNameAsync(name);*/
    }
}