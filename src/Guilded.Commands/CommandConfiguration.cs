using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Guilded.Base;
using Guilded.Client;
using Guilded.Commands.Items;
using Guilded.Servers;

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
    public static readonly char[] DefaultSeparators = [' ', '\t', '\n'];

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
            {
                typeof(string),
                (CommandArgument arg, RootCommandEvent rootInvokation, ref string x, out object? y) =>
                {
                    if (arg.IsRest)
                    {
                        y = x;
                        x = string.Empty;
                        return true;
                    }

                    string[] split = x.Split(rootInvokation.Configuration.Separators, 2, rootInvokation.Configuration.SplitOptions);

                    x = split.ElementAtOrDefault(1) ?? string.Empty;
                    y = split.First();

                    return true;
                }
            },
            { typeof(int),            CommandArgument.FromParser<int>(int.TryParse) },
            { typeof(bool),           CommandArgument.FromParser<bool>(bool.TryParse) },
            { typeof(Member),         GetMemberFromArgument },
            { typeof(Role),           GetRoleFromArgument },
            { typeof(ServerChannel),  GetChannelFromArgument },
            { typeof(Guid),           CommandArgument.FromParser<Guid>(Guid.TryParse) },
            { typeof(HashId),         CommandArgument.FromParser<HashId>(HashId.TryParse) },
            { typeof(uint),           CommandArgument.FromParser<uint>(uint.TryParse) },
            { typeof(long),           CommandArgument.FromParser<long>(long.TryParse) },
            { typeof(ulong),          CommandArgument.FromParser<ulong>(ulong.TryParse) },
            { typeof(float),          CommandArgument.FromParser<float>(float.TryParse) },
            { typeof(short),          CommandArgument.FromParser<short>(short.TryParse) },
            { typeof(ushort),         CommandArgument.FromParser<ushort>(ushort.TryParse) },
            { typeof(byte),           CommandArgument.FromParser<byte>(byte.TryParse) },
            { typeof(sbyte),          CommandArgument.FromParser<sbyte>(sbyte.TryParse) },
            { typeof(double),         CommandArgument.FromParser<double>(double.TryParse) },
            { typeof(decimal),        CommandArgument.FromParser<decimal>(decimal.TryParse) },
            { typeof(char),           CommandArgument.FromParser<char>(char.TryParse) },
            {
                typeof(Match),
                (CommandArgument arg, RootCommandEvent rootInvokation, ref string y, out object? z) =>
                {
                    CommandRegexAttribute attr = arg.Parameter.GetCustomAttribute<CommandRegexAttribute>()!;

                    Match match = attr.Regex.Match(y);
                    z = match;
                    y = y[match.Length..].TrimStart(rootInvokation.Configuration.Separators);

                    return match.Success;
                }
            },
            {
                typeof(MatchCollection),
                (CommandArgument arg, RootCommandEvent rootInvokation, ref string y, out object? z) =>
                {
                    CommandRegexAttribute attr = arg.Parameter.GetCustomAttribute<CommandRegexAttribute>()!;

                    MatchCollection matches = attr.Regex.Matches(y);
                    z = matches;
                    y = string.Empty;

                    return matches.Count > 0;
                }
            },
            {
                typeof(string[]),
                (CommandArgument _, RootCommandEvent rootInvokation, ref string argument, out object? value) =>
                {
                    value = argument is null ? [] : argument.Split(rootInvokation.Configuration.Separators, rootInvokation.Configuration.SplitOptions);

                    argument = string.Empty;

                    return true;
                }
            },
            { typeof(Uri),
                (CommandArgument _, RootCommandEvent rootInvokation, ref string arguments, out object? value) =>
                {
                    string[] split = arguments.Split(rootInvokation.Configuration.Separators, 2, rootInvokation.Configuration.SplitOptions);

                    // Since out object cannot get value from out Uri? for some reason
                    bool b = Uri.TryCreate(split.First(), UriKind.Absolute, out Uri? uriValue);
                    value = uriValue;
                    arguments = split.ElementAtOrDefault(1) ?? string.Empty;

                    return b;
                }
            },
            { typeof(DateTime),       CommandArgument.FromParser<DateTime>(DateTime.TryParse) },
            { typeof(TimeSpan),       CommandArgument.FromParser<TimeSpan>(TimeSpan.TryParse) }
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

    #region Static methods
    internal static bool GetMemberFromArgument(CommandArgument _, RootCommandEvent rootInvokation, ref string arguments, out object? value)
    {
        value = null;

        // It's a mention
        if (!arguments.StartsWith('@'))
            return false;

        string afterAmpersat = arguments[1..];
        Member? knownMember = rootInvokation.KnownMembers.FirstOrDefault(x => ArgumentContainsName(afterAmpersat, x.DisplayName, rootInvokation.Configuration.Separators));

        // To use up the argument and not leave Guilded.NET confused
        if (knownMember is not null)
        {
            arguments = arguments[(1 + knownMember.DisplayName.Length)..].TrimStart(rootInvokation.Configuration.Separators);
        }

        value = knownMember!;

        return knownMember is not null;
    }

    internal static bool GetRoleFromArgument(CommandArgument _, RootCommandEvent rootInvokation, ref string arguments, out object? value) =>
        GetItemFromArgument('@', rootInvokation.Configuration, ref arguments, out value, rootInvokation.KnownRoles);

    internal static bool GetChannelFromArgument(CommandArgument _, RootCommandEvent rootInvokation, ref string arguments, out object? value) =>
        GetItemFromArgument('#', rootInvokation.Configuration, ref arguments, out value, rootInvokation.KnownChannels);

    internal static bool GetItemFromArgument<T>(char prefix, CommandConfiguration config, ref string arguments, [NotNullWhen(true)] out object? value, IEnumerable<T> knownList)
        where T : IModelHasName
    {
        value = null;

        // It's a mention
        if (!arguments.StartsWith(prefix))
            return false;

        string afterAmpersat = arguments[1..];
        T? known = knownList.FirstOrDefault(x => ArgumentContainsName(afterAmpersat, x.Name, config.Separators));

        // To use up the argument and not leave Guilded.NET confused
        if (known is not null)
            arguments = arguments[(1 + known.Name.Length)..].TrimStart(config.Separators);

        value = known!;

        return known is not null;
    }

    internal static bool ArgumentContainsName(string arguments, string name, char[] separators) =>
        // The display name must be within the argument
        name.Length <= arguments.Length &&
        // The beginning of the argument must be the display name (ignoring @ or #)
        arguments.StartsWith(name) &&
        // Either the display name is all the argument has or there is a separator after the name
        (name.Length == arguments.Length || separators.Contains(arguments[name.Length]));
    #endregion
}