using System;
using System.Text;

namespace Guilded.Markdown;

/// <summary>
/// Provides Guilded Markdown extension methods to <see cref="StringBuilder" />.
/// </summary>
public static class MarkdownBuilder
{
    private static StringBuilder AppendInlineFormatting(StringBuilder builder, string? value, Func<string, StringBuilder> buildLine)
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

    /// <summary>
    /// Appends the specified <paramref name="value" /> in an inline code format.
    /// </summary>
    /// <param name="builder">The string builder to add <paramref name="value" /> to</param>
    /// <param name="value">The value to apply inline code formatting to</param>
    /// <returns>The <see cref="StringBuilder">string builder</see> that was passed</returns>
    public static StringBuilder AppendInlineCode(this StringBuilder builder, string? value) =>
        AppendInlineFormatting(builder, value, line => builder.Append('`').Append(line).Append('`'));
}