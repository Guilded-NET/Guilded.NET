using System;

namespace Guilded.Base.Permissions;

/// <summary>
/// Represents channel permissions related to voice and voice rooms.
/// </summary>
/// <seealso cref="AnnouncementPermissions" />
/// <seealso cref="BotPermissions" />
/// <seealso cref="BracketPermissions" />
/// <seealso cref="CalendarPermissions" />
/// <seealso cref="ChatPermissions" />
/// <seealso cref="CustomPermissions" />
/// <seealso cref="DocPermissions" />
/// <seealso cref="FormPermissions" />
/// <seealso cref="ForumPermissions" />
/// <seealso cref="GeneralPermissions" />
/// <seealso cref="ListPermissions" />
/// <seealso cref="MatchmakingPermissions" />
/// <seealso cref="MediaPermissions" />
/// <seealso cref="RecruitmentPermissions" />
/// <seealso cref="SchedulingPermissions" />
/// <seealso cref="StreamPermissions" />
/// <seealso cref="XpPermissions" />
[Flags]
public enum VoicePermissions
{
    /// <summary>
    /// No given permissions.
    /// </summary>
    None = 0,

    /// <summary>
    /// Allows you to talk in voice chat
    /// </summary>
    AddVoice = 1,

    /// <summary>
    /// Allows you to listen to voice chat
    /// </summary>
    AttendVoice = 2,

    /// <summary>
    /// Allows you to move members to other voice rooms
    /// </summary>
    MoveMember = 16,

    /// <summary>
    /// Allows you to prioritize your voice when speaking in voice chat
    /// </summary>
    UsePrioritySpeaker = 32,

    /// <summary>
    /// Allows you to use voice activity input mode from voice chaats
    /// </summary>
    UseVoiceActivity = 64,

    /// <summary>
    /// Allows you to mute members in voice chats
    /// </summary>
    MuteMember = 128,

    /// <summary>
    /// Allows you to deafen members in voice chat
    /// </summary>
    DeafenMembers = 256,

    /// <summary>
    /// Allows you to create, rename, and delete voice rooms
    /// </summary>
    ManageVoiceRoom = 512,

    /// <summary>
    /// Allows you to broadcast your voice to rooms lower in hierarchy when speaking in voice chat
    /// </summary>
    UseBroadcast = 1024,

    /// <summary>
    /// Allows you to direct your voice to specific users
    /// </summary>
    UseWhisper = 2048,

    /// <summary>
    /// Allows you to send chat messages in the voice channel
    /// </summary>
    CreateMessage = 4096,

    #region Methods
    /// <summary>
    /// All of the permissions combined.
    /// </summary>
    All = AddVoice | AttendVoice | MoveMember | UsePrioritySpeaker | UseVoiceActivity |
            MuteMember | DeafenMembers | ManageVoiceRoom | UseBroadcast | UseWhisper | CreateMessage,

    /// <summary>
    /// All of the manage permissions combined.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="MoveMember" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="MuteMember" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="DeafenMembers" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ManageVoiceRoom" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Manage = MoveMember | MuteMember | DeafenMembers | ManageVoiceRoom,

    /// <summary>
    /// A simple permission combination allowing writing permissions and reading permissions.
    /// </summary>
    /// <remarks>
    /// <para>Sets these permissions:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="AddVoice" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="AttendVoice" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="UseVoiceActivity" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="UseWhisper" /></description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="CreateMessage" /></description>
    ///     </item>
    /// </list>
    /// </remarks>
    Basic = AddVoice | AttendVoice | UseVoiceActivity | UseWhisper | CreateMessage
    #endregion
}