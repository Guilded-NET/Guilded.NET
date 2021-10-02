using System;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Defines a list of URLs to Guilded services.
    /// </summary>
    public static class GuildedUrl
    {
        /// <summary>
        /// The URL to Guilded's API.
        /// </summary>
        /// <value>API URL</value>
        public static readonly Uri Api = new Uri("https://www.guilded.gg/api/v1/");
        /// <summary>
        /// The URL to Guilded's media services.
        /// </summary>
        /// <remarks>
        /// <para>Provides the URL to Guilded's media services that allow image &amp; video uploads, as well as webhook-related functions.</para>
        /// </remarks>
        /// <value>Media URL</value>
        public static readonly Uri Media = new Uri("https://media.guilded.gg/");
        /// <summary>
        /// The URL to upload Guilded's media.
        /// </summary>
        /// <remarks>
        /// <para>The URL that will be used to upload videos, images and other files.</para>
        /// </remarks>
        /// <value>Media upload URL</value>
        public static readonly Uri MediaFileUpload = new Uri("https://media.guilded.gg/media/upload?dynamicMediaTypeId=ContentMedia");
        /// <summary>
        /// The URL to upload Guilded's media.
        /// </summary>
        /// <remarks>
        /// <para>The URL that will be used to upload URL links.</para>
        /// </remarks>
        /// <value>Media upload URL</value>
        public static readonly Uri MediaUrlUpload = new Uri("https://media.guilded.gg/media/upload");
        /// <summary>
        /// The URL to Guilded's image CDN.
        /// </summary>
        /// <remarks>
        /// <para>Provides the URL to Guilded's image CDN that hosts all of the images on Guilded.</para>
        /// </remarks>
        /// <value>Image CDN URL</value>
        public static readonly Uri ImageCdn = new Uri("https://img.guildedcdn.com/");
        /// <summary>
        /// The URL to Guilded's websocket.
        /// </summary>
        /// <remarks>
        /// <para>Provides the URL to Guilded's API WebSocket.</para>
        /// </remarks>
        /// <value>WebSocket URL</value>
        public static readonly Uri Websocket = new Uri("wss://api.guilded.gg/v1/websocket");
    }
}
