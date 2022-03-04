using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Base.Users;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded
{
    public abstract partial class AbstractGuildedClient
    {
        #region Profile info
        /// <inheritdoc/>
        public override async Task<SocialLink> GetSocialLinkAsync(HashId serverId, HashId memberId, SocialLinkType linkType) =>
            await GetResponseProperty<SocialLink>(new RestRequest($"servers/{serverId}/members/{memberId}/social-links/{linkType.ToString().ToLower()}", Method.Get), "socialLink").ConfigureAwait(false);
        #endregion
    }
}