using System;
using System.Collections.Generic;
using System.Linq;
using Guilded.Base;
using Guilded.Base.Content;
using Guilded.Base.Servers;
using Guilded.Base.Users;

namespace Guilded.Markdown;

/// <summary>
/// Provides utility methods for formatting in Guilded Markdown.
/// </summary>
public static class GuildedMarkdown
{
    #region Fields
    /// <summary>
    /// The syntax for Markdown dividers.
    /// </summary>
    public const string Divider = "---";
    #endregion

    #region Methods Inline Markdown
    /// <summary>
    /// Returns inline code formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to apply inline code formatting to</param>
    /// <returns>Inline code formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string InlineCode(string? value) =>
        ApplyToLines(value?.Replace("`", "\\`"), x => $"`{x}`");

    /// <summary>
    /// Returns inline code formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to apply inline code formatting to</param>
    /// <returns>Inline code formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string InlineCode(object? value) =>
        InlineCode(value);

    /// <summary>
    /// Returns the specified <paramref name="value" /> as a spoiler.
    /// </summary>
    /// <param name="value">The value to apply spoiler formatting to</param>
    /// <returns><paramref name="value">Value</paramref> as a spoiler</returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Spoiler(string? value) =>
        $"||{value?.Replace("||", "\\|\\|") ?? " "}||";

    /// <summary>
    /// Returns the specified <paramref name="value" /> as a spoiler.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply spoiler formatting to</param>
    /// <returns><paramref name="value">Value</paramref> as a spoiler</returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Spoiler(object? value) =>
        Spoiler(StripInlineMarkdown(value) ?? " ");


    /// <summary>
    /// Returns bold formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to apply bold formatting to</param>
    /// <returns>Bold formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Bold(string? value) =>
        ApplyToLines(value, x => $"**{x}**");

    /// <summary>
    /// Returns bold formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply bold formatting to</param>
    /// <returns>Bold formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Bold(object? value) =>
        Bold(StripInlineMarkdown(value));

    /// <summary>
    /// Returns italic formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply italic formatting to</param>
    /// <returns>Italic formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Italic(string? value) =>
        ApplyToLines(value, x => $"_{x}_");

    /// <summary>
    /// Returns italic formatting applied to the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply italic formatting to</param>
    /// <returns>Italic formatting applied to <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Italic(object? value) =>
        Italic(StripInlineMarkdown(value));

    /// <summary>
    /// Returns the underlined version of the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply underline formatting to</param>
    /// <returns>Underlined <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Underline(string? value) =>
        ApplyToLines(value, x => $"__{x}__");

    /// <summary>
    /// Returns the underlined version of the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply underline formatting to</param>
    /// <returns>Underlined <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Underline(object? value) =>
        Underline(StripInlineMarkdown(value));

    /// <summary>
    /// Returns the striked-through version of the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply strike-through formatting to</param>
    /// <returns>Striked-through <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(object)" />
    public static string Strikethrough(string? value) =>
        ApplyToLines(value, x => $"~~{x}~~");

    /// <summary>
    /// Returns the striked-through version of the specified <paramref name="value" />.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to apply strike-through formatting to</param>
    /// <returns>Striked-through <paramref name="value" /></returns>
    /// <seealso cref="InlineCode(object)" />
    /// <seealso cref="InlineCode(string)" />
    /// <seealso cref="Spoiler(object)" />
    /// <seealso cref="Spoiler(string)" />
    /// <seealso cref="Bold(object)" />
    /// <seealso cref="Bold(string)" />
    /// <seealso cref="Italic(object)" />
    /// <seealso cref="Italic(string)" />
    /// <seealso cref="Underline(object)" />
    /// <seealso cref="Underline(string)" />
    /// <seealso cref="Strikethrough(string)" />
    public static string Strikethrough(object? value) =>
        Strikethrough(StripInlineMarkdown(value));
    #endregion

