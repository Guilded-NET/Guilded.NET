using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Guilded.Base;
using Guilded.Client;
using Guilded.Commands.Items;
using Guilded.Content;
using Guilded.Events;
using Guilded.Servers;

namespace Guilded.Commands.Utils;

internal static class CommandArgumentUtil
{
    public static int GetArgumentOfTypeCount(CommandArgument[] arguments, Type type) =>
        arguments.Count(x => x.ArgumentType == type);

    public static (int member, int role, int channel) GetMaxNumbersOfFetchableArguments(IEnumerable<ICommand<MemberInfo>> anyCommands)
    {
        IEnumerable<Command> commands = anyCommands.Where((x) => x is Command).Select((x) => (Command)x);

        if (!commands.Any())
            return (0, 0, 0);

        return (commands.Max((x) => x.MemberArgumentCount), commands.Max((x) => x.RoleArgumentCount), commands.Max((x) => x.ChannelArgumentCount));
    }

    public static async Task<IEnumerable<TItem>> FetchMoreItemsAsync<TItemId, TItem, TItemMention>(AbstractGuildedClient client, MessageEvent messageEvent, IEnumerable<TItem> known, int neededCount, Func<Mentions?, IList<TItemMention>?> getMentions, Func<AbstractGuildedClient, HashId, TItemId, Task<TItem>> getItem)
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

    public static Task<IEnumerable<Member>> FetchMoreMembersAsync(AbstractGuildedClient client, MessageEvent messageEvent, IEnumerable<Member> known, int neededCount) =>
        FetchMoreItemsAsync<HashId, Member, Mentions.UserMention>(
            client,
            messageEvent,
            known,
            neededCount,
            static mentions => mentions?.Users,
            static (client, serverId, memberId) => client.GetMemberAsync(serverId, memberId)
        );

    public static Task<IEnumerable<Role>> FetchMoreRolesAsync(AbstractGuildedClient client, MessageEvent messageEvent, IEnumerable<Role> known, int neededCount) =>
        FetchMoreItemsAsync<uint, Role, Mentions.RoleMention>(
            client,
            messageEvent,
            known,
            neededCount,
            static mentions => mentions?.Roles,
            static (client, serverId, roleId) => client.GetRoleAsync(serverId, roleId)
        );

    public static Task<IEnumerable<ServerChannel>> FetchMoreChannelsAsync(AbstractGuildedClient client, MessageEvent messageEvent, IEnumerable<ServerChannel> known, int neededCount) =>
        FetchMoreItemsAsync<Guid, ServerChannel, Mentions.ChannelMention>(
            client,
            messageEvent,
            known,
            neededCount,
            static mentions => mentions?.Channels,
            static (client, _serverId, channelId) => client.GetChannelAsync(channelId)
        );
}