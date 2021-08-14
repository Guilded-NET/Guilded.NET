namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// The information about <see cref="ChannelMention"/> or <see cref="MemberMention"/>.
    /// </summary>
    /// <seealso cref="ChannelMentionData"/>
    /// <seealso cref="MemberMentionData"/>
    public abstract class BaseMention : ClientObject
    {
        /// <summary>
        /// Mention as a string.
        /// </summary>
        /// <value>Matcher</value>
        public string Matcher
        {
            get; set;
        }
        /// <summary>
        /// A name of a channel, role or user.
        /// </summary>
        /// <value>Name</value>
        public string Name
        {
            get; set;
        }
    }
}