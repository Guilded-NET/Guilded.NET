using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Guilded;
using Newtonsoft.Json.Linq;

// Get the configuration values
JObject config = JObject.Parse(await File.ReadAllTextAsync("./config/config.json").ConfigureAwait(false));

string? auth = config.Value<string>("auth"),
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
