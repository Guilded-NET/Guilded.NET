# v1.6.0

-   Added the ability to create threads through `client.CreateChannelAsync` or `message.CreateThreadAsync`. Thread creation in `ServerChannel`/`ChatChannel`/`VoiceChannel`/`StreamChannel` is still missing
-   Added announcements, announcement reactions, announcement comments and their events, as well as CRUD
-   Added user status CRUD and events
-   Added Groups CRUD and events
-   Added Role CRUD and events
-   Added the ability to get member's permissions
-   Added server subscription tiers and get server subscription tiers method
-   Added `Message.GroupId`
-   Added `IArchivableContent` that is now an interface for `Group` and `ServerChannel` objects
-   Added `GetServersAsync` to fetch bot's servers
-   Renamed `IWebhookCreatable` to `IWebhookCreated` for consistency with `IUserCreated`
-   Moved `IUserCreated`, `ICreatableContent`, `IUpdatableContent`, `IWebhookCreated` to `Guilded` namespace

# v1.5.1

-   Fix some serialization issues and channel creation specifically

# v1.5.0

## BREAKING

-   Changed name from `CalendarRsvp` to `CalendarEventRsvp`
-   Changed name from `CalendarRsvpStatus` to `CalendarEventRsvpStatus`
-   Changed name from `CalendarCancellation` to `CalendarEventCancellation`
-   Changed `MessageDeleted.ToString()` return value to `Deleted Message message-id-here`
-   Removed long deleted `XpAddedEvent` and `AbstractGuildedClient.XpAdded`. These will be re-added once Guilded API adds them back.

## Non-breaking

**Events:**

-   Added `CalendarEventComment` class
-   Added `CreateEventCommentAsync`, `UpdateEventCommentAsync`, `DeleteEventCommentAsync`, `AddEventCommentReaction`, `RemoveEventCommentReaction` methods for calendar event comments
-   Added `CalendarEventReaction`, `CalendarEventCommentReaction` reaction classes
-   Added `CalendarEventSeries` for repeating events.
-   Added `CalendarEventRepetition` and `CalendarEventInterval` classes, `CalendarEventRepetitionType` and `CalendarEventIntervalType` enums for setting calendar repetition
-   Added the ability to create, update and delete event series through `CreateEventAsync`, `UpdateEventSeriesAsync`, `DeleteEventSeriesAsync`.

-   Added `EventSeriesUpdated`, `EventSeriesDeleted` events for calendar event repetition changes
-   Added `EventReactionAdded`, `EventReactionRemoved` events for calendar event reactions
-   Added `EventCommentCreated`, `EventCommentUpdated`, `EventCommentDeleted`, `EventCommentReactionAdded`, `EventCommentReactionRemoved` events for calendar event comments
-   Added `UpdateEventSeriesAsync` and `DeleteEventSeriesAsync` to `EventChannel` and `CalendarEvent`

**Docs:**

-   Added `DocReactionAdded`, `DocReactionRemoved` events for doc reactions
-   Added `DocCommentCreated`, `DocCommentUpdated`, `DocCommentDeleted`, `DocCommentReactionAdded`, `DocCommentReactionRemoved` events for doc comments
-   Added `DocComment` class for document comments
-   Added `DocReaction`, `DocCommentReaction` classes for document reactions
-   Added `Id` getter to `DocEvent`

**Other stuff:**

-   Added `UserId` to `SocialLink`
-   Added `CreatedAt` to `SocialLink`
-   Added `MemberSocialLinkCreated`, `MemberSocialLinkUpdated`, `MemberSocialLinkDeleted` events for `SocialLink` changes
-   Changed `client.UpdateItemAsync`'s `message` argument to be nullable.
-   Changed lots of documentation

# v1.4.0

-   Added `UserReference` and its single enum field `Me`
-   Added support for `UserReference` in most areas where user ID is required (except bans)

# v1.3.0

-   Added support for topic comment reactions
-   Fixed topic comment bug where it used wrong API event name

# v1.2.0

