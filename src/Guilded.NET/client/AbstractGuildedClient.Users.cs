using System;
using System.Threading.Tasks;

using RestSharp;

namespace Guilded.NET
{
    using Base;
    using Base.Users;
    public abstract partial class AbstractGuildedClient
    {
        #region Profile info
        /// <inheritdoc/>
        public override async Task<SocialLink> GetSocialLinkAsync(GId userId, SocialLinkType linkType) =>
            await GetObject<SocialLink>($"users/{userId}/social-links/{linkType.ToString().ToLower()}", Method.GET, key: "socialLink").ConfigureAwait(false);
        #endregion
    }
}