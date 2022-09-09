using System;
using Guilded.Base.Client;

namespace Guilded.Commands;

/// <summary>
/// Represents the module that adds <see cref="CommandAttribute">commands</see> to <see cref="BaseGuildedClient">Guilded clients</see>.
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
    /// <value>Argument separator characters</value>
    public static readonly char[] DefaultSeparators = new char[] { ' ', '\t', '\n' };

    /// <summary>
    /// The default splitting options for command arguments.
    /// </summary>
    /// <remarks>
    /// <para>By default, it uses <see cref="StringSplitOptions.RemoveEmptyEntries" />.</para>
    /// </remarks>
    /// <value>Split options</value>
    public const StringSplitOptions DefaultSplitOptions = StringSplitOptions.RemoveEmptyEntries;

    /// <summary>
    /// The prefix that will be used by default if not specified.
    /// </summary>
    /// <remarks>
    /// <para>By default, it's <c>/</c>.</para>
    /// </remarks>
    /// <value>Prefix</value>
    public const string DefaultPrefix = "/";
    #endregion

    #region Properties
    /// <summary>
    /// Gets the piece of text with which commands need to start with.
    /// </summary>
    /// <value>Prefix</value>
    public string Prefix { get; set; }

    /// <summary>
    /// Gets the characters that separate command arguments.
    /// </summary>
    /// <value>Separator characters</value>
    public char[] Separators { get; set; }

    /// <summary>
    /// Gets the splitting options that will be used while splitting command arguments.
    /// </summary>
    /// <value>Splitting options</value>
    public StringSplitOptions SplitOptions { get; set; }
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