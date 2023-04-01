# 🟡 Guilded.Commands

Adds the ability for Guilded.NET clients to process text commands.

## 📥 Installing

You can run this command to add Guilded.NET commands to an existing .NET project:

```bash
# If you haven't installed Guilded yet
dotnet add package Guilded
# To install Guilded.Commands
dotnet add package Guilded.Commands
```

Otherwise, you can use Guilded.NET commands from a template while creating a new Guilded.NET projects:

```bash
dotnet new -i Guilded.Templates
dotnet new guilded.command
```

## ⚙️ Using Guilded.NET commands

You can check out [commands page in Guilded.NET's docs](https://guilded-net.github.io/docs/commands) to get started with Guilded.NET commands.

It is recommended to use .NET 6 or above for Guilded.NET Commands and Guilded.NET. While all Guilded.NET packages support .NET 5 for now, this will definitely change in the future.

## 📙 Example

Here's a quick example of a starter Guilded.NET bot with a few commands using Guilded.NET commands:

```cs
// Program.cs
using System.Reactive.Linq;
using Guilded;
using ProjectName;

string auth   = "your_bots_auth_token",
       prefix = "!";

await using var client = new GuildedBotClient(auth).AddCommands(new BotCommands(), prefix);

client
    .Prepared
    .Subscribe(me =>
        Console.WriteLine("The bot is prepared!\nLogged in as \"{0}\" with the ID \"{1}\"", me.Name, me.Id)
    );

await client.ConnectAsync();

// Don't close the program when the bot connects; not recommended to put code after this
await Task.Delay(-1);
```

```cs
// BotCommands.cs
using Guilded;
using Guilded.Commands;

namespace ProjectName;

public partial class BotCommands : CommandModule
{
    [Command(Aliases = new string[] { "p" })]
    public static Task Ping(CommandEvent invokation) =>
        invokation.ReplyAsync("Pong!");

    [Command(Aliases = new string[] { "commands", "h" })]
    public Task Help(CommandEvent invokation)
    {
        var commandNames = CommandNames;

        return invokation.ReplyAsync($"Here are available commands: `{string.Join("`, `", commandNames)}`");
    }

    [Description("This does stuff.")]
    [Command(Aliases = new string[] { "ex", "e" })]
    [Example("10"), Example("ex", "50")]
    public static async Task Example(CommandEvent invokation, [CommandParam("number to say")] int number)
    {
        await invokation.ReplyAsync($"Someone secretly said number `{number}`");

        // Delete the command message ("/example 10")
        await invokation.DeleteAsync();
    }
}
```

> **Note**: The code above uses enabled implicit usings option.

## ⁉️ Support

If you need any help related to Guilded.NET, you can check out the following sources:

- [Official Guilded.NET Server](https://guilded.gg/Guilded-NET)
- [Programming Space](https://guilded.gg/programming)