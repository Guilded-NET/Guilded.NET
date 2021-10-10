using System.Threading.Tasks;
using Guilded.NET.Base;
using Guilded.NET.Base.Users;
using RestSharp;

namespace Guilded.NET
{
    public abstract partial class AbstractGuildedClient
    {
        #region Profile info
        /// <inheritdoc/>
        public override async Task<SocialLink> GetSocialLinkAsync(GId userId, SocialLinkType linkType) =>
            await GetObject<SocialLink>(new RestRequest($"users/{userId}/social-links/{linkType.ToString().ToLower()}", Method.GET), "socialLink").ConfigureAwait(false);
        #endregion
    }
}