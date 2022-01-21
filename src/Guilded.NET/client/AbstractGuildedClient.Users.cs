using System.Threading.Tasks;
using Guilded.NET.Base;
using Guilded.NET.Base.Users;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Guilded.NET
{
    public abstract partial class AbstractGuildedClient
    {
        #region Profile info
        /// <inheritdoc/>
        public override async Task<SocialLink> GetSocialLinkAsync(HashId serverId, HashId memberId, SocialLinkType linkType) =>
            await GetObject<SocialLink>(new RestRequest($"servers/{serverId}/members/{memberId}/social-links/{linkType.ToString().ToLower()}", Method.GET), "socialLink").ConfigureAwait(false);
        #endregion
    }
}