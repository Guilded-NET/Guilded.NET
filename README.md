[![Banner](https://raw.githubusercontent.com/Guilded-NET/Guilded.NET/early-access/assets/Banner.png)](https://github.com/Guilded-NET/Guilded.NET)

# Guilded.NET

[![Version](https://img.shields.io/badge/Version-0.5.0-red?style=for-the-badge)](https://github.com/IdkGoodName/Guilded.NET) [![Version](https://img.shields.io/badge/Version-Beta-orange?style=for-the-badge)](https://github.com/Guilded-NET/Guilded.NET)

Guilded.NET is an open-source unofficial API wrapper for [Guilded](https://guilded.gg/) written on .NET platform. It tries to integrate as many Guilded API features as possible, while also maintaining the usability and ease of use for the developers.

- To get started with **Guilded.NET**, check out [documentation page](https://guilded-net.github.io/docs).
- To see all **Guilded.NET** references, check out [reference page](https://guilded-net.github.io/references).

## Example

An example of a bot that only responds to ping commands in C# 10:

```json
// config/config.js
{
    "auth": "your_auth_token_here",
    "prefix": "!"
}
```
```cs
// Program.cs
using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded;
using Newtonsoft.Json.Linq;

// Get the configuration values
JObject config = JObject.Parse(await File.ReadAllTextAsync("./config/config.json").ConfigureAwait(false));

string? auth   = config.Value<string>("auth"),
        prefix = config.Value<string>("prefix");

using GuildedBotClient client = new(auth);

client.Connected += (_, _) => Console.WriteLine("Connected");
client.Prepared += (_, _) => Console.WriteLine("Prepared");

// Wait for !ping messages
client.MessageCreated
    .Where(msgCreated => msgCreated.Content == prefix + "ping")
    .Subscribe(async msgCreated => await msgCreated.ReplyAsync("Pong!").ConfigureAwait(false));

await client.ConnectAsync().ConfigureAwait(false);
// Don't close the program when the bot connects
await Task.Delay(-1).ConfigureAwait(false);
```

## Links
- [Website](https://guilded-net.github.io/)
- [NuGet](https://www.nuget.org/packages/Guilded.NET/)
- [FuGet](https://www.fuget.org/packages/Guilded.NET/)

## Libraries
- **NewtonSoft.Json** - Used as a library to (de)serialize Guilded.NET models.
- **RestSharp** - Used for REST clients
- **Websocket.Client** - Used for WebSocket clients
- **DefaultDocumentation** - Generates documentation/references from .NET XML documentation

## Maintainers
- [**IdkGoodName**](https://guilded.gg/profile/R40Mp0Wd) - leading maintainer

## See also
- **[Guilded.NET Discussions Group](https://www.guilded.gg/guilded-api/groups/aDk5j9Jz/channels/8c247143-2009-415b-ab99-97912c0685bc/announcements)**
- **[Programming Space](https://guilded.gg/Programming)**