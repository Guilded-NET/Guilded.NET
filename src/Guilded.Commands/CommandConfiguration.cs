using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;
using Guilded.Base;
using Guilded.Client;
using Guilded.Commands.Items;

namespace Guilded.Commands;

/// <summary>
/// Represents the module that adds <see cref="CommandAttribute">commands</see> to <see cref="AbstractGuildedClient">Guilded clients</see>.
/// </summary>
/// <seealso cref="CommandParent" />
/// <seealso cref="CommandAttribute" />
/// <seealso cref="CommandFallbackAttribute" />
public record CommandConfiguration
{
    #region Static & Constants
    /// <summary>
    /// The default argument separator characters.
    /// </summary>
    /// <remarks>
    /// <para>By default, <c> </c>, <c>\t</c>, <c>\v</c>, <c>\n</c> and <c>\r</c> will be used.</para>
    /// </remarks>
    /// <value>The default argument separator characters</value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="Separators" />
    /// <seealso cref="DefaultSplitOptions" />
    /// <seealso cref="DefaultPrefix" />
    public static readonly char[] DefaultSeparators = new char[] { ' ', '\t', '\n' };

    /// <summary>
    /// The default splitting options for <see cref="CommandParamAttribute">command arguments</see>.
    /// </summary>
    /// <remarks>
    /// <para>By default, it uses <see cref="StringSplitOptions.RemoveEmptyEntries" />.</para>
    /// </remarks>
    /// <value>The default splitting options for <see cref="CommandParamAttribute">command arguments</see></value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="SplitOptions" />
    /// <seealso cref="DefaultSeparators" />
    /// <seealso cref="DefaultPrefix" />
    public const StringSplitOptions DefaultSplitOptions = StringSplitOptions.RemoveEmptyEntries;

    /// <summary>
    /// The prefix that will be used by default if not specified.
    /// </summary>
    /// <remarks>
    /// <para>By default, it's <c>/</c>.</para>
    /// </remarks>
    /// <value>The prefix that will be used by default if not specified</value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="DefaultSeparators" />
    /// <seealso cref="DefaultSplitOptions" />
    public const string DefaultPrefix = "/";
    #endregion

    #region Properties
    /// <summary>
    /// Gets the piece of text with which commands need to start with.
    /// </summary>
    /// <remarks>
    /// <para>If no prefix is specified, <see cref="DefaultPrefix">Guilded.NET's default prefix</see> will be used instead.</para>
    /// </remarks>
    /// <value>The piece of text with which commands need to start with</value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="DefaultPrefix" />
    /// <seealso cref="Separators" />
    /// <seealso cref="SplitOptions" />
    public string Prefix { get; set; }

    /// <summary>
    /// Gets the characters that will be used to separate <see cref="CommandParamAttribute">command arguments</see>.
    /// </summary>
    /// <remarks>
    /// <para>If no separators are specified, <see cref="DefaultSeparators">Guilded.NET's default separators</see> will be used instead.</para>
    /// </remarks>
    /// <value>The characters that will be used to separate <see cref="CommandParamAttribute">command arguments</see></value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="SplitOptions" />
    /// <seealso cref="ArgumentConverters" />
    public char[] Separators { get; set; }

    /// <summary>
    /// Gets the splitting options that will be used for splitting the <see cref="CommandParamAttribute">command arguments</see>.
    /// </summary>
    /// <remarks>
    /// <para>If no split options are specified, <see cref="DefaultSplitOptions">Guilded.NET's default split options</see> will be used instead.</para>
    /// </remarks>
    /// <value>The splitting options that will be used for splitting the <see cref="CommandParamAttribute">command arguments</see></value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="Separators" />
    /// <seealso cref="ArgumentConverters" />
    public StringSplitOptions SplitOptions { get; set; }

