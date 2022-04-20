using System;

namespace Guilded.Base;

/// <summary>
/// Defines a list of URLs to Guilded services.
/// </summary>
public static class GuildedUrl
{
    /// <summary>
    /// The base URL for Guilded's API requests.
    /// </summary>
    /// <value>API URL</value>
    public static readonly Uri Api = new("https://www.guilded.gg/api/v1/");
    /// <summary>
    /// The base URL for Guilded's media API.
    /// </summary>
    /// <remarks>
    /// <para>Can be used for executing webhooks.</para>
    /// </remarks>
    /// <value>URL</value>
    public static readonly Uri Media = new("https://media.guilded.gg/");
    /// <summary>
    /// The URL for to upload Guilded's media from files.
    /// </summary>
    /// <value>API URL</value>
    public static readonly Uri MediaFileUpload = new("https://media.guilded.gg/media/upload?dynamicMediaTypeId=ContentMedia");
    /// <summary>
    /// The URL to upload Guilded's media from URLs.
    /// </summary>
    /// <value>API URL</value>
    public static readonly Uri MediaUrlUpload = new("https://media.guilded.gg/media/upload");
    /// <summary>
    /// The URL to Guilded's image CDN.
    /// </summary>
    /// <value>URL</value>
    public static readonly Uri ImageCdn = new("https://img.guildedcdn.com/");
    /// <summary>
    /// The URL to Guilded's WebSockets.
    /// </summary>
    /// <value>API URL</value>
    public static readonly Uri Websocket = new("wss://api.guilded.gg/v1/websocket");
}