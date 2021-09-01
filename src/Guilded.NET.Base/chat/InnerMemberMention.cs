using System;
using System.Drawing;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// The information about <see cref="MemberMention"/>.
    /// </summary>
    /// <seealso cref="ChannelMentionData"/>
    public class MemberMentionData : BaseMention
    {
        private static readonly Color hereColour = Color.FromArgb(0xF5C400);
        /// <summary>
        /// Information about the member(s) mentioned.
        /// </summary>
        /// <param name="isHere">Whether the mention is @here or @everyone</param>
        public MemberMentionData(bool isHere = false) =>
            (Type, Name, Matcher, Id, MentionColor, Description) = (
                isHere ? "here" : "everyone",
                Type, $"@{Type}",
                Type,
                isHere ? hereColour : Color.White,
                "Notify everyone in the channel" + (isHere ? " that is online and not idle" : "")
            );
        // /// <summary>
        // /// Information about the member(s) mentioned.
        // /// </summary>
        // /// <param name="user">The user to mention</param>
        // public MemberMentionData(BaseUser user) =>
        //     (Type, Name, Avatar, Matcher, Id, MentionColor, Nickname) = (
        //         "person",
        //         user.Username,
        //         user.ProfilePicture,
        //         "@" + user.Username.ToLower(),
        //         user.Id.ToString(),
        //         null,
        //         false
        //     );
        // /// <summary>
        // /// Information about the member(s) mentioned.
        // /// </summary>
        // /// <param name="role">The role to mention</param>
        // public MemberMentionData(TeamRole role) =>
        //     (Type, Name, Matcher, Id, MentionColor) = (
        //         "role",
        //         role.Name,
        //         "@" + role.Name.ToLower(),
        //         role.Id.ToString(),
        //         role.Color
        //     );
        // /// <summary>
        // /// Information about the member(s) mentioned.
        // /// </summary>
        // /// <param name="member">The member to mention</param>
        // /// <param name="color">The display colour of this member</param>
        // public MemberMentionData(Member member, Color? color = null) =>
        //     (Type, Name, Nickname, Avatar, Matcher, Id, MentionColor) = (
        //         "person",
        //         member.Nickname ?? member.Name,
        //         !(member.Nickname is null),
        //         member.ProfilePicture,
        //         $"@{member.Name.ToLower()} {(member.Nickname is null ? "" : "@")}{member.Nickname}",
        //         member.Id.ToString(),
        //         color ?? Color.White
        //     );
        /// <summary>
        /// Type of the mention.
        /// </summary>
        /// <value>person, here, everyone, role</value>
        public string Type
        {
            get; set;
        }
        /// <summary>
        /// ID of the mention.
        /// </summary>
        /// <value>Name</value>
        public string Id
        {
            get; set;
        }
        /// <summary>
        /// Description of the mention.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// Avatar of the user being mentioned.
        /// </summary>
        /// <value>URL</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Uri Avatar
        {
            get; set;
        }
        /// <summary>
        /// Colour of the mention.
        /// </summary>
        /// <value>Hex colour</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Color? MentionColor
        {
            get; set;
        }
        /// <summary>
        /// Whether the Mentioned has a nickname.
        /// </summary>
        /// <value>Boolean</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Nickname
        {
            get; set;
        }
    }
}