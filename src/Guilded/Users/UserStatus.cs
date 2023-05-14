using Newtonsoft.Json;

namespace Guilded.Users;

/// <summary>
/// Represents the status message of a <see cref="User">user</see>.
/// </summary>
/// <seealso cref="User" />
/// <seealso cref="UserSummary" />
public class UserStatus
{
    #region Properties
    /// <summary>
    /// Gets the text contents of the <see cref="UserStatus">status</see>.
    /// </summary>
    /// <remarks>
    /// <para>The content only allows emotes and plain text. Content cannot contain more than 1 line.</para>
    /// </remarks>
    /// <value>The text contents of the <see cref="UserStatus">status</see></value>
    /// <seealso cref="UserStatus" />
    /// <seealso cref="EmoteId" />
    public string Content { get; }

    /// <summary>
    /// Gets the prefix emote of the <see cref="UserStatus">status</see>.
    /// </summary>
    /// <value>The prefix emote of the <see cref="UserStatus">status</see></value>
    /// <seealso cref="UserStatus" />
    /// <seealso cref="Content" />
    public uint? EmoteId { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="UserStatus" /> from the specified JSON properties.
    /// </summary>
    /// <param name="content">The text contents of the <see cref="UserStatus">status</see></param>
    /// <param name="emoteId">The prefix emote of the <see cref="UserStatus">status</see></param>
    /// <returns>New <see cref="UserStatus" /> JSON instance</returns>
    /// <seealso cref="UserStatus" />
    [JsonConstructor]
    public UserStatus(
        [JsonProperty(Required = Required.Always)]
        string content,

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        uint? emoteId = null
    ) =>
        (Content, EmoteId) = (content, emoteId);
    #endregion
}