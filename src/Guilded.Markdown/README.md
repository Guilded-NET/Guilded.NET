<div align="center">

[![Banner](https://raw.githubusercontent.com/Guilded-NET/Guilded.NET/early-access/assets/Banner.png)](https://github.com/Guilded-NET/Guilded.NET)

# 🟡 Guilded.NET Webhooks
</div>

Guilded.NET is a free and open-source unofficial API framework/library for [Guilded](https://guilded.gg/) written on .NET platform. It allows creating bots, webhooks and interacting any other way with Guilded API.

This particular package was made for Guilded Webhooks. Everything that is necessary for interacting and executing webhooks should be here.

[![Version](https://img.shields.io/badge/Version-0.10.0-red?style=for-the-badge)](https://github.com/IdkGoodName/Guilded.NET) [![Version](https://img.shields.io/badge/Version-Beta-orange?style=for-the-badge)](https://github.com/Guilded-NET/Guilded.NET)

## 📥 Installing

Guilded.NET Webhooks is available as a package on [NuGet](https://www.nuget.org/packages/Guilded.Webhooks/) (or [FuGet](https://www.fuget.org/packages/Guilded.Webhooks/)).

You can run this command to add Guilded.NET webhooks client to an existing .NET project:

```bash
dotnet add package Guilded.Webhooks
```

Otherwise, you can install Guilded.NET templates and create new Guilded.NET projects:

```bash
dotnet new -i Guilded.Templates
dotnet new guilded.webhook
```

## ⚙️ Using Guilded.NET

You can check out [Guilded.NET's](https://guilded-net.github.io/docs/webhooks) guide to get started on your webhook client.

It is recommended to use .NET 6 or above for Guilded.NET. While Guilded.NET supports .NET 5 or similar for now, this will change in the kind-of-late future.

## 📙 Example

Here's a quick example of Guilded.NET webhook client being in use:

```cs
// Program.cs
using Guilded.Webhook;

await using var webhookClient = new GuildedWebhookClient("...url here...", "... another webhook's url here...", ...);

await webhookClient.CreateMessageAsync("Everyone, we have an announcement to make: Stop bullying!");
```

(The showcased code uses enabled implicit usings option)

## ⁉️ Support

If you need help related to Guilded.NET, you can check out these places:

- [Official Guilded.NET Server](https://guilded.gg/Guilded-NET)
- [Programming Space](https://guilded.gg/programming)

## ✅ Goals

Our goal is to provide a library or a framework that is consistent and fast, while also maintaining friendliness towards the bot developers. API library that does not bite bot developer's hand allows bot developers to focus more on their code, have fun in what they are doing and have easier time creating their bots. Consistency helps code be more predictable, easier to rewrite and waste less time. As such, these 3 points are our main goals while maintaining Guilded.NET.
