using System;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base
{
    using Chat;
    using Teams;
    using Users;
    using Content;
    /// <summary>
    /// Metadata of given route.
    /// </summary>
    public class Metadata : BaseObject
    {
        #region General
        /// <summary>
        /// Title of the route's metadata.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty(Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// Description of the route's metadata.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty(Required = Required.Always)]
        public string Description
        {
            get; set;
        }
        #endregion


        #region Route References
        /// <summary>
        /// Rest of the unused route parameters.
        /// </summary>
        /// <value>{"rest": "/path/of/the/route", ???}</value>
        [JsonProperty("restParamsDoNotUse")]
        public IDictionary<string, object> RestParams
        {
            get; set;
        }

        #region Team
        /// <summary>
        /// Team referenced in the route.
        /// </summary>
        /// <value>Team</value>
        public Team Team
        {
            get; set;
        }
        /// <summary>
        /// Invite referenced in the route.
        /// </summary>
        /// <value>Invite information</value>
        [JsonProperty("inviteInfo")]
        public MetadataInvite Invite
        {
            get; set;
        }
        /// <summary>
        /// Group referenced in the route.
        /// </summary>
        /// <value>Group</value>
        public Group Group
        {
            get; set;
        }
        /// <summary>
        /// Channel referenced in the route.
        /// </summary>
        /// <value>Channel | ThreadChannel</value>
        public TeamChannel Channel
        {
            get; set;
        }
        /// <summary>
        /// User's profile referenced in the route.
        /// </summary>
        /// <value>User</value>
        public User User
        {
            get; set;
        }
        #endregion

        
        #region Content
        /// <summary>
        /// Message referenced in the route.
        /// </summary>
        /// <value>Message</value>
        public Message Message
        {
            get; set;
        }
        // /// <summary>
        // /// Document referenced in the route.
        // /// </summary>
        // /// <value>Document</value>
        // [JsonProperty("doc")]
        // public GuildedDocument Document
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// Calendar event referenced in the route.
        // /// </summary>
        // /// <value>Calendar event</value>
        // public CalendarEvent Event
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// Announcement referenced in the route.
        // /// </summary>
        // /// <value>Announcement</value>
        // public Announcement Announcement
        // {
        //     get; set;
        // }
        // /// <summary>
        // /// Media referenced in the route.
        // /// </summary>
        // /// <value>Media</value>
        // public GuildedMedia Media
        // {
        //     get; set;
        // }
        /// <summary>
        /// Voice room referenced in the route.
        /// </summary>
        /// <value>Voice Room/Group</value>
        [JsonProperty("voiceGroup")]
        public VoiceRoom VoiceRoom
        {
            get; set;
        }
        /// <summary>
        /// Forum thread/post referenced in the route.
        /// </summary>
        /// <value>Forum post</value>
        public ForumThread Thread
        {
            get; set;
        }
        /// <summary>
        /// Reply of the content that was referenced in the route.
        /// </summary>
        /// <value>Reply</value>
        public Reply Reply
        {
            get; set;
        }
        #endregion

        #endregion

        
        #region Misc
        /// <summary>
        /// ID of users who are part of the metadata.
        /// </summary>
        /// <value>List of user IDs</value>
        public IList<GId> UserIds
        {
            get; set;
        }
        /// <summary>
        /// Similar URL that could be used.
        /// </summary>
        /// <value>URL path</value>
        [JsonProperty(Required = Required.Always)]
        public string CanonicalUrl
        {
            get; set;
        }
        /// <summary>
        /// Original URL used for the route.
        /// </summary>
        /// <value>URL path</value>
        [JsonProperty(Required = Required.Always)]
        public string OriginalUrl
        {
            get; set;
        }
        #endregion
    }
}