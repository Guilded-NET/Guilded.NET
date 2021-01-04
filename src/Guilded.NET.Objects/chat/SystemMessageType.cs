namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A type of the system message.
    /// </summary>
    public enum SystemMessageType {
        /// <summary>
        /// Channel auto archive got enabled.
        /// </summary>
        AutoArchiveEnabled,
        /// <summary>
        /// Channel auto archive got disabled.
        /// </summary>
        AutoArchiveDisabled,
        /// <summary>
        /// User was added to a DM group.
        /// </summary>
        GroupDMUserAdded,
        /// <summary>
        /// User was removed from a DM group.
        /// </summary>
        GroupDMUserRemoved,
        /// <summary>
        /// DM group was created.
        /// </summary>
        GroupDMChannelCreated,
        /// <summary>
        /// Channel name was changed.
        /// </summary>
        ChannelRenamed,
        /// <summary>
        /// Channel was archived.
        /// </summary>
        ChannelArchived,
        /// <summary>
        /// Channel was unarchived.
        /// </summary>
        ChannelRestored,
        /// <summary>
        /// User in DMs changed their nickname.
        /// </summary>
        DMUserNicknameSet,
        /// <summary>
        /// User in DMs changed their nickname.
        /// </summary>
        TeamChannelCreated,
        /// <summary>
        /// Thread was created in response to a message.
        /// </summary>
        ThreadCreated,
        /// <summary>
        /// User was added to a thread.
        /// </summary>
        ThreadUsersAdded,
        /// <summary>
        /// User left a thread.
        /// </summary>
        ThreadUserLeft,
        /// <summary>
        /// User has started a video/stream call.
        /// </summary>
        VideoCallStarted,
        /// <summary>
        /// User has started a normal voice call.
        /// </summary>
        VoiceCallStarted,
        /// <summary>
        /// User stopped streaming in a streaming channel.
        /// </summary>
        TeamStreamWentOffline,
        /// <summary>
        /// User got invited into a voice group.
        /// </summary>
        VoiceGroupInvite,
        /// <summary>
        /// User joined a team/server.
        /// </summary>
        TeamMemberJoined,
        /// <summary>
        /// Unknown.
        /// </summary>
        MatchCreated,
        /// <summary>
        /// List item was set as completed
        /// </summary>
        ListItemCompleted,
        /// <summary>
        /// List item was removed from a completed item list.
        /// </summary>
        ListItemIncompleted,
        /// <summary>
        /// Order of the list item was changed.
        /// </summary>
        ListItemMoved,
        /// <summary>
        /// Stream started.
        /// </summary>
        StreamingScreenshareStarted
    }
}