using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Current user account being controlled by this client.
    /// </summary>
    public class ThisUser: User {
        /// <summary>
        /// Current user account being controlled by this client.
        /// </summary>
        public ThisUser() =>
            MinimalNav = true;
        /// <summary>
        /// A URL subdomain for this user.
        /// </summary>
        /// <value>Subdomain</value>
        [JsonProperty("subdomain", Required = Required.AllowNull)]
        public string Subdomain {
            get; set;
        }
        /// <summary>
        /// Your email address. This will not be visible in profiles of other people.
        /// </summary>
        /// <value>Email</value>
        [JsonProperty("email", Required = Required.Always)]
        public string Email {
            get; set;
        }
        /// <summary>
        /// All social links of this member.
        /// </summary>
        /// <value>Social links</value>
        [JsonProperty("socialLinks", Required = Required.Always)]
        public IList<SocialLink> SocialLinks {
            get; set;
        }
        /// <summary>
        /// Games this user has on their profile.
        /// </summary>
        /// <value>List of game aliases</value>
        [JsonProperty("aliases", Required = Required.Always)]
        public IList<GameAlias> Aliases {
            get; set;
        }
        /// <summary>
        /// Users which have been blocked by this client's user.
        /// </summary>
        /// <value>List of user IDs</value>
        [JsonProperty("blockedUsers", Required = Required.Always)]
        public IList<GId> BlockedUsers {
            get; set;
        }
        /// <summary>
        /// If simplified navigation is turned on.
        /// </summary>
        /// <value>Simplified navigation</value>
        [JsonProperty("useMinimalNav")]
        public bool MinimalNav {
            get; set;
        }
        /// <summary>
        /// If this user can redeem Guilded gold.
        /// </summary>
        /// <value>Redeem gold</value>
        [JsonProperty("canRedeemGold", Required = Required.Always)]
        public bool CanRedeemGold {
            get; set;
        }
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public async Task ChangeNameAsync(string name) =>
            await ParentClient.ChangeNameAsync(name);
        /// <summary>
        /// Changes the name of the user.
        /// </summary>
        /// <param name="name">New name</param>
        public void ChangeName(string name) =>
            ParentClient.ChangeName(name);
    }
}