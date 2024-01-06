using System.Runtime.Serialization;
using Guilded.Client;
using Guilded.Content;
using Guilded.Users;
using Guilded.Servers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.Permissions;

/// <summary>
/// Represents what a <see cref="Member">member</see> or a <see cref="Role">role</see> is capable of doing in the <see cref="Server">server</see>.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Permission
{
    #region Properties API
    /// <summary>
    /// Allows you to receive more than <see cref="AbstractGuildedClient.MessageCreated">command message creation events</see>.
    /// </summary>
    [EnumMember(Value = "CanReceiveAllSocketEvents")]
    ReceiveSocketEvents,
    #endregion

    #region Properties General
    /// <summary>
    /// Allows you to update the <see cref="Server">server's</see> settings.
    /// </summary>
    [EnumMember(Value = "CanUpdateServer")]
    ManageServer,

    /// <summary>
    /// Allows you to update lower-ranked <see cref="Role">roles</see>.
    /// </summary>
    [EnumMember(Value = "CanManageRoles")]
    ManageRoles,

    /// <summary>
    /// Allows you to directly invite <see cref="Member">members</see> to the <see cref="Server">server</see>.
    /// </summary>
    [EnumMember(Value = "CanInviteMembers")]
    CreateInvites,

    /// <summary>
    /// Allows you to kick or ban <see cref="Member">members</see> from the <see cref="Server">server</see>.
    /// </summary>
    [EnumMember(Value = "CanKickMembers")]
    RemoveMembers,

    /// <summary>
    /// Allows you to create new <see cref="Group">groups</see> and edit or delete existing ones.
    /// </summary>
    [EnumMember(Value = "CanManageGroups")]
    ManageGroups,

    /// <summary>
    /// Allows you to create new <see cref="ServerChannel">channels</see> and edit or delete existing ones.
    /// </summary>
    [EnumMember(Value = "CanManageChannels")]
    ManageChannels,

    /// <summary>
    /// Allows you to create new <see cref="Webhook">webhooks</see> and edit or delete existing ones.
    /// </summary>
    [EnumMember(Value = "CanManageWebhooks")]
    ManageWebhooks,

    /// <summary>
    /// Allows you to use @everyone and @here <see cref="Content.Mentions">mentions</see>.
    /// </summary>
    [EnumMember(Value = "CanMentionEveryone")]
    UseEveryoneMention,

    /// <summary>
    /// Allows you to access the moderator view to see all <see cref="Message.IsPrivate">private replies</see>.
    /// </summary>
    [EnumMember(Value = "CanModerateChannels")]
    GetPrivateContent,

    /// <summary>
    /// Makes <see cref="Member">member</see> exempt from any slowmode restrictions.
    /// </summary>
    [EnumMember(Value = "CanBypassSlowMode")]
    BypassSlowMode,
    #endregion

    #region Properties Recruitment
    /// <summary>
    /// Allows you to view <see cref="Server">server</see> and game applications.
    /// </summary>
    [EnumMember(Value = "CanReadApplications")]
    GetApplications,

    /// <summary>
    /// Allows you to approve <see cref="Server">server</see> and game applications.
    /// </summary>
    [EnumMember(Value = "CanApproveApplications")]
    ApproveApplications,

    /// <summary>
    /// Allows you to edit the <see cref="Server">server</see> and game applications, and toggle accepting applications.
    /// </summary>
    [EnumMember(Value = "CanEditApplicationForm")]
    UpdateApplicationForms,

    /// <summary>
    /// Allows you to indicate interest in a <see cref="User">player</see> instead of an upvote.
    /// </summary>
    [EnumMember(Value = "CanIndicateLfmInterest")]
    CreateLfmInterests,

    /// <summary>
    /// Allows you to modify the Find Player status for the <see cref="Server">server</see> listing card.
    /// </summary>
    [EnumMember(Value = "CanModifyLfmStatus")]
    ManageLfmStatus,
    #endregion

    #region Properties Announcements
    /// <summary>
    /// Allows you to view <see cref="Announcement">announcements</see>.
    /// </summary>
    [EnumMember(Value = "CanReadAnnouncements")]
    GetAnnouncements,

    /// <summary>
    /// Allows you to create and remove (only created by the <see cref="AbstractGuildedClient">client</see>) <see cref="Announcement">announcements</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateAnnouncements")]
    CreateAnnouncements,

    /// <summary>
    /// Allows you to delete <see cref="Announcement">announcements</see> by other <see cref="Member">members</see> or pin any <see cref="Announcement">announcement</see>.
    /// </summary>
    [EnumMember(Value = "CanManageAnnouncements")]
    ManageAnnouncements,
    #endregion

    #region Properties Chat
    /// <summary>
    /// Allows you to read <see cref="Message">chat messages</see>.
    /// </summary>
    [EnumMember(Value = "CanReadChats")]
    GetMessages,

    /// <summary>
    /// Allows you to send <see cref="Message">chat messages</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateChats")]
    CreateMessages,

    /// <summary>
    /// Allows you to reply to <see cref="ServerChannel.IsThread">threads</see> in the <see cref="ServerChannel">channel</see>.
    /// </summary>
    [EnumMember(Value = "CanCreatePrivateMessages")]
    CreatePrivateMessages,

    /// <summary>
    /// Allows members to send <see cref="Message.IsPrivate">private messages</see> and privately reply to <see cref="Message">messages</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateThreadMessages")]
    CreateThreadMessages,

    /// <summary>
    /// Allows you to upload images and videos to <see cref="Message">chat messages</see>.
    /// </summary>
    [EnumMember(Value = "CanUploadChatMedia")]
    CreateMessageMedia,

    /// <summary>
    /// Allows you to create <see cref="ServerChannel.IsThread">threads</see> in the channel.
    /// </summary>
    [EnumMember(Value = "CanCreateThreads")]
    CreateThreads,

    /// <summary>
    /// Allows you to delete <see cref="Message">chat messages</see> by other <see cref="Member">members</see> or pin any <see cref="Message">message</see>.
    /// </summary>
    [EnumMember(Value = "CanManageChats")]
    ManageMessages,

    /// <summary>
    /// Allow you to archive, restore and delete <see cref="ServerChannel.IsThread">threads</see>.
    /// </summary>
    [EnumMember(Value = "CanManageThreads")]
    ManageThreads,
    #endregion

    #region Properties Calendar
    /// <summary>
    /// Allows you to view <see cref="CalendarEvent">events</see>.
    /// </summary>
    [EnumMember(Value = "CanReadEvents")]
    GetEvents,

    /// <summary>
    /// Allows you to create <see cref="CalendarEvent">events</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateEvents")]
    CreateEvents,

    /// <summary>
    /// Allows you to update <see cref="CalendarEvent">events</see> created by others and move them to other <see cref="CalendarChannel">channels</see>.
    /// </summary>
    [EnumMember(Value = "CanEditEvents")]
    UpdateEvents,

    /// <summary>
    /// Allows you to remove <see cref="CalendarEvent">events</see> created by others.
    /// </summary>
    [EnumMember(Value = "CanDeleteEvents")]
    DeleteEvents,

    /// <summary>
    /// Allows you to edit the <see cref="CalendarEventRsvp">RSVP status</see> for <see cref="Member">members</see> in an <see cref="CalendarEvent">event</see>.
    /// </summary>
    [EnumMember(Value = "CanEditEventRsvps")]
    UpdateEventRsvps,
    #endregion

    #region Properties Forums
    /// <summary>
    /// Allows you to read <see cref="Topic">forum topics</see>.
    /// </summary>
    [EnumMember(Value = "CanReadForums")]
    GetTopics,

    /// <summary>
    /// Allows you to create <see cref="Topic">forum topics</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateTopics")]
    CreateTopics,

    /// <summary>
    /// Allows you to create <see cref="TopicComment">forum topic replies</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateTopicReplies")]
    CreateTopicComments,

    /// <summary>
    /// Allows you to remove <see cref="Topic">forum topics</see> and <see cref="TopicComment">replies</see> created by others, or move them to other <see cref="ForumChannel">channels</see>.
    /// </summary>
    [EnumMember(Value = "CanDeleteTopics")]
    ManageTopics,

    /// <summary>
    /// Allows you to sticky a <see cref="Topic">forum topic</see>.
    /// </summary>
    [EnumMember(Value = "CanStickyTopics")]
    PinTopics,

    /// <summary>
    /// Allows you to lock a <see cref="Topic">forum topic</see>.
    /// </summary>
    [EnumMember(Value = "CanLockTopics")]
    LockTopics,
    #endregion

    #region Properties Docs
    /// <summary>
    /// Allows you to view <see cref="Doc">documents</see>.
    /// </summary>
    [EnumMember(Value = "CanReadDocs")]
    GetDocs,

    /// <summary>
    /// Allows you to create <see cref="Doc">documents</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateDocs")]
    CreateDocs,

    /// <summary>
    /// Allows you to update <see cref="Doc">documents</see> created by others and move them to other <see cref="DocChannel">channels</see>.
    /// </summary>
    [EnumMember(Value = "CanEditDocs")]
    UpdateDocs,

    /// <summary>
    /// Allows you to remove <see cref="Doc">documents</see> created by others.
    /// </summary>
    [EnumMember(Value = "CanDeleteDocs")]
    DeleteDocs,
    #endregion

    #region Properties Media
    /// <summary>
    /// Allows you to see media.
    /// </summary>
    [EnumMember(Value = "CanReadMedia")]
    GetMedia,

    /// <summary>
    /// Allows you to create media.
    /// </summary>
    [EnumMember(Value = "CanAddMedia")]
    CreateMedia,

    /// <summary>
    /// Allows you to edit media created by others and move media items to other <see cref="MediaChannel">channels</see>.
    /// </summary>
    [EnumMember(Value = "CanEditMedia")]
    UpdateMedia,

    /// <summary>
    /// Allows you to remove media created by others.
    /// </summary>
    [EnumMember(Value = "CanDeleteMedia")]
    DeleteMedia,
    #endregion

    #region Properties Voice
    /// <summary>
    /// Allows you to listen to <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanListenVoice")]
    GetVoice,

    /// <summary>
    /// Allows you to talk in voice chat.
    /// </summary>
    [EnumMember(Value = "CanAddVoice")]
    AddVoice,

    /// <summary>
    /// Allows you to create, rename, and delete voice rooms.
    /// </summary>
    [EnumMember(Value = "CanManageVoiceGroups")]
    ManageVoiceGroups,

    /// <summary>
    /// Allows you to move <see cref="Member">members</see> to other voice rooms.
    /// </summary>
    [EnumMember(Value = "CanAssignVoiceGroup")]
    ManageVoiceGroupMembers,

    /// <summary>
    /// Allows you to remove <see cref="Member">members</see> from voice rooms.
    /// </summary>
    [EnumMember(Value = "CanDisconnectUsers")]
    RemoveVoiceGroupMembers,

    /// <summary>
    /// Allows you to broadcast your voice to voice rooms lower in the hierarchy when speaking in <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanBroadcastVoice")]
    UseVoiceBroadcast,

    /// <summary>
    /// Allows you to direct your voice to specific <see cref="Member">members</see>.
    /// </summary>
    [EnumMember(Value = "CanDirectVoice")]
    UseVoiceWhisper,

    /// <summary>
    /// Allows you to prioritize your voice when speaking in <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanPrioritizeVoice")]
    UseVoicePrioritization,

    /// <summary>
    /// Allows you to use voice activity input mode for <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanUseVoiceActivity")]
    UseVoiceActivity,

    /// <summary>
    /// Allows you to mute <see cref="Member">members</see> in <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanMuteMembers")]
    MuteVoiceMembers,

    /// <summary>
    /// Allows you to deafen <see cref="Member">members</see> in <see cref="VoiceChannel">voice chat</see>.
    /// </summary>
    [EnumMember(Value = "CanDeafenMembers")]
    DeafenVoiceMembers,

    /// <summary>
    /// Allows you to send <see cref="Message">chat messages</see> in the <see cref="VoiceChannel">voice channel</see>.
    /// </summary>
    [EnumMember(Value = "CanSendVoiceMessages")]
    CreateVoiceMessages,
    #endregion

    #region Properties Matchmaking
    /// <summary>
    /// Allows you to create matchmaking scrims.
    /// </summary>
    [EnumMember(Value = "CanCreateScrims")]
    CreateScrims,

    /// <summary>
    /// Allows you to use the <see cref="Server">server</see> to create and manage tournaments.
    /// </summary>
    [EnumMember(Value = "CanManageTournaments")]
    ManageTournaments,

    /// <summary>
    /// Allows you to register the <see cref="Server">server</see> for tournaments.
    /// </summary>
    [EnumMember(Value = "CanRegisterForTournaments")]
    UseTournaments,
    #endregion

    #region Properties Customization
    /// <summary>
    /// Allows the creation and management of <see cref="Emote">server emoji</see>.
    /// </summary>
    [EnumMember(Value = "CanManageEmotes")]
    ManageEmotes,

    /// <summary>
    /// Allows you to change <see cref="Member.Nickname">nicknames</see> of the <see cref="AbstractGuildedClient">client bot</see>. 
    /// </summary>
    [EnumMember(Value = "CanChangeNickname")]
    ManageOwnNicknames,

    /// <summary>
    /// Allows you to change <see cref="Member.Nickname">nicknames</see> of other <see cref="Member">members</see>. 
    /// </summary>
    [EnumMember(Value = "CanManageNicknames")]
    ManageNicknames,
    #endregion

    #region Properties Forms
    /// <summary>
    /// Allows you to view all form responses.
    /// </summary>
    [EnumMember(Value = "CanViewFormResponses")]
    GetFormResponses,

    /// <summary>
    /// Allows you to view all poll responses.
    /// </summary>
    [EnumMember(Value = "CanViewPollResponses")]
    GetPollResponses,
    #endregion

    #region Properties List Items
    /// <summary>
    /// Allows you to view <see cref="Item">list items</see>.
    /// </summary>
    [EnumMember(Value = "CanReadListItems")]
    GetItems,

    /// <summary>
    /// Allows you to create <see cref="Item">list items</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateListItems")]
    CreateItems,

    /// <summary>
    /// Allows you to update <see cref="ItemBase{T}.Message">list item messages</see> created by others and move <see cref="Item">list items</see> to other channels.
    /// </summary>
    [EnumMember(Value = "CanUpdateListItems")]
    UpdateItems,

    /// <summary>
    /// Allows you to remove <see cref="Item">list items</see> created by others.
    /// </summary>
    [EnumMember(Value = "CanDeleteListItems")]
    DeleteItems,

    /// <summary>
    /// Allows you to complete <see cref="Item">list items</see> created by others.
    /// </summary>
    [EnumMember(Value = "CanCompleteListItems")]
    CompleteItems,

    /// <summary>
    /// Allows you to reorder <see cref="Item">list items</see>.
    /// </summary>
    [EnumMember(Value = "CanReorderListItems")]
    UpdateItemOrders,
    #endregion

    #region Properties Brackets
    /// <summary>
    /// Allows you to view tournament brackets.
    /// </summary>
    [EnumMember(Value = "CanViewBracket")]
    GetBrackets,

    /// <summary>
    /// Allows you to report match scores on behalf of your <see cref="Server">server</see>.
    /// </summary>
    [EnumMember(Value = "CanReportScores")]
    AddScoreReports,
    #endregion

    #region Properties Scheduling
    /// <summary>
    /// Allows you to view <see cref="Member">server member's</see> schedules.
    /// </summary>
    [EnumMember(Value = "CanReadSchedules")]
    GetSchedules,

    /// <summary>
    /// Allows you to let your <see cref="Server">server</see> know your available schedule.
    /// </summary>
    [EnumMember(Value = "CanCreateSchedule")]
    CreateSchedules,

    /// <summary>
    /// Allows you to remove schedules created by others.
    /// </summary>
    [EnumMember(Value = "CanDeleteSchedule")]
    DeleteSchedules,
    #endregion

    #region Properties Bots
    /// <summary>
    /// Allows you to create and edit bots for automated workflows.
    /// </summary>
    /// <remarks>
    /// <note>This permission will allow its <see cref="Member">members</see> to assign flows or implement actions for bots that may exceed their own <see cref="Permission">permissions</see>. Exercise caution when granting this permission.</note>
    /// </remarks>
    [EnumMember(Value = "CanManageBots")]
    ManageBots,
    #endregion

    #region Properties XP
    /// <summary>
    /// Allows you to manage XP on <see cref="Member">server members</see>.
    /// </summary>
    [EnumMember(Value = "CanManageServerXp")]
    ManageXp,
    #endregion

    #region Properties Scheduling
    /// <summary>
    /// Allows you to view streams.
    /// </summary>
    [EnumMember(Value = "CanReadStreams")]
    GetStreams,

    /// <summary>
    /// Allows you to listen to voice chat in the <see cref="StreamChannel">stream channel</see>.
    /// </summary>
    [EnumMember(Value = "CanJoinStreamVoice")]
    UseStreamVoice,

    /// <summary>
    /// Allows you to add a stream and also talk in the <see cref="StreamChannel">stream channel</see>.
    /// </summary>
    [EnumMember(Value = "CanCreateStreams")]
    AddStream,

    /// <summary>
    /// Allows you to to send <see cref="Message">messages</see> in the <see cref="StreamChannel">stream channel</see>.
    /// </summary>
    [EnumMember(Value = "CanSendStreamMessages")]
    CreateStreamMessages,

    /// <summary>
    /// Allows you to talk in the <see cref="StreamChannel">stream channel</see>.
    /// </summary>
    [EnumMember(Value = "CanAddStreamVoice")]
    AddStreamVoice,

    /// <summary>
    /// Allows you to use voice activity input mode for talking in <see cref="StreamChannel">stream channels</see>.
    /// </summary>
    [EnumMember(Value = "CanUseVoiceActivityInStream")]
    UseStreamVoiceActivity,
    #endregion
}