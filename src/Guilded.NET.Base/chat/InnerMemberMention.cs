using System;
using System.Drawing;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Teams;
    using Users;
    /// <summary>
    /// Information about the member(s) mentioned.
    /// </summary>
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
        /// <summary>
        /// Information about the member(s) mentioned.
        /// </summary>
        /// <param name="user">The user to mention</param>
        public MemberMentionData(BaseUser user) =>
            (Type, Name, Avatar, Matcher, Id, MentionColor, Nickname) = (
                "person",
                user.Username,
                user.ProfilePicture,
                "@" + user.Username.ToLower(),
                user.Id.ToString(),
                null,
                false
            );
        /// <summary>
        /// Information about the member(s) mentioned.
        /// </summary>
        /// <param name="role">The role to mention</param>
        public MemberMentionData(TeamRole role) =>
            (Type, Name, Matcher, Id, MentionColor) = (
                "role",
                role.Name,
                "@" + role.Name.ToLower(),
                role.Id.ToString(),
                role.Color
            );
        /// <summary>
        /// Information about the member(s) mentioned.
        /// </summary>
        /// <param name="member">The member to mention</param>
        /// <param name="color">The display colour of this member</param>
        public MemberMentionData(Member member, Color? color = null) =>
            (Type, Name, Nickname, Avatar, Matcher, Id, MentionColor) = (
                "person",
                member.Nickname ?? member.Name,
                !(member.Nickname is null),
                member.ProfilePicture,
                $"@{member.Name.ToLower()} {(member.Nickname is null ? "" : "@")}{member.Nickname}",
                member.Id.ToString(),
                color ?? Color.White
            );
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
        /// <summary>
        /// Generates user mention for DMs.
        /// </summary>
        /// <param name="user">User to mention</param>
        /// <returns>Mention data</returns>
        public static MemberMentionData Generate(BaseUser user) =>
            new MemberMentionData
            {
                Type = "person",
                Matcher = "@" + user.Username.ToLower(),
                MentionColor = null,
                Name = user.Username,
                Id = user.Id.ToString(),
                Avatar = user.ProfilePicture,
                Nickname = false
            };
        /// <summary>
        /// Generates member mention.
        /// </summary>
        /// <param name="member">User to mention</param>
        /// <param name="color">Colour of the mention</param>
        /// <returns>Mention data</returns>
        public static MemberMentionData Generate(TeamMember member, Color? color = null) =>
            new MemberMentionData
            {
                Type = "person",
                Matcher = $"@{member.Username.ToLower()} {(member.Nickname is null ? "" : "@")}{member.Nickname}",
                Name = member.Nickname ?? member.Username,
                MentionColor = color ?? Color.White,
                Id = member.Id.ToString(),
                Avatar = member.ProfilePicture,
                Nickname = !(member.Nickname is null)
            };
        /// <summary>
        /// Generates @everyone mention.
        /// </summary>
        /// <returns>Mention data</returns>
        public static MemberMentionData GenerateEveryone() =>
            new MemberMentionData
            {
                Type = "everyone",
                Matcher = "@everyone",
                MentionColor = Color.White,
                Name = "everyone",
                Id = "everyone",
                Description = "Notify everyone in the channel"
            };
        /// <summary>
        /// Generates @here mention.
        /// </summary>
        /// <returns>Mention data</returns>
        public static MemberMentionData GenerateHere() =>
            new MemberMentionData
            {
                Type = "here",
                Matcher = "@here",
                MentionColor = hereColour,
                Name = "here",
                Id = "here",
                Description = "Notify everyone in this channel that is online and not idle"
            };
        /// <summary>
        /// Generates role mention.
        /// </summary>
        /// <param name="role">Role to mention</param>
        /// <returns>Mention data</returns>
        public static MemberMentionData Generate(TeamRole role) =>
            new MemberMentionData
            {
                Type = "role",
                Matcher = "@" + role.Name.ToLower(),
                Name = role.Name,
                MentionColor = role.Color,
                Id = role.Id.ToString()
            };
    }
}