    #region Methods additional inlines
    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The identifier of the <see cref="User">user</see> to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="user" /></returns>
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention(Mentions.UserMention)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention(HashId user) =>
        $"<@{user}>";

    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The <see cref="User">user</see> to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="user" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(Mentions.UserMention)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention(UserSummary user) =>
        Mention(user.Id);

    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The <see cref="User">user</see> to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="user" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention(Mentions.UserMention user) =>
        Mention(user.Id);

    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="member" />.
    /// </summary>
    /// <param name="member">The <see cref="Member">member</see> to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="member" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention<T>(MemberSummary<T> member) where T : UserSummary =>
        Mention(member.User);

    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="role" />.
    /// </summary>
    /// <param name="role">The identifier of the role to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="role" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention(Mentions.UserMention)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention(uint role) =>
        $"<@{role}>";

    /// <summary>
    /// Returns the <see cref="Mentions">mention</see> of the specified <paramref name="role" />.
    /// </summary>
    /// <param name="role">The identifier of the role to <see cref="Mentions">mention</see></param>
    /// <returns>The <see cref="Mentions">mention</see> of the specified <paramref name="role" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Mention(Mentions.RoleMention role) =>
        Mention(role.Id);

    /// <summary>
    /// Returns the specified <paramref name="value" /> hyperlinked with the specified <paramref name="url" />.
    /// </summary>
    /// <param name="value">The value to hyperlink</param>
    /// <param name="url">The URL to hyperlink the <paramref name="value" /> with</param>
    /// <returns><paramref name="value">Value</paramref> hyperlinked with <paramref name="url" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention(Mentions.UserMention)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Hyperlink(string? value, Uri url) =>
        $"[{value?.Replace("](", "\\]\\(") ?? " "}]({url})";

    /// <inheritdoc cref="Hyperlink(string, Uri)" />
    /// <param name="value">The value to hyperlink</param>
    /// <param name="url">The URL to hyperlink the <paramref name="value" /> with</param>
    public static string Hyperlink(string? value, string url) =>
        Hyperlink(value, new Uri(url));

    /// <inheritdoc cref="Hyperlink(string, Uri)" />
    /// <param name="value">The value to hyperlink</param>
    /// <param name="url">The URL to hyperlink the <paramref name="value" /> with</param>
    public static string Hyperlink(object? value, Uri url) =>
        Hyperlink(value?.ToString(), url);

    /// <inheritdoc cref="Hyperlink(string, Uri)" />
    /// <param name="value">The value to hyperlink</param>
    /// <param name="url">The URL to hyperlink the <paramref name="value" /> with</param>
    public static string Hyperlink(object? value, string url) =>
        Hyperlink(value, new Uri(url));

    /// <summary>
    /// Returns the image from the specified <paramref name="url" />.
    /// </summary>
    /// <param name="url">The URL to the image</param>
    /// <returns>The image from the specified <paramref name="url" /></returns>
    /// <seealso cref="Mention(HashId)" />
    /// <seealso cref="Mention(UserSummary)" />
    /// <seealso cref="Mention(Mentions.UserMention)" />
    /// <seealso cref="Mention{T}(MemberSummary{T})" />
    /// <seealso cref="Mention(uint)" />
    /// <seealso cref="Mention(Mentions.RoleMention)" />
    /// <seealso cref="Hyperlink(string, Uri)" />
    /// <seealso cref="Hyperlink(string, string)" />
    /// <seealso cref="Hyperlink(object, Uri)" />
    /// <seealso cref="Hyperlink(object, string)" />
    /// <seealso cref="Media(Uri)" />
    /// <seealso cref="Media(string)" />
    public static string Media(Uri url) =>
        $"!{Hyperlink(string.Empty, url)}";

    /// <inheritdoc cref="Media(Uri)" />
    /// <param name="url">The URL to the image</param>
    public static string Media(string url) =>
        Media(new Uri(url));
    #endregion

    #region Methods Block markdown
    /// <summary>
    /// Returns the specified <paramref name="code" /> formatted in a code block with the specified <paramref name="language" />
    /// </summary>
    /// <remarks>
    /// <para>If the <paramref name="language" /> is not given, the <paramref name="code"/> will not be highlighted in any language.</para>
    /// </remarks>
    /// <param name="code">The code to format in code block formatting</param>
    /// <param name="language">The language to highlight <paramref name="code" /> in.</param>
    /// <returns>The <paramref name="code" /> in a code block and highlighted in <paramref name="language" /></returns>
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string CodeBlock(string? code, string language = "") =>
        $"""
        ```{language}
        {code?.Replace("`", "\\`") ?? string.Empty}
        ```
        """;

    /// <summary>
    /// Returns the specified <paramref name="value" /> formatted in a code block with the specified <paramref name="language" />
    /// </summary>
    /// <remarks>
    /// <para>If the <paramref name="language" /> is not given, the <paramref name="value"/> will not be highlighted in any language.</para>
    /// </remarks>
    /// <param name="value">The object to format in code block formatting</param>
    /// <param name="language">The language to highlight <paramref name="value" /> in.</param>
    /// <returns>The <paramref name="value" /> in a code block and highlighted in <paramref name="language" /></returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string CodeBlock(object? value, string language = "") =>
        CodeBlock(value?.ToString(), language);

    /// <summary>
    /// Returns the given code <paramref name="lines" /> formatted in a code block with the specified <paramref name="language" />
    /// </summary>
    /// <remarks>
    /// <para>If the <paramref name="language" /> is not given, the given code <paramref name="lines"/> will not be highlighted in any language.</para>
    /// </remarks>
    /// <param name="lines">The code to format in code block formatting</param>
    /// <param name="language">The language to highlight code <paramref name="lines" /> in.</param>
    /// <returns>The code <paramref name="lines" /> highlighted in <paramref name="language" /></returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string CodeBlock(IEnumerable<string> lines, string language = "") =>
        CodeBlock(string.Join('\n', lines), language);

    /// <summary>
    /// Returns the given code <paramref name="lines" /> formatted in a code block with the specified <paramref name="language" />
    /// </summary>
    /// <remarks>
    /// <para>If the <paramref name="language" /> is not given, the given code <paramref name="lines"/> will not be highlighted in any language.</para>
    /// </remarks>
    /// <param name="lines">The code to format in code block formatting</param>
    /// <param name="language">The language to highlight code <paramref name="lines" /> in.</param>
    /// <returns>The code <paramref name="lines" /> highlighted in <paramref name="language" /></returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string CodeBlock(IEnumerable<object> lines, string language = "") =>
        CodeBlock(lines.Select(x => x?.ToString() ?? string.Empty), language);

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted in a quote.
    /// </summary>
    /// <param name="value">The value to format in a quote block</param>
    /// <returns>The quoted <paramref name="value" /></returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string QuoteBlock(string? value) =>
        ApplyToLines(value ?? string.Empty, x => $"> {x}");

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted in a quote.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to format in a quote block</param>
    /// <returns>The quoted <paramref name="value" /></returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string QuoteBlock(object? value) =>
        QuoteBlock(StripMarkdown(value?.ToString()));

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted as a large header.
    /// </summary>
    /// <param name="value">The value to format in a large header</param>
    /// <returns>The <paramref name="value" /> formatted in a large header</returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string LargeHeader(string? value) =>
        ApplyToLines(value ?? " ", x => $"# {x}");

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted as a large header.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to format in a large header</param>
    /// <returns>The <paramref name="value" /> formatted in a large header</returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string LargeHeader(object? value) =>
        LargeHeader(StripMarkdown(value?.ToString()));

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted as a small header.
    /// </summary>
    /// <param name="value">The value to format in a small header</param>
    /// <returns>The <paramref name="value" /> formatted in a small header</returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string SmallHeader(string? value) =>
        ApplyToLines(value ?? " ", x => $"## {x}");

    /// <summary>
    /// Returns the given <paramref name="value" /> formatted as a small header.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="value">The value to format in a small header</param>
    /// <returns>The <paramref name="value" /> formatted in a small header</returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string SmallHeader(object? value) =>
        SmallHeader(value?.ToString());

    /// <summary>
    /// Returns the given <paramref name="lines" /> converted to individual paragraphs.
    /// </summary>
    /// <remarks>
    /// <para>String that is fetched from the object will have all of its already present Markdown contents <see cref="StripMarkdown(string)">stripped away</see>.</para>
    /// </remarks>
    /// <param name="lines">The sequence of lines to convert to paragraphs</param>
    /// <returns>The <paramref name="lines" /> as multiple paragraphs</returns>
    /// <seealso cref="CodeBlock(string, string)" />
    /// <seealso cref="CodeBlock(object, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{object}, string)" />
    /// <seealso cref="CodeBlock(IEnumerable{string}, string)" />
    /// <seealso cref="QuoteBlock(string)" />
    /// <seealso cref="QuoteBlock(object)" />
    /// <seealso cref="LargeHeader(string)" />
    /// <seealso cref="LargeHeader(object)" />
    /// <seealso cref="SmallHeader(string)" />
    /// <seealso cref="SmallHeader(object)" />
    /// <seealso cref="Paragraphs(IEnumerable{string})" />
    /// <seealso cref="Paragraphs(string[])" />
    public static string Paragraphs(IEnumerable<string> lines) =>
        string.Join("\n\n", lines);

    /// <inheritdoc cref="Paragraphs(IEnumerable{string})" />
    /// <param name="lines">The array of lines to convert to paragraphs</param>
    public static string Paragraphs(params string[] lines) =>
        Paragraphs((IEnumerable<string>)lines);
    #endregion

    #region Methods strip markdown
    /// <summary>
    /// Returns a version of <paramref name="value" /> that has no inline Markdown.
    /// </summary>
    /// <param name="value">The value to remove Markdown from</param>
    /// <returns>The <paramref name="value" /> with no inline Markdown</returns>
    /// <seealso cref="StripInlineMarkdown(string)" />
    /// <seealso cref="StripInlineMarkdown(object)" />
    /// <seealso cref="StripMarkdown(string)" />
    /// <seealso cref="StripMarkdown(object)" />
    public static string? StripInlineMarkdown(string? value) =>
        value?
            .Replace("\\", "\\\\")
            // General inline formatting
            .Replace("*", "\\*")
            .Replace("_", "\\_")
            .Replace("~~", "\\~\\~")
            .Replace("||", "\\|\\|")
            .Replace("`", "\\`")
            // Emotes
            .Replace(":", "\\:")
            // Mentions
            .Replace("<@", "<\\@")
            // Hyperlinks, images
            .Replace("](", "]\\(");

    /// <inheritdoc cref="StripInlineMarkdown(string)" />
    /// <param name="value">The value to remove Markdown from</param>
    public static string? StripInlineMarkdown(object? value) =>
        StripInlineMarkdown(value?.ToString());

    /// <summary>
    /// Returns a version of <paramref name="value" /> that has no Markdown.
    /// </summary>
    /// <param name="value">The value to remove Markdown from</param>
    /// <returns>The <paramref name="value" /> with no Markdown</returns>
    /// <seealso cref="StripInlineMarkdown(string)" />
    /// <seealso cref="StripInlineMarkdown(object)" />
    /// <seealso cref="StripMarkdown(string)" />
    /// <seealso cref="StripMarkdown(object)" />
    public static string? StripMarkdown(string? value) =>
        value is null
        ? value
        : string.Join('\n', StripInlineMarkdown(value)!.Split('\n').Select(EscapePrefixedFormat));

    /// <inheritdoc cref="StripMarkdown(string)" />
    /// <param name="value">The value to remove Markdown from</param>
    public static string? StripMarkdown(object? value) =>
        StripMarkdown(value?.ToString());
    #endregion

    #region Methods private
    private static string ApplyToLines(string? value, Func<string, string> format) =>
        value is not null
        ? string.Join('\n', value.Split('\n').Select(format))
        : string.Empty;

    private static string EscapePrefixedFormat(string value) =>
        value.StartsWith("> ") || value.StartsWith("# ") || value.StartsWith("## ")
        ? $"\\{value}"
        : value;
    #endregion
}