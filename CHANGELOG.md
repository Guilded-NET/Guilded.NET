# v0.7.5-beta

- Added `IsCompleted` property in `ListItem`
- Added `isSilent` parameter while creating messages
- Reformatted and reworked a lot of documentation
- Changed method parameter names (`messageId` -> `message`; `emoteId` -> `emote`)
- Commented out remove reaction method that can fool developers
- Added `TitledContent`, which is now a parent type of `Doc` and `ForumThread` types
- `UserSummary.ToString` will now return the mention of the user
- Added `IServerEvent` to the events that are supposed to have it
- Added command system
- Fix `message` argument in `UpdateListItemAsync` not being required (consistency with API)
- Fix `type` argument in `UserSummary` not being optional (consistency with API)

# v0.7.4-beta

- Fix `Prepared` being called constantly

# v0.7.2-beta

- Fix `Prepared` actually not being observable

# v0.7.1-beta

- Added `Me` and `IsPrepared` properties to AbstractGuildedClient
- Added `IsSystemMessage` to `Message` and `MessageEvent`
- Added `IsReply` to `MessageEvent`
- Added `Reconnected` to AbstractGuildedClient
- Changed `Prepared`, `Connected`, `Disconnected` to observables for consistency
- Changed `Prepared` to now be invoked after `Welcome`
- Changed `Disconnected`, `Prepared` and `Connected` returned arguments
    - `Disconnected` — `Websocket.Client.DisconnectionInfo`
    - `Connected` — `BaseGuildedClient`
    - `Prepared` — `Me`

# v0.7.0-beta

- Added `limit`, `before` and `after` parameters to get messages, docs and list items methods
- Added rest of list item CRUD methods (`UpdateListItemAsync`, `DeleteListItemAsync`, `GetListItemAsync`, `GetListItemsAsync`)
- Added member moderation commands (`KickMemberAsync`, `BanMemberAsync`, `UnbanMemberAsync`, `GetBanAsync`, `GetBansAsync`)
- Added member removal, member join, member (un)ban, webhook creation/updating events
- Added member, user, member ban and webhook models (`Member`, `MemberSummary`, `MemberBan`, `User`, `UserSummary`, `UserType`)
- Changed list item's note type to `ListItemNote` (from create, update, get list item methods) or `ListItemNoteSummary` (from get list item list method)
- Merged `MessageCreatedEvent` and `MessageUpdatedEvent` into `MessageEvent`:
    - `WebhookEvent` and `MemberBanEvent` are also merged
    - `MessageCreatedEvent` added nothing new and `MessageUpdatedEvent` only added non-null `UpdatedAt`
- Minor fixes