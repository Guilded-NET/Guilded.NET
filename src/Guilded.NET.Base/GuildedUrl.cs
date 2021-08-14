using System;

namespace Guilded.NET.Base
{
    /// <summary>
    /// The list of URLs for Guilded services.
    /// </summary>
    public class GuildedUrl
    {
        /// <summary>
        /// The URL for performing REST-related things on Guilded.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Api = new Uri("https://www.guilded.gg/api/v1/");
        /// <summary>
        /// The URL for image, media and webhook related things.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Media = new Uri("https://media.guilded.gg/");
        /// <summary>
        /// The URL for viewing Guilded images and videos.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri ImageCdn = new Uri("https://img.guildedcdn.com/");
        /// <summary>
        /// The URL for initiating Guilded WebSocket.
        /// </summary>
        /// <value>URL</value>
        public static readonly Uri Websocket = new Uri("wss://api.guilded.gg/v1/websocket");
    }
}
