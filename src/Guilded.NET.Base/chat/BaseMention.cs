namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A data of a channel or user mention.
    /// </summary>
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