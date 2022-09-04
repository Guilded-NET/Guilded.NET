using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Guilded.Base;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Markdown;

/// <summary>
/// Provides Guilded Markdown extension methods to <see cref="StringBuilder" />.
/// </summary>
public static class MarkdownBuilder
{
    #region Methods markdown
    /// <summary>
    /// Appends the specified value with its Markdown contents escaped.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendStrippedInlineMarkdown(this StringBuilder builder, string? value)
    {
        if (value is null) return builder;
        int start = builder.Length;

        return builder
            .AppendEscapedSpecificMarkdown(start, value, "`", "\\`")
            .ReplaceLastValue("*", "\\*", start)
            .ReplaceLastValue("_", "\\_", start)
            .ReplaceLastValue("~~", "\\~\\~", start)
            .ReplaceLastValue("<@", "<\\@", start)
            .ReplaceLastValue(":", "\\:", start);
    }
    #endregion

    #region Methods inline formatting
    /// <summary>
    /// Appends the specified <paramref name="value" /> in an inline code format.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply inline code formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendInlineCode(this StringBuilder builder, string? value) =>
        builder.AppendInlineFormatting(value, line => builder.Append('`').AppendEscapedSpecificMarkdown(line, "`", "\\`").Append('`'));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an inline code format.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply inline code formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendInlineCode(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append('`').AppendStrippedInlineMarkdown(line).Append('`'));


    /// <summary>
    /// Appends the specified <paramref name="value" /> as a spoiler.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply spoiler formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendSpoiler(this StringBuilder builder, string? value) =>
        builder.Append("||").AppendEscapedSpecificMarkdown(value, "||", "\\|\\|").Append("||");

