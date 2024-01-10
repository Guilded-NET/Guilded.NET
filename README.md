<div align="center">

[![Banner](https://raw.githubusercontent.com/Guilded-NET/Guilded.NET/early-access/assets/Banner.png)](https://github.com/Guilded-NET/Guilded.NET)

# 🟡 Guilded.NET

</div>

Guilded.NET is a free and open-source unofficial API framework/library for [Guilded](https://guilded.gg/) written on .NET platform. It allows creating bots, webhooks and interacting any other way with Guilded API.

[![Version](https://img.shields.io/badge/Version-1.7.1-blue?style=for-the-badge)](https://github.com/IdkGoodName/Guilded.NET) [![Version](https://img.shields.io/badge/Version-Release-blue?style=for-the-badge)](https://github.com/Guilded-NET/Guilded.NET)

## 📥 Installing

Guilded.NET is available as a package on [NuGet](https://www.nuget.org/packages/Guilded/) (or [FuGet](https://www.fuget.org/packages/Guilded/)).

You can run this command to add Guilded.NET to an existing .NET project:

```bash
dotnet add package Guilded
```

Otherwise, you can install Guilded.NET templates and create new Guilded.NET projects:

```bash
dotnet new -i Guilded.Templates
dotnet new guilded.bot
```

## ⚙️ Using Guilded.NET

You can check out [Guilded.NET's guide](https://guilded-net.github.io/docs) to get started on your bot. If you want to see everything that Guilded.NET offers, check out [reference page](https://guilded-net.github.io/references).

It is recommended to use .NET 6 or above for Guilded.NET. While Guilded.NET supports .NET 5 or similar for now, this will definitely change in the future.

## 📙 Example

Here's a quick example of a starter Guilded.NET bot with a `!ping` command:

```cs
// Program.cs
using System.Reactive.Linq;
using Guilded;

string auth   = "your_bots_auth_token",
       prefix = "!";

await using var client = new GuildedBotClient(auth);

client
    .Prepared
    .Subscribe(me =>
        Console.WriteLine("The bot is prepared!\nLogged in as \"{0}\" with the ID \"{1}\"", me.Name, me.Id)
    );

// Wait for !ping messages
client
    .MessageCreated
    .Where(msgCreated => msgCreated.Content == prefix + "ping")
    .Subscribe(async msgCreated =>
        await msgCreated.ReplyAsync("Pong!")
    );

await client.ConnectAsync();

// Don't close the program when the bot connects; not recommended to put code after this
await Task.Delay(-1);
```

> **Note**: The code above uses enabled implicit usings option.

## ⁉️ Support

If you need any help related to Guilded.NET, you can check out the following sources:

-   [Official Guilded.NET Server](https://guilded.gg/Guilded-NET)
-   [Programming Space](https://guilded.gg/programming)

## ✅ Goals

Our goal is to provide a library that is consistent and fast, while also maintaining ease of use. A library that does not bite a developer's hand allows them to focus more heavily on their code, have fun in what they are doing along, and have an easier time making bots. The consistency of code helps increase readability and collaboration.
