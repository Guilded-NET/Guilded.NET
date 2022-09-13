using System.Threading.Tasks;

using Guilded.Base;
using Guilded.Base.Users;

using RestSharp;

namespace Guilded.Abstract;

public abstract partial class AbstractGuildedClient
{
    #region Methods

    #region Methods Profile info
    /// <inheritdoc />
    public override Task<SocialLink> GetSocialLinkAsync(HashId server, HashId member, SocialLinkType linkType) =>
        GetResponseProperty<SocialLink>(new RestRequest($"servers/{server}/members/{member}/social-links/{linkType.ToString().ToLower()}", Method.Get), "socialLink");
    #endregion

    #endregion
}