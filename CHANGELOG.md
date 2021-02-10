## `ALPHA` 0.0.11.2

- Node generation fixes
- MarkType & Mark fixes
- Fixed .GetPermissionOf() errors

## `ALPHA` 0.0.11.1

- Fixed a bug where client's message would make Guilded.NET hang because of client ID missing
- Fixed converters for some unknown reason being static and protected. Now they are public.
- Fixed icons

## `ALPHA` 0.0.11

- Now you can give prefix as a string in GuildedClientConfig
- Connected is now called before `.Me` is assigned
- Added Prepared event when client is ready and `.Me` is assigned
- Removed `.CurrentUser`
- Added ability to reorder categories, channels
- Added ability to remove a role or add a role to a category or channel
- Added ability to give or remove a role to/from someone
- Added ability to remove or add a user to a channel or category
- Edit, delete forum posts, forum replies
- Create, edit, delete document, media, announcement and event replies
- Update announcement
- Create, delete, edit schedule availabilities
- Edit list items
- Channel settings
- Tons of fixes
- `Guilded.NET.API` has a new dependency: `Guilded.NET.Objects`