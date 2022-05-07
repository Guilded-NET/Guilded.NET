using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Users;

using RestSharp;

namespace Guilded;

public abstract partial class AbstractGuildedClient
{
    #region Profile info
    /// <inheritdoc />
    public override async Task<SocialLink> GetSocialLinkAsync(HashId server, HashId member, SocialLinkType linkType) =>
        await GetResponseProperty<SocialLink>(new RestRequest($"servers/{server}/members/{member}/social-links/{linkType.ToString().ToLower()}", Method.Get), "socialLink").ConfigureAwait(false);
    #endregion
}