-   Added `ArgumentConverters` to `CommandConfiguration` that exposes command argument converters that were previously internal. This will allow you to add new argument converters for types, as well as modify them (in cases where you want there to be more than `true` and `false` boolean values, for instance)
-   Added `ChannelId` to `TopicComment` and `TopicCommentEvent`
-   Added `Updated`, `Deleted` to `TopicComment` and `TopicCommentEvent`
-   Added `CommentCreated`, `CommentUpdated`, `CommentDeleted` to `Topic` and `TopicEvent`
-   Fixed `TopicReactionEvent` using `MessageReaction` instead of `TopicReaction`

# v1.1.5

-   Added `Uri` as a type of a command argument
-   Added the ability to have multiple prefixes
-   Fixed `teamRolesUpdated` not being replaced

# v1.1.4

-   Use the new `Server...` event naming scheme instead of `Team...`.

# v1.1.3

-   Added `BaseGuildedConnection.DisconnectedWithError` observable
-   Fixed deserialization errors not being caught and crashing WS events

# v1.1.2

-   Made `CalendarCancellation.Description` optional
-   Made `ServerChannel` non-abstract for the time being because of Newtonsoft screaming about absolutely nothing (will fix later)

# v1.1.1 (`Guilded.Commands` only)

-   Fixed where command names that end with `Command` and `Async` would cut down to the length of `Command` of `Async` strings

# v1.1.0

Important renames:

-   `ListItemBase<T>`, `ListItemSummary`, `ListItem`, `ListItemNoteSummary`, `ListItemNote`, `ListItemEvent` were renamed to `ItemBase<T>`, `ItemSummary`, `Item`, `ItemNoteSummary`, `ItemNote`, `ItemEvent` respectively to be more consistent with `Topic`, `TopicComment`, `Doc`, `Message` and future types like `Media`, `MediaComment`, `Schedule`, as well as to have `ListItemNoteSummary` shorter.

Package changes:

-   `Guilded.Base` is now very barebones for Guilded REST stuff. Doesn't contain things like `client.CreateMessageAsync` or anything. Still contains embeds and `MessageContent`
-   `Guilded.Connection`. This has `Guilded.Base` as a dependency and builds upon it for WebSocket stuff. Only `Guilded` package uses it, but this is primarily made for unofficial Guilded libraries.

Actual changes:

-   Added `client.Id`, `client.BotId`, `client.CreatedBy`, `client.CreatedAt`, `client.Name` that get the named properties from `client.Me`
-   Added `message.Replied`, `message.Updated`, `topic.Updated`, `topicEvent.Updated`, `user.MemberUpdated`, `member.Updated` and similar events
-   Added `Guilded.GuildedObservableExtensions` that add `observable.ElapseOn(TimeSpan)`, `observable.ElapseOnIfNothing(TimeSpan)`, `guildedObservable.InServer(HashId)`, `guildedObservable.InChannel(Guid)`, `guildedObservable.CreatedBy(HashId)`, etc.
-   Added `TopicComment`, `CreateTopicCommentAsync`, etc.
-   Added `CreateCommentAsync`, `UpdateCommentAsync`, etc. to `Topic`
-   Added `LockTopicAsync`, `UnlockTopicAsync` to `ForumChannel`
-   Renamed `BaseGuildedClient` to `BaseGuildedConnection`, made it for webhook stuff only
-   Renamed `GuildedWebsocketException` to `GuildedSocketException` for consistency with `GuildedSocketMessage`
-   Renamed `RolesUpdatedEvent.RolesUpdated` class to `RolessUpdatedEvent.UserRoleUpdate`
-   Renamed tons of things like `UserId`, `EmoteId`, etc. etc. in models like `RolesUpdatedEvent.UserRoleUpdate` to just `Id` to use `IModelHasId<T>` interface (this adds additional extensions to things like observables and `IList<T>`).
-   Moved most of the models from `Guilded.Base` package to `Guilded` package. `Guilded.Base` now only contains exception types, `HashId`, `FormId`, embed stuff, `BaseGuildedService`, `GuildedUrl`, `IWebhook`, `MessageContent` and some JSON converters.
-   Moved `GuildedSocketException`, `GuildedSocketMessage`, `BaseGuildedConnection` and `SocketOpcode` to `Guilded.Connection` package

Template changes:

-   Added VB.NET and F# templates for Webhook client
-   Added another example command to commands bot

# v1.0.1-v1.0.2

