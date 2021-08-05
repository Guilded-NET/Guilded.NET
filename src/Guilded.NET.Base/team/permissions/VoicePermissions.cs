using System;

namespace Guilded.NET.Base.Permissions
{
    /// <summary>
    /// Voice chat/channel permissions.
    /// </summary>
    [Flags]
    public enum VoicePermissions
    {
        /// <summary>
        /// Allows you to talk in voice chat
        /// </summary>
        AddVoice = 1,
        /// <summary>
        /// Allows you to listen to voice chat
        /// </summary>
        HearVoice = 2,
        /// <summary>
        /// Allows you to move members to other voice rooms
        /// </summary>
        MoveMembers = 16,
        /// <summary>
        /// Allows you to prioritize your voice when speaking in voice chat
        /// </summary>
        PrioritySpeaker = 32,
        /// <summary>
        /// Allows you to use voice activity input mode from voice chaats
        /// </summary>
        VoiceActivity = 64,
        /// <summary>
        /// Allows you to mute members in voice chats
        /// </summary>
        MuteMembers = 128,
        /// <summary>
        /// Allows you to deafen members in voice chat
        /// </summary>
        DeafenMembers = 256,
        /// <summary>
        /// Allows you to create, rename, and delete voice rooms
        /// </summary>
        ManageVoiceRooms = 512,
        /// <summary>
        /// Allows you to broadcast your voice to rooms lower in hierarchy when speaking in voice chat
        /// </summary>
        Broadcast = 1024,
        /// <summary>
        /// Allows you to direct your voice to specific users
        /// </summary>
        Whisper = 2048,
        /// <summary>
        /// Allows you to send chat messages in the voice channel
        /// </summary>
        SendMessages = 4096
    }
}