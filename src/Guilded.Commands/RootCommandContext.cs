using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Content;
using Guilded.Events;
using Guilded.Servers;

namespace Guilded.Commands;

/// <summary>
/// Represents the information about the root/original command.
/// </summary>
/// <example>
/// <para>Let's say we have this command structure:</para>
/// <code>
/// - `config` command
///     - `items` command
///         - `add` command with arguments (string name)
/// </code>
/// <para>Even if we invoke <q>config items add</q> command, the root command will always remain <q>config</q>.</para>
/// </example>
/// <seealso cref="CommandEvent" />
/// <seealso cref="FailedCommandEvent" />
public class RootCommandEvent : IHasParentClient
{
    #region Properties
    /// <summary>
    /// Gets the prefix that was fetched for the command.
    /// </summary>
    /// <value>Command configuration used in the command</value>
    public CommandConfiguration Configuration { get; }

    /// <summary>
    /// Gets the prefix that was fetched for the command.
    /// </summary>
    /// <value>Prefix used in the command</value>
    public string Prefix { get; }

    /// <summary>
    /// Gets the name of the top-most/first-most command that was used.
    /// </summary>
    /// <value>The name of the original command used</value>
    public string CommandName { get; }

    /// <summary>
    /// Gets the given arguments to the top-most/first-most command.
    /// </summary>
    /// <value>The arguments of the original command used</value>
    public string Arguments { get; }

    /// <summary>
    /// Gets the message event that invoked the command.
    /// </summary>
    /// <value>The message event that invoked the command</value>
    public MessageEvent MessageEvent { get; }

    /// <summary>
    /// Gets any additional context that were passed manually by the bot developer (you).
    /// </summary>
    /// <remarks>
    /// <para>By default, the value will be <see langword="null" />.</para>
    /// </remarks>
    /// <value>Any additional context that were passed manually by the bot developer (you)</value>
    public object? AdditionalContext { get; }

    /// <summary>
    /// Gets all the fetched <see cref="Member">members</see> from the <see cref="Message.Mentions">message's mentions</see> at a given time.
    /// </summary>
    /// <value>All the fetched <see cref="Member">members</see> from the <see cref="Message.Mentions">message's mentions</see></value>
    public IEnumerable<Member> KnownMembers { get; set; }

    /// <summary>
    /// Gets all the fetched <see cref="Role">roles</see> from the <see cref="Message.Mentions">message's mentions</see> at a given time.
    /// </summary>
    /// <value>All the fetched <see cref="Role">roles</see> from the <see cref="Message.Mentions">message's mentions</see></value>
    public IEnumerable<Role> KnownRoles { get; set; }

    /// <summary>
    /// Gets all the fetched <see cref="ServerChannel">channels</see> from the <see cref="Message.Mentions">message's mentions</see> at a given time.
    /// </summary>
    /// <value>All the fetched <see cref="ServerChannel">channels</see> from the <see cref="Message.Mentions">message's mentions</see></value>
    public IEnumerable<ServerChannel> KnownChannels { get; set; }
    #endregion

    #region Properties Getters
    /// <inheritdoc />
    public AbstractGuildedClient ParentClient => MessageEvent.ParentClient;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="RootCommandEvent" />.
    /// </summary>
    /// <param name="messageEvent">The message event that invoked the command</param>
    /// <param name="config">The configuration of the command system used</param>
    /// <param name="prefix">The prefix that was fetched for the command</param>
    /// <param name="commandName">The name of the original command that was used</param>
    /// <param name="arguments">The given arguments to the original command</param>
    /// <param name="additionalContext">The additional context that were passed</param>
    public RootCommandEvent(MessageEvent messageEvent, CommandConfiguration config, string prefix, string commandName, string arguments, object? additionalContext) =>
        (MessageEvent, Configuration, Prefix, CommandName, Arguments, KnownMembers, KnownRoles, KnownChannels, AdditionalContext) = (messageEvent, config, prefix, commandName, arguments, Enumerable.Empty<Member>(), Enumerable.Empty<Role>(), Enumerable.Empty<ServerChannel>(), additionalContext);
    #endregion

    #region Methods

    private static async Task<IEnumerable<TItem>> FetchMoreItemsAsync<TItemId, TItem, TItemMention>(AbstractGuildedClient client, MessageEvent messageEvent, IEnumerable<TItem> known, int neededCount, Func<Mentions?, IList<TItemMention>?> getMentions, Func<AbstractGuildedClient, HashId, TItemId, Task<TItem>> getItem)
        where TItemId : notnull
        where TItem : IModelHasId<TItemId>
        where TItemMention : IModelHasId<TItemId>
    {
        int knownCount = known.Count();

        // Nothing to fetch; continue
        if (neededCount == knownCount)
            return known;

        // How many more will needed to be fetched
        int fetchCount = neededCount - knownCount;
        IList<TItemMention>? mentions = getMentions(messageEvent.Mentions);

        if (mentions is null)
            return known;

        HashId serverId = (HashId)messageEvent.ServerId!;

        // Fetch additional ones
        IEnumerable<TItem> additionalItems = await Task.WhenAll(
            mentions
                .Skip(knownCount)
                .Take(fetchCount)
                .Select(x => getItem(client, serverId, x.Id))
        );

        return known.Concat(additionalItems);
    }

    internal Task FetchRequiredArgumentsAsync((int members, int roles, int channels) needed) =>
        Task.WhenAll(FetchMoreMembersAsync(needed.members), FetchMoreRolesAsync(needed.roles), FetchMoreChannelsAsync(needed.channels));

    internal async Task FetchMoreMembersAsync(int neededCount) =>
        KnownMembers = await FetchMoreItemsAsync<HashId, Member, Mentions.UserMention>(
            ParentClient,
            MessageEvent,
            KnownMembers,
            neededCount,
            static mentions => mentions?.Users,
            static (client, serverId, memberId) => client.GetMemberAsync(serverId, memberId)
        );

    internal async Task FetchMoreRolesAsync(int neededCount) =>
        KnownRoles = await FetchMoreItemsAsync<uint, Role, Mentions.RoleMention>(
            ParentClient,
            MessageEvent,
            KnownRoles,
            neededCount,
            static mentions => mentions?.Roles,
            static (client, serverId, roleId) => client.GetRoleAsync(serverId, roleId)
        );

    internal async Task FetchMoreChannelsAsync(int neededCount) =>
        KnownChannels = await FetchMoreItemsAsync<Guid, ServerChannel, Mentions.ChannelMention>(
            ParentClient,
            MessageEvent,
            KnownChannels,
            neededCount,
            static mentions => mentions?.Channels,
            static (client, _serverId, channelId) => client.GetChannelAsync(channelId)
        );
    #endregion
}