    /// <summary>
    /// Gets the dictionary of <see cref="ArgumentConverter">command argument converts</see>.
    /// </summary>
    /// <value>The dictionary of <see cref="ArgumentConverter">command argument converts</see></value>
    /// <seealso cref="CommandConfiguration" />
    /// <seealso cref="Prefix" />
    /// <seealso cref="SplitOptions" />
    /// <seealso cref="Separators" />
    public Dictionary<Type, ArgumentConverter> ArgumentConverters { get; set; } =
        new()
        {
            { typeof(string),   (CommandArgument _, CommandConfiguration _, string x, [NotNullWhen(true)] out object? y) => { y = x; return true; } },
            { typeof(int),      CommandArgument.FromParser<int>(int.TryParse) },
            { typeof(bool),     CommandArgument.FromParser<bool>(bool.TryParse) },
            { typeof(Guid),     CommandArgument.FromParser<Guid>(Guid.TryParse) },
            { typeof(HashId),   CommandArgument.FromParser<HashId>(HashId.TryParse) },
            { typeof(uint),     CommandArgument.FromParser<uint>(uint.TryParse) },
            { typeof(long),     CommandArgument.FromParser<long>(long.TryParse) },
            { typeof(ulong),    CommandArgument.FromParser<ulong>(ulong.TryParse) },
            { typeof(float),    CommandArgument.FromParser<float>(float.TryParse) },
            { typeof(short),    CommandArgument.FromParser<short>(short.TryParse) },
            { typeof(ushort),   CommandArgument.FromParser<ushort>(ushort.TryParse) },
            { typeof(byte),     CommandArgument.FromParser<byte>(byte.TryParse) },
            { typeof(sbyte),    CommandArgument.FromParser<sbyte>(sbyte.TryParse) },
            { typeof(double),   CommandArgument.FromParser<double>(double.TryParse) },
            { typeof(decimal),  CommandArgument.FromParser<decimal>(decimal.TryParse) },
            { typeof(char),     CommandArgument.FromParser<char>(char.TryParse) },
            {
                typeof(Match),
                (CommandArgument arg, CommandConfiguration config, string y, [NotNullWhen(true)] out object? z) =>
                {
                    CommandRegexAttribute attr = arg.Parameter.GetCustomAttribute<CommandRegexAttribute>()!;

                    Match match = attr.Regex.Match(y);
                    z = match;

                    return match.Success;
                }
            },
            {
                typeof(MatchCollection),
                (CommandArgument arg, CommandConfiguration config, string y, [NotNullWhen(true)] out object? z) =>
                {
                    CommandRegexAttribute attr = arg.Parameter.GetCustomAttribute<CommandRegexAttribute>()!;

                    MatchCollection matches = attr.Regex.Matches(y);
                    z = matches;

                    return matches.Count > 0;
                }
            },
            {
                typeof(string[]),
                (CommandArgument _, CommandConfiguration config, string argument, [NotNullWhen(true)] out object? value) =>
                {
                    value = argument is null ? Array.Empty<string>() : argument.Split(config.Separators, config.SplitOptions);
                    return true;
                }
            },
            { typeof(Uri),
                (CommandArgument _, CommandConfiguration _, string argument, [NotNullWhen(true)] out object? value) =>
                {
                    // Since out object cannot get value from out Uri? for some reason
                    bool b = Uri.TryCreate(argument, UriKind.Absolute, out Uri? uriValue);
                    value = uriValue;
                    return b;
                }
            },
            { typeof(DateTime), CommandArgument.FromParser<DateTime>(DateTime.TryParse) },
            { typeof(TimeSpan), CommandArgument.FromParser<TimeSpan>(TimeSpan.TryParse) }
        };
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes the configuration of Guilded.NET's commands.
    /// </summary>
    /// <param name="prefix">The prefix with which all commands should start</param>
    /// <param name="separators">The separators that split the command's arguments</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandConfiguration(string prefix, char[] separators, StringSplitOptions splitOptions = DefaultSplitOptions) =>
        (Prefix, Separators, SplitOptions) = (prefix, separators, splitOptions);

    /// <summary>
    /// Initializes the configuration of Guilded.NET's commands.
    /// </summary>
    /// <param name="prefix">The context-based prefix method for commands</param>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandConfiguration(string prefix, StringSplitOptions splitOptions = DefaultSplitOptions) : this(prefix, DefaultSeparators, splitOptions) { }

    /// <summary>
    /// Initializes the configuration of Guilded.NET's commands with <c>/</c> prefix.
    /// </summary>
    /// <param name="splitOptions">The splitting options of the command's arguments</param>
    public CommandConfiguration(StringSplitOptions splitOptions = DefaultSplitOptions) : this(DefaultPrefix, splitOptions) { }

    /// <summary>
    /// Initializes the configuration of Guilded.NET's commands with <c>/</c> prefix.
    /// </summary>
    public CommandConfiguration() : this(DefaultPrefix) { }
    #endregion

    #region Methods
    internal string[] SplitWithConfig(string text) =>
        text.Split(Separators, SplitOptions);
    #endregion
}