-   Fixed channel conversion (v1.0.1)
-   Fixed `null` being passed to arguments despite
-   Removed overlooked channel conversion `Console.WriteLine` (v1.0.2)

# v1.0.0

-   Added webhook masquerading
-   Added `ChatChannel`, `ForumChannel`, `MediaChannel`, etc.
-   Added `CommandRestAttribute` for string command arguments with spaces included
-   Added `CommandRegexAttribute` for `MatchCollection` and `Match` arguments
-   Added `object? additionalContext` argument to `DoCommandsAsync(MessageEvent, string, CommandConfig)`. This allows you to pass anything you need to pass down to commands or something you don't want to re-fetch.
-   Added `Guilded.Markdown` package. This includes:
    -   `MarkdownUtil` static class with `InlineCode`, `CodeBlock`, `Bold`, etc. methods
    -   `MarkdownBuilder` static class with `StringBuilder` extension methods like `AppendInlineCode`, `AppendBold`
    -   `GuildedMarkdownExtensions` static class with extension methods like `message.AddReactionAsync('...')` and `message.AddReactionAsync("smile_guilded")`
    -   `Emotes` static class with `ByName` and `BySymbol` dictionaries of emotes
-   Added `AddTopicPinAsync`, `RemoveTopicPinAsync`
-   Added `GuildedListExtensions` with extension methods for `IList<T>` for Guilded objects. These include:
    -   `IList<TModel>.ById(TId)` (e.g., `memberList.ById(new HashId("..."))`)
    -   `IList<TModel>.ByIdOrDefault(TId)` (e.g., `messageList.ByIdOrDefault(new Guid("..."))`)
    -   `IList<TModel>.Contains(TId)` (e.g., `topicList.Contains(12345678)`)
-   Added `DoCommandsAsync(MessageEvent, CommandConfig)` that will now be invoked by the client instead. This will still invoke previous `DoCommandsAsync`
-   Renamed `CreateListItemAsync`, `UpdateListItemAsync`, `CompleteListItemAsync`, etc. to `CreateItemAsync`, `UpdateItemAsync`, `CompleteItemAsync`, etc.
-   Changed to new WebSocket URL
-   Moved `AbstractGuildedClient` to `Guilded.Abstract` namespace
-   Moved `BaseGuildedClient` to `Guilded.Base.Client` namespace
-   Fixed docs (to make more sense/a bit more consistent with .NET, especially `<returns>`)
-   Removed `BaseModel` and all types no longer have it

# v0.10.0-beta

-   Added `Banner` and `CreatedAt` to `Member` type (not `User`, which already had them)
-   Added `AddReactionAsync(..., Emote emote)` and `RemoveReactionAsync(..., Emote emote)`
-   Added `Mentions` to `TopicEvent`
-   Added `Webhook.CreateUrl(webhookId, token)`
-   Added `ServerId` to `Member` and `MemberSummary` (this gets set in the behind the scenes of Guilded.NET, so bugs may occur)
-   Added `RemoveAsync`, `AddBanAsync`, `GetSocialLinkAsync`, etc. to `Member` and `MemberSummary`
-   Moved `GuildedWebhookClient` and `WebhookSkeleton` to `Guilded.Webhook` package
-   Fixed client hanging on reconnect

# v0.9.3-beta

Important:

-   Renamed `AddRoleAsync` and `RemoveRoleAsync` to `AddMemberRoleAsync` and `RemoveMemberRoleAsync` respectively

Other changes:

-   Added `Url` and `IsExecutable` properties to `Webhook` model
-   Added `WebhookSkeleton`, `BaseGuildedService` and `GuildedWebhookClient` (extends `BaseGuildedService`)
-   Fixed `ParentClient` in `CommandEvent` always being null
-   Fixed `InvokeCommandAsync` in `CommandParent` not being used at all (now it is used for command invokation, useful for permission stuff)
-   Moved REST stuff to `BaseGuildedService`
-   Made `BaseGuildedClient` extend `BaseGuildedService`

# v0.9.2-beta

-   Added `Mentions` property to `Topic` class
-   Added `ParentClient` to `MessageEvent` (for `CommandEvent`)
-   Added different `CommandBase` class that extends older `CommandBase` (`CommandParent`), now with `InstanceInfo`, `Name`, `Description`, `Examples` and `Aliases` properties
-   Renamed `CommandBase` to `CommandParent` (`CommandBase` still exists)

