using System;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Utilities for API.
    /// </summary>
    public class GuildedUrl
    {
        /// <summary>
        /// URL for performing REST-related things on Guilded.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Api = new Uri("https://www.guilded.gg/api/v1/");
        /// <summary>
        /// URL for uploading media to Guilded.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Media = new Uri("https://media.guilded.gg/");
        /// <summary>
        /// URL for getting images uploaded on Guilded.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri ImageCdn = new Uri("https://img.guildedcdn.com/");
        /// <summary>
        /// URL for Guilded WebSockets.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Websocket = new Uri("wss://api.guilded.gg/v1/websocket");
    }
}
