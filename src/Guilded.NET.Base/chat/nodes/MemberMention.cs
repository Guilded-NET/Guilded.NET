using System.Drawing;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A mention of a member/the members.
    /// </summary>
    /// <seealso cref="ChannelMention"/>
    public class MemberMention : ContainerNode<TextContainer, MemberMention>
    {
        #region Properties
        /// <summary>
        /// Represents data of this mention.
        /// </summary>
        /// <value>Mention data?</value>
        [JsonIgnore]
        public MemberMentionData MentionData
        {
            get => Data.Mention;
            set => Data.Mention = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// A mention of a member/the members.
        /// </summary>
        /// <param name="data">Information about the mention</param>
        public MemberMention(MemberMentionData data) : base(NodeType.Mention, ElementType.Inline, new TextContainer($"@{data.Name}")) =>
            Data.Mention = data;
        /// <summary>
        /// A mention of a member/the members.
        /// </summary>
        /// <param name="isHere">Whether the mention is @here or @everyone</param>
        public MemberMention(bool isHere = false) : this(new MemberMentionData(isHere)) { }
        // /// <summary>
        // /// A mention of a member/the members.
        // /// </summary>
        // /// <param name="user">User to create mention of</param>
        // public MemberMention(BaseUser user) : this(new MemberMentionData(user)) { }
        // /// <summary>
        // /// A mention of a member/the members.
        // /// </summary>
        // /// <param name="role">Role to create mention of</param>
        // public MemberMention(TeamRole role) : this(new MemberMentionData(role)) { }
        // /// <summary>
        // /// A mention of a member/the members.
        // /// </summary>
        // /// <param name="member">The member to mention</param>
        // /// <param name="color">The display colour of this member</param>
        // public MemberMention(Member member, Color? color = null) : this(new MemberMentionData(member, color)) { }
        #endregion

        #region Additional
        /// <summary>
        /// Converts mention to its string equivalent.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString() =>
            $"@{MentionData.Name}";
        #endregion
    }
}