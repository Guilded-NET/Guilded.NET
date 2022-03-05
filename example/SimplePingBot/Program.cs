using System.Reactive.Linq;
using Guilded;
using Newtonsoft.Json.Linq;

// Get the configuration values
JObject config = JObject.Parse(await File.ReadAllTextAsync("./config/config.json"));

string auth = config.Value<string>("auth")!,
       prefix = config.Value<string>("prefix")!;

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
