using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Events;
using Guilded.Servers;
using RestSharp;

namespace Guilded.Client;

public abstract partial class AbstractGuildedClient
{
    #region Properties Servers
    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="GuildedBotClient">client bot</see> gets added to a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>BotServerMembershipCreated</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ServerRemoved" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<ServerAddedEvent> ServerAdded => ((IEventInfo<ServerAddedEvent>)GuildedEvents["BotServerMembershipCreated"]).Observable;

    /// <summary>
    /// Gets the <see cref="IObservable{T}">observable</see> for an event when the <see cref="GuildedBotClient">client bot</see> gets removed from a <see cref="Server">server</see>.
    /// </summary>
    /// <remarks>
    /// <para>An event with the name <c>BotServerMembershipDeleted</c> and opcode <c>0</c>.</para>
    /// </remarks>
    /// <seealso cref="ServerAdded" />
    /// <seealso cref="MemberJoined" />
    /// <seealso cref="MemberUpdated" />
    /// <seealso cref="RolesUpdated" />
    /// <seealso cref="MemberRemoved" />
    /// <seealso cref="MemberBanned" />
    /// <seealso cref="MemberUnbanned" />
    /// <seealso cref="WebhookCreated" />
    /// <seealso cref="WebhookUpdated" />
    public IObservable<ServerAddedEvent> ServerRemoved => ((IEventInfo<ServerAddedEvent>)GuildedEvents["BotServerMembershipDeleted"]).Observable;
    #endregion

    #region Methods Servers specifically
    /// <summary>
    /// Gets the specified <see cref="Server">server</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="Server">server</see> that was specified in the arguments</returns>
    public Task<Server> GetServerAsync(HashId server) =>
        GetResponsePropertyAsync<Server>(new RestRequest($"servers/{server}", Method.Get), "server");
    #endregion

    #region Methods Server subscriptions
    /// <summary>
    /// Gets a list of <see cref="SubscriptionTier">subscription tiers</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="SubscriptionTier">subscription tiers</see> from</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedResourceException" />
    /// <exception cref="GuildedRequestException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The list of fetched <see cref="SubscriptionTier">server subscription tiers</see> in the specified <paramref name="server" /></returns>
    public Task<IList<SubscriptionTier>> GetSubscriptionTiersAsync(HashId server) =>
        GetResponsePropertyAsync<IList<SubscriptionTier>>(new RestRequest($"servers/{server}/subscriptions/tiers", Method.Get), "serverSubscriptionTiers");

    /// <summary>
    /// Gets the specified <see cref="SubscriptionTier">server subscription tier</see>.
    /// </summary>
    /// <param name="server">The identifier of the <see cref="Server">server</see> to get <see cref="SubscriptionTier">subscription tier</see> from</param>
    /// <param name="type">The <see cref="SubscriptionType">subscription tier type</see> to get</param>
    /// <exception cref="GuildedException" />
    /// <exception cref="GuildedAuthorizationException" />
    /// <returns>The <see cref="SubscriptionTier">server subscription tier</see> that was specified in the arguments</returns>
    public Task<SubscriptionTier> GetSubscriptionTierAsync(HashId server, SubscriptionType type) =>
        GetResponsePropertyAsync<SubscriptionTier>(new RestRequest($"servers/{server}/subscriptions/tiers/{type}", Method.Get), "serverSubscriptionTier");
    #endregion
}