# v0.9.1-beta

-   Added forum CRUD and events
-   Renamed `ReactionAdded`/`ReactionRemoved` to `MessageReactionAdded`/`MessageReactionRemoved`
-   Fixed update calendar event method
-   Fixed error related to `Websocket.NativeClient`

# v0.9.0-beta

Important:

-   !!! Bumped framework version from .NET Standard 2.1 to .NET 5
-   Removed `IDisposable` from the client (that means you now have to do `await using var client = ...;` instead)

Other stuff:

-   Added rate-limiting handling (rejects)
-   Added automatic resuming
-   Added `ResponseReceived` observable
-   Added `ToString` to `GuildedSocketMessage`
-   Added Calendar event RSVPs
-   Added `IChannelBased` and `IGlobalContent` interfaces
-   Added `client.GetServerAsync(HashId)`, because I forgot to do that for some reason
-   Renamed `IServerEvent` to `IServerBased`
-   Changed `LastMessageId`'s access from `public` to `protected`
-   Changed `WebsocketMessage`'s access from `protected` to `public`
-   Removed `IServerEvent` from all events
-   Removed `IDisposable` from the client

# v0.8.6-beta

-   Added support for static methods to be commands too
-   Added `CommandConfiguration`
-   Added new `AddCommands` arguments, which either require `CommandConfiguration` or its constructor's arguments
-   Added `Emote` model
-   Added `ReactionAdded` and `ReactionRemoved` event
-   Fixed having the ability to declare abstract types and abstract methods as commands
-   Fixed not being able to use multiple `Example` attributes
-   Removed `Prefix`, `Separators` and `SplitOptions` in `CommandModule`
-   Removed required constructors in `CommandModule`

# v0.8.5-beta

-   Added calendar event support
-   Renamed `KickMemberAsync` and `member.KickAsync` to `RemoveMemberAsync`
-   Renamed `BanMemberAsync` and `member.BanAsync` to `AddMemberBanAsync`
-   Renamed `UnbanMemberAsync` and `member.UnbanAsync` to `RemoveMemberBanAsync`
-   Fixed mentions always being `null` in messages

# v0.8.4-beta

-   Added `DescriptionAttribute`, `ExampleAttribute` attributes for commands
-   Added `UsageAreaAttribute` attribute for commands, but it isn't used anywhere yet
-   Added more `EmbedField`, `EmbedAuthor` and `EmbedFooter` constructors
-   Added `Mentions` property to `ListItemNote`
-   Added support for nullable command arguments
-   Renamed `ForumThread` and `CreateForumThreadAsync` to `Topic` and `CreateTopicAsync`
-   Fixed bugs related to commands
-   Fixed embeds not being sent if author does not contain an icon and URL
-   Removed `CommandAttribute.Description` and `CommandAttribute.Examples` properties

# v0.8.3-beta (Guilded.Commands package)

Minor bug fix

# v0.8.2-beta

-   Fixed commands not being able to have `uint`, `ulong` or `ushort` types as their arguments
-   Fixed arguments of `char` type being parsed as `decimal` type instead
-   Made `BaseGuildedClient`, `AbstractGuildedClient` and `GuildedBotClient` implement `IAsyncDisposable`
    -   It is recommended to use `await using var client = ...;`, but `using var client = ...;` is still suppported

# v0.8.1-beta

-   Added `CommandLookup` property in `CommandBase`
-   Added `Description` and `Examples` getters in `ICommandInfo<T>`
-   Added `Mentions` property to `Message`, `ListItemSummary` and `Doc` types
-   Added `IModelHasId<T>`
-   Added `CommandBase.InvokeCommandAsync` virtual methods for overriding
-   Added `char` and `TimeSpan` types to the command argument type list
-   Fixed event property's documentation
-   Renamed `BaseObject` to `BaseModel` and `ClientObject` to `ContentModel`
-   Renamed `RootCommandContext` to `RootCommandEvent` for consistency
-   Renamed `FailedCommandEvent.Type` to `FailedCommandEvent.FailType` (since `MessageEvent.Type` is now inherited)
-   Renamed `CommandBase.InvokeAnyCommandAsync` to `CommandBase.InvokeCommandByNameAsync`
-   Made `CommandEvent` extend `MessageEvent`
    -   **NOTE:** `FailedCommandEvent` extends `CommandEvent`, so it appplies to `FailedCommandEvent` as well
