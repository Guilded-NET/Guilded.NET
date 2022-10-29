<div align="center">

[![Banner](https://raw.githubusercontent.com/Guilded-NET/Guilded.NET/early-access/assets/Banner.png)](https://github.com/Guilded-NET/Guilded.NET)

# 🟡 Guilded.NET's Markdown package
</div>

Guilded.Markdown is an utility package for Guilded-flavoured Markdown, as well as Guilded content. It adds things like `MarkdownBuilder`, `GuildedMarkdown` for `InlineCode`, `Bold`, etc. and the ability to react with emotes by specifying their name or character.

## 📙 Example

```cs
using Guilded.Markdown;
using static Guilded.Markdown.GuildedMarkdown;

// ...
Message message = await client.CreateMessageAsync(channelId, $"Here's some inline code: {InlineCode("xyz")}");
await message.AddReactionAsync("white_check_mark");
await message.AddReactionAsync('❌');
```

## ⁉️ Support

If you need any help related to Guilded.NET, you can check out the following sources:

- [Official Guilded.NET Server](https://guilded.gg/Guilded-NET)
- [Programming Space](https://guilded.gg/programming)
