namespace Guilded.NET {
    using System;
    using Objects;
    using Objects.Events;
    /// <summary>
    /// Delegate for getting prefix based on the author, team and message creation event.
    /// </summary>
    /// <param name="author">User who posted the message</param>
    /// <param name="team">Team this message was posted in</param>
    /// <param name="messageCreated">Message creation event</param>
    /// <returns></returns>
    public delegate string PrefixInfo(MessageCreatedEvent messageCreated);
    /// <summary>
    /// Config of Guilded client.
    /// </summary>
    public class GuildedClientConfig {
        /// <summary>
        /// Always returns given prefix. Use this if you don't need any server custom prefixes.
        /// </summary>
        /// <param name="prefix">Default prefix</param>
        /// <returns>PrefixInfo function</returns>
        public static PrefixInfo BasicPrefix(string prefix) => e => prefix;
        /// <summary>
        /// Gets a prefix for the bot based on team, group, channel and user which invoked the command.
        /// </summary>
        /// <value>Bot prefix delegate</value>
        public PrefixInfo Prefix {
            get; set;
        }
        /// <summary>
        /// Owner of this bot.
        /// </summary>
        /// <value>Owner ID</value>
        public GId OwnerId {
            get; set;
        }
        /// <summary>
        /// Enables commands. Disable this if you want to customize how commands are being handled.
        /// </summary>
        /// <value>Enabled commands</value>
        public bool EnableCommands {
            get; set;
        }
        /// <summary>
        /// Whether or not it should ignore the commands used by itself. True by default.
        /// </summary>
        /// <value>Ignore commands invoked by itself</value>
        public bool IgnoreOwnCommands {
            get; set;
        }
        /// <summary>
        /// Whether or not it should disable disposing the bot when CTRL + C is pressed.
        /// </summary>
        /// <value>Disable CTRL + C disposing</value>
        public bool DisableCancelKeyPress {
            get; set;
        }
        /// <summary>
        /// List of strings it should split arguments by.
        /// </summary>
        /// <value>Argument split array</value>
        public string[] CommandArgumentSplit {
            get; set;
        }
        /// <summary>
        /// Options of argument splitting.
        /// </summary>
        /// <value>Argument split options</value>
        public StringSplitOptions SplitOptions {
            get; set;
        }
        /// <summary>
        /// Config of Guilded client.
        /// </summary>
        /// <param name="prefix">A function for getting a prefix</param>
        /// <param name="owner">Owner of this bot</param>
        /// <param name="moderators">IDs of the bot moderators</param>
        public GuildedClientConfig(PrefixInfo prefix = null, GId owner = null, bool enableCommands = true) =>
            (Prefix, OwnerId, EnableCommands, IgnoreOwnCommands, DisableCancelKeyPress, CommandArgumentSplit, SplitOptions) = (prefix, owner, enableCommands, true, false, new string[] {" ", "\t"}, StringSplitOptions.None);
    }
}