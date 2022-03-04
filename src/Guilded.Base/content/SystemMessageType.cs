// using System.Runtime.Serialization;

// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;

// namespace Guilded.Base.Content
// {
//     /// <summary>
//     /// A type of the system message.
//     /// </summary>
//     /// <seealso cref="BaseMessage"/>
//     /// <seealso cref="Message"/>
//     /// <seealso cref="MessageType"/>
//     [JsonConverter(typeof(StringEnumConverter))]
//     public enum SystemMessageType
//     {
//         /// <summary>
//         /// Channel auto archive got enabled.
//         /// </summary>
//         [EnumMember(Value = "auto-archive-enabled")]
//         AutoArchiveEnabled,
//         /// <summary>
//         /// Channel auto archive got disabled.
//         /// </summary>
//         [EnumMember(Value = "auto-archive-disabled")]
//         AutoArchiveDisabled,
//         /// <summary>
//         /// User was added to a DM group.
//         /// </summary>
//         [EnumMember(Value = "group-dm-user-added")]
//         GroupDmUserAdded,
//         /// <summary>
//         /// User was removed from a DM group.
//         /// </summary>
//         [EnumMember(Value = "group-dm-user-removed")]
//         GroupDmUserRemoved,
//         /// <summary>
//         /// DM group was created.
//         /// </summary>
//         [EnumMember(Value = "group-dm-channel-created")]
//         GroupDmChannelCreated,
//         /// <summary>
//         /// Channel name was changed.
//         /// </summary>
//         [EnumMember(Value = "channel-renamed")]
//         ChannelRenamed,
//         /// <summary>
//         /// Channel was archived.
//         /// </summary>
//         [EnumMember(Value = "channel-archived")]
//         ChannelArchived,
//         /// <summary>
//         /// Channel was unarchived.
//         /// </summary>
//         [EnumMember(Value = "channel-restored")]
//         ChannelRestored,
//         /// <summary>
//         /// User in DMs changed their nickname.
//         /// </summary>
//         [EnumMember(Value = "dm-user-nickname-set")]
//         DmUserNicknameSet,
//         /// <summary>
//         /// User in DMs changed their nickname.
//         /// </summary>
//         [EnumMember(Value = "team-channel-created")]
//         TeamChannelCreated,
//         /// <summary>
//         /// Thread was created in response to a message.
//         /// </summary>
//         [EnumMember(Value = "thread-created")]
//         ThreadCreated,
//         /// <summary>
//         /// User was added to a thread.
//         /// </summary>
//         [EnumMember(Value = "thread-users-added")]
//         ThreadUsersAdded,
//         /// <summary>
//         /// User left a thread.
//         /// </summary>
//         [EnumMember(Value = "thread-user-left")]
//         ThreadUserLeft,
//         /// <summary>
//         /// User has started a video/stream call.
//         /// </summary>
//         [EnumMember(Value = "video-call-started")]
//         VideoCallStarted,
//         /// <summary>
//         /// User has started a normal voice call.
//         /// </summary>
//         [EnumMember(Value = "voice-call-started")]
//         VoiceCallStarted,
//         /// <summary>
//         /// User stopped streaming in a streaming channel.
//         /// </summary>
//         [EnumMember(Value = "team-stream-went-offline")]
//         TeamStreamWentOffline,
//         /// <summary>
//         /// User got invited into a voice group.
//         /// </summary>
//         [EnumMember(Value = "voice-group-invite")]
//         VoiceGroupInvite,
//         /// <summary>
//         /// User joined a team/server.
//         /// </summary>
//         [EnumMember(Value = "team-member-joined")]
//         TeamMemberJoined,
//         /// <summary>
//         /// Unknown.
//         /// </summary>
//         [EnumMember(Value = "match-created")]
//         MatchCreated,
//         /// <summary>
//         /// List item was set as completed
//         /// </summary>
//         [EnumMember(Value = "list-item-completed")]
//         ListItemCompleted,
//         /// <summary>
//         /// List item was removed from a completed item list.
//         /// </summary>
//         [EnumMember(Value = "list-item-incompleted")]
//         ListItemIncompleted,
//         /// <summary>
//         /// Order of the list item was changed.
//         /// </summary>
//         [EnumMember(Value = "list-item-moved")]
//         ListItemMoved,
//         /// <summary>
//         /// Stream started.
//         /// </summary>
//         [EnumMember(Value = "streaming-screenshare-started")]
//         StreamingScreenshareStarted
//     }
// }