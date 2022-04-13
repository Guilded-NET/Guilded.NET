[![Banner](https://raw.githubusercontent.com/Guilded-NET/Guilded.NET/early-access/assets/Banner.png)](https://github.com/Guilded-NET/Guilded.NET)

# Guilded.NET

[![Version](https://img.shields.io/badge/Version-0.7.1-red?style=for-the-badge)](https://github.com/IdkGoodName/Guilded.NET) [![Version](https://img.shields.io/badge/Version-Beta-orange?style=for-the-badge)](https://github.com/Guilded-NET/Guilded.NET)

Guilded.NET is a free and open-source unofficial API wrapper for [Guilded](https://guilded.gg/) written on .NET platform. It allows creating bots, webhooks and interacting any other way with Guilded API.

- To get started with **Guilded.NET**, check out [documentation page](https://guilded-net.github.io/docs).
- To see all **Guilded.NET** API references, check out [reference page](https://guilded-net.github.io/references).

## Installing

To add Guilded.NET to your existing project:

```bash
dotnet add package Guilded
```

To create a new Guilded.NET project:

```bash
dotnet new -i Guilded.Templates
dotnet new guilded.bot
```

## Example

Here's an example of a minimal bot with a "ping" command in C# 10:

```cs
// Program.cs
using System.Reactive.Linq;
using Guilded;

string auth   = "your_bots_auth_token",
       prefix = "!";

using GuildedBotClient client = new(auth);

client.Prepared += (_, _) => Console.WriteLine("The bot is prepared!");

// Wait for !ping messages
client.MessageCreated
    .Where(msgCreated => msgCreated.Content == prefix + "ping")
    .Subscribe(async msgCreated =>
        await msgCreated.ReplyAsync("Pong!")
    );

await client.ConnectAsync();

// Don't close the program when the bot connects
await Task.Delay(-1);
```
(Implicit usings option is enabled)

## Goals

Our goal is to provide a library that is consistent and fast, while also maintaining friendliness towards the bot developers. API library that does not bite bot developer's hand allows bot developers to focus more on their code, have fun in what they are doing and have easier time creating their bots. Consistency helps code be more predictable, easier to rewrite and waste less time. As such, these 3 points are our main goals while maintaining Guilded.NET.

## Other information

### Links
- [Website](https://guilded-net.github.io/)
- [NuGet](https://www.nuget.org/packages/Guilded/)
- [FuGet](https://www.fuget.org/packages/Guilded/)

### Maintainers
- **[IdkGoodName](https://guilded.gg/profile/R40Mp0Wd)** - leading maintainer

### Libraries
- **NewtonSoft.Json** - Used as a library to (de)serialize Guilded.NET models.
- **RestSharp** - Used for REST clients
- **Websocket.Client** - Used for WebSocket clients
- **DefaultDocumentation** - Generates documentation/references from .NET XML documentation

## See also
- **[Guilded.NET Discussions Group](https://www.guilded.gg/guilded-api/groups/aDk5j9Jz/channels/8c247143-2009-415b-ab99-97912c0685bc/announcements)**
- **[Programming Space](https://guilded.gg/programming)**