    /// <summary>
    /// Appends the specified <paramref name="value" /> as a spoiler.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply spoiler formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendSpoiler(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append("||").AppendStrippedInlineMarkdown(line).Append("||"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in a bold format.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply bold formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendBold(this StringBuilder builder, string? value) =>
        builder.AppendInlineFormatting(value, line => builder.Append("**").Append(line).Append("**"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in a bold format.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply bold formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendBold(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append("**").AppendStrippedInlineMarkdown(line).Append("**"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an italic format.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply italic formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendItalic(this StringBuilder builder, string? value) =>
        builder.AppendInlineFormatting(value, line => builder.Append('_').Append(line).Append('_'));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an italic format.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply italic formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendItalic(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append('_').AppendStrippedInlineMarkdown(line).Append('_'));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an underline.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply underline formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendUnderline(this StringBuilder builder, string? value) =>
        builder.AppendInlineFormatting(value, line => builder.Append("__").Append(line).Append("__"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an underline.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply underline formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendUnderline(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append("__").AppendStrippedInlineMarkdown(line).Append("__"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an strike-through.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply strike-through formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendStrikethrough(this StringBuilder builder, string? value) =>
        builder.AppendInlineFormatting(value, line => builder.Append("~~").Append(line).Append("~~"));

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an strike-through.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply strike-through formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendStrikethrough(this StringBuilder builder, object? value) =>
        builder.AppendInlineFormatting(value?.ToString(), line => builder.Append("~~").AppendStrippedInlineMarkdown(line).Append("~~"));
    #endregion

    #region Methods additional inlines
    /// <summary>
    /// Appends a <see cref="Mentions">mention</see> of the specified <paramref name="user" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="user" /> mention to</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> to add <see cref="Mentions">mention</see> of</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendMention(this StringBuilder builder, HashId user) =>
        builder.Append("<@").Append(user).Append('>');

    /// <summary>
    /// Appends a <see cref="Mentions">mention</see> of the specified <paramref name="user" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="user" /> mention to</param>
    /// <param name="user">The <see cref="User">user</see> to add <see cref="Mentions">mention</see> of</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendMention(this StringBuilder builder, UserSummary user) =>
        builder.AppendMention(user.Id);

    /// <inheritdoc cref="AppendMention(StringBuilder, UserSummary)" />
    /// <param name="builder">The string builder to add <paramref name="user" /> mention to</param>
    /// <param name="user">The <see cref="User">user</see> <see cref="Mentions">mention</see> to add</param>
    public static StringBuilder AppendMention(this StringBuilder builder, Mentions.UserMention user) =>
        builder.AppendMention(user.Id);

    /// <summary>
    /// Appends a <see cref="Mentions">mention</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="member" /> mention to</param>
    /// <param name="member">The <see cref="Member">member</see> to add <see cref="Mentions">mention</see> of</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendMention<T>(this StringBuilder builder, MemberSummary<T> member) where T : UserSummary =>
        builder.AppendMention(member.Id);

    /// <summary>
    /// Appends a <see cref="Mentions">mention</see> of the specified <paramref name="role" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="role" /> mention to</param>
    /// <param name="role">The identifier of the role to add <see cref="Mentions">mention</see> of</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendMention(this StringBuilder builder, uint role) =>
        builder.Append("<@").Append(role).Append('>');

    /// <summary>
    /// Appends a <see cref="Mentions">mention</see> of the specified <paramref name="role" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="role" /> mention to</param>
    /// <param name="role">The role <see cref="Mentions">mention</see> to add</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendMention(this StringBuilder builder, Mentions.RoleMention role) =>
        builder.AppendMention(role.Id);

    /// <summary>
    /// Appends an <see cref="Emote">emote</see> to the string <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="emote" /> to</param>
    /// <param name="emote">The identifier of the <see cref="Emote">emote</see> to add</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendEmote(this StringBuilder builder, uint emote) =>
        builder.Append(':').Append(emote).Append(':');

    /// <summary>
    /// Appends an <see cref="Emote">emote</see> to the string <paramref name="builder" />.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="emote" /> to</param>
    /// <param name="emote">The <see cref="Emote">emote</see> to add</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendEmote(this StringBuilder builder, Emote emote) =>
        builder.AppendEmote(emote.Id);

    /// <summary>
    /// Appends the prefix of a hyperlink.
    /// </summary>
    /// <param name="builder">The string builder to add hyperlink prefix to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendHyperlinkPrefix(this StringBuilder builder) =>
        builder.Append('[');

    /// <summary>
    /// Appends the infix of a hyperlink between the text and the URL.
    /// </summary>
    /// <param name="builder">The string builder to add hyperlink infix to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendHyperlinkInfix(this StringBuilder builder) =>
        builder.Append("](");

    /// <summary>
    /// Appends the suffix/postfix of a hyperlink.
    /// </summary>
    /// <param name="builder">The string builder to add hyperlink postfix to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendHyperlinkPostfix(this StringBuilder builder) =>
        builder.Append(')');

    /// <summary>
    /// Appends the specified hyperlinked <paramref name="text" />.
    /// </summary>
    /// <param name="builder">The string builder to add hyperlinked <paramref name="text" /> to</param>
    /// <param name="text">The text to hyperlink with the specified <paramref name="url" /> and add to the <paramref name="builder" /></param>
    /// <param name="url">The URL to hyperlink the <paramref name="text" /> with</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendHyperlink(this StringBuilder builder, string? text, Uri url) =>
        builder.AppendHyperlinkPrefix().AppendEscapedSpecificMarkdown(text, "](", "\\]\\(").AppendHyperlinkInfix().Append(url).AppendHyperlinkPostfix();

    /// <summary>
    /// Appends the specified hyperlinked <paramref name="text" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="AppendStrippedInlineMarkdown(StringBuilder, string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="builder">The string builder to add hyperlinked <paramref name="text" /> to</param>
    /// <param name="text">The text to hyperlink with the specified <paramref name="url" /> and add to the <paramref name="builder" /></param>
    /// <param name="url">The URL to hyperlink the <paramref name="text" /> with</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendHyperlink(this StringBuilder builder, object? text, Uri url) =>
        builder.AppendHyperlinkPrefix().AppendStrippedInlineMarkdown(text?.ToString()).AppendHyperlinkInfix().Append(url).AppendHyperlinkPostfix();
    #endregion

    #region Methods private
    private static StringBuilder AppendInlineFormatting(this StringBuilder builder, string? value, Func<string, StringBuilder> buildLine)
    {
        if (value is null) return builder;

        string[] lines = value.Split("\n");

        int i = 0;

        foreach (string line in lines)
        {
            buildLine(line);
            if (lines.Length > i) builder.AppendLine();
            i++;
        }

        return builder;
    }

    private static StringBuilder ReplaceLastValue(this StringBuilder builder, string fromStr, string toStr, int preValueLength) =>
        builder.Replace(fromStr, toStr, preValueLength, builder.Length - preValueLength);

    private static StringBuilder AppendEscapedSpecificMarkdown(this StringBuilder builder, string? value, string fromStr, string toStr)
    {
        // Could be merged with AppendEscapedInlineMarkdown, but eh
        if (value is null) return builder;
        int start = builder.Length;

        builder.Append(value);

        if (value.EndsWith("\\", StringComparison.InvariantCulture) && !value.EndsWith("\\\\", StringComparison.InvariantCulture)) builder.ReplaceLastValue("\\", "\\\\", value.Length - 1);

        return builder.ReplaceLastValue(fromStr, toStr, start);
    }

    private static StringBuilder AppendEscapedSpecificMarkdown(this StringBuilder builder, int start, string value, string fromStr, string toStr) =>
        builder
            .Append(value)
            .ReplaceLastValue("\\", "\\\\", start)
            .ReplaceLastValue(fromStr, toStr, start);
    #endregion
}