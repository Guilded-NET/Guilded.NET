using System;
using System.Text.RegularExpressions;
using Guilded.Commands.Items;

namespace Guilded.Commands;

/// <summary>
/// Declares the <see cref="System.Text.RegularExpressions.Regex" /> that will match the <see cref="CommandArgument">command arguments</see> with <see cref="Match" /> or <see cref="MatchCollection" /> types.
/// </summary>
/// <remarks>
/// <para>This will only be used by the <see cref="CommandArgument">command arguments</see> that have either <see cref="Match" /> or <see cref="MatchCollection" /> as their parameter types.</para>
/// </remarks>
/// <example>
/// <para>The following is an example of the <see cref="CommandRegexAttribute" /> being used with a <see cref="CommandArgument">command argument</see> that has <see cref="MatchCollection" /> parameter type.</para>
/// <code language="csharp">
/// [Command]
/// public static async Task StuffCommand(CommandEvent invokation, [CommandParam, CommandRest, CommandRegex("([0-9]+)")] MatchCollection nums) =>
///     ...;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
public sealed class CommandRegexAttribute : Attribute
{
    /// <summary>
    /// Gets the instance of <see cref="System.Text.RegularExpressions.Regex" /> for matching the  <see cref="CommandArgument">command argument</see> values.
    /// </summary>
    /// <value><see cref="CommandArgument">Command argument</see> value regex</value>
    public Regex Regex { get; }

    /// <summary>
    /// Declares the <see cref="System.Text.RegularExpressions.Regex" /> that will match the <see cref="CommandArgument">command arguments</see> with <see cref="Match" /> or <see cref="MatchCollection" /> types.
    /// </summary>
    /// <param name="regex">The instance of <see cref="System.Text.RegularExpressions.Regex" /> for matching the  <see cref="CommandArgument">command argument</see> values</param>
    public CommandRegexAttribute(Regex regex) =>
        Regex = regex;

    /// <summary>
    /// Declares the <see cref="System.Text.RegularExpressions.Regex" /> that will match the <see cref="CommandArgument">command arguments</see> with <see cref="Match" /> or <see cref="MatchCollection" /> types.
    /// </summary>
    /// <param name="expression">The formula of the <see cref="System.Text.RegularExpressions.Regex" /> matching</param>
    /// <param name="options">The options of the <see cref="System.Text.RegularExpressions.Regex" /> matching</param>
    public CommandRegexAttribute(string expression, RegexOptions options = RegexOptions.Compiled) : this(new Regex(expression, options)) { }

    /// <summary>
    /// Declares the <see cref="System.Text.RegularExpressions.Regex" /> that will match the <see cref="CommandArgument">command arguments</see> with <see cref="Match" /> or <see cref="MatchCollection" /> types.
    /// </summary>
    /// <param name="expression">The formula of the <see cref="System.Text.RegularExpressions.Regex" /> matching</param>
    /// <param name="options">The options of the <see cref="System.Text.RegularExpressions.Regex" /> matching</param>
    /// <param name="matchTimeout">The max limit of how much time <see cref="System.Text.RegularExpressions.Regex" /> matching can take</param>
    public CommandRegexAttribute(string expression, RegexOptions options, TimeSpan matchTimeout) : this(new Regex(expression, options, matchTimeout)) { }
}