-   Removed `CommandEvent.MessageEvent`

# v0.8.0-beta

-   Added the ability to execute webhooks using URLs (not only using tokens and webhook IDs independently)
-   Added `IsOwner` property to `Member`
-   Added command system
-   Added `IsSilent` to `Message` type
-   Added `IsPrivate` to `MessageDeletedEvent.MessageDeleted` and `MessageDeletedEvent` types
-   Added `IsPrivate`, `IsSilent` `ReplyMessageIds` and `Embeds` properties to `MessageEvent` type

# v0.7.6-beta

-   Added channels and channel events
-   Added docs events
-   Added list item events
-   Added see also to a lot of documented items and added a lot of see references in documentation
-   Added comparisons between `HashId` and `string`
-   Fixed requiring `embeds:` in message creation for embeds
-   Fixed `includePrivate` error in `GetMessageAsync`
-   Renamed `ListItem<T>` to `ListItemBase<T>` and added `ListItem` and `ListItemSummary`
-   Removed extra unused properties and arguments in embeds

# v0.7.5-beta

-   Added `IsCompleted` property in `ListItem`
-   Added `isSilent` parameter while creating messages
-   Added `Message.EmbedLimit` and `Message.ReplyLimit`
-   Added creating messages with embeds support
-   Added `Message.Embeds`
-   Added `TitledContent`, which is now a parent type of `Doc` and `ForumThread` types
-   Added `IServerEvent` to the events that are supposed to have it
-   Changed method parameter names (`messageId` -> `message`; `emoteId` -> `emote`)
-   Changed `UserSummary.ToString` return value to the mention of the user
-   Reformatted and reworked a lot of documentation
-   Renamed `Message.ContentLimit` to `Message.TextLimit`
-   Fix `message` argument in `UpdateListItemAsync` not being required (consistency with API)
-   Fix `type` argument in `UserSummary` not being optional (consistency with API)
-   Fix `iconUrl` and `url` arguments being switched in `EmbedAuthor`
-   Commented out remove reaction method that can fool developers

# v0.7.4-beta

-   Fix `Prepared` being called constantly

# v0.7.2-beta

-   Fix `Prepared` actually not being observable

# v0.7.1-beta

-   Added `Me` and `IsPrepared` properties to AbstractGuildedClient
-   Added `IsSystemMessage` to `Message` and `MessageEvent`
-   Added `IsReply` to `MessageEvent`
-   Added `Reconnected` to AbstractGuildedClient
-   Changed `Prepared`, `Connected`, `Disconnected` to observables for consistency
-   Changed `Prepared` to now be invoked after `Welcome`
-   Changed `Disconnected`, `Prepared` and `Connected` returned arguments
    -   `Disconnected` — `Websocket.Client.DisconnectionInfo`
    -   `Connected` — `BaseGuildedClient`
    -   `Prepared` — `Me`

# v0.7.0-beta

-   Added `limit`, `before` and `after` parameters to get messages, docs and list items methods
-   Added rest of list item CRUD methods (`UpdateListItemAsync`, `DeleteListItemAsync`, `GetListItemAsync`, `GetListItemsAsync`)
-   Added member moderation commands (`KickMemberAsync`, `BanMemberAsync`, `UnbanMemberAsync`, `GetBanAsync`, `GetBansAsync`)
-   Added member removal, member join, member (un)ban, webhook creation/updating events
-   Added member, user, member ban and webhook models (`Member`, `MemberSummary`, `MemberBan`, `User`, `UserSummary`, `UserType`)
-   Changed list item's note type to `ListItemNote` (from create, update, get list item methods) or `ListItemNoteSummary` (from get list item list method)
-   Merged `MessageCreatedEvent` and `MessageUpdatedEvent` into `MessageEvent`:
    -   `WebhookEvent` and `MemberBanEvent` are also merged
    -   `MessageCreatedEvent` added nothing new and `MessageUpdatedEvent` only added non-null `UpdatedAt`
-   Minor fixes
