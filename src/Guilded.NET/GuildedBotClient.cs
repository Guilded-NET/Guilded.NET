using System;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    /// <summary>
    /// A client type for Guilded bots.
    /// </summary>
    /// <remarks>
    /// <para>Use this to initiate and log into Guilded bot.</para>
    /// <para>If you want to connect, set <see cref="AuthToken"/> and then use <see cref="ConnectAsync()"/>.</para>
    /// <para>You can also use <see cref="ConnectAsync(string)"/>, which doesn't require <see cref="AuthToken"/> set.</para>
    /// </remarks>
    /// <seealso cref="AbstractGuildedClient"/>
    /// <seealso cref="Base.BaseGuildedClient"/>
    /// <seealso cref="ConnectAsync()"/>
    /// <seealso cref="ConnectAsync(string)"/>
    public class GuildedBotClient : AbstractGuildedClient
    {
        /// <summary>
        /// An authentication token used to log into a bot in Guilded.
        /// </summary>
        /// <value>Token</value>
        protected string AuthToken
        {
            get;
        }
        /// <summary>
        /// Creates a new <see cref="GuildedBotClient"/> instance without authentication token.
        /// </summary>
        /// <remarks>
        /// <para>This creates a new client and only initiates it. It does not connect to Guilded.</para>
        /// <para>If you want to connect to Guilded, use <see cref="ConnectAsync(string)"/> with bot's authentication token.</para>
        /// </remarks>
        public GuildedBotClient() { }
        /// <summary>
        /// Creates a new <see cref="GuildedBotClient"/> instance with given <paramref name="authToken"/>.
        /// </summary>
        /// <remarks>
        /// <para>This creates a new client and only initiates it. It does not connect to Guilded.</para>
        /// <para>If you want to connect, set <see cref="AuthToken"/> and then use <see cref="ConnectAsync()"/>.</para>
        /// <para>You can also use <see cref="ConnectAsync(string)"/>, which doesn't require <see cref="AuthToken"/> set.</para>
        /// </remarks>
        /// <param name="authToken">Authentication token used to log into the bot in Guilded</param>
        /// <exception cref="ArgumentException">When passed argument <paramref name="authToken"/> is null, empty or whitespace</exception>
        public GuildedBotClient(string authToken)
        {
            // Make sure correct token is passed
            if (string.IsNullOrWhiteSpace(authToken))
                throw new ArgumentException($"{nameof(authToken)} cannot be null, full of whitespace or empty.");

            AuthToken = authToken;
        }
        /// <summary>
        /// Connects to Guilded bot using <paramref name="authToken"/>.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new connection to Guilded using argument <paramref name="authToken"/>. This does not use <see cref="AuthToken"/>.</para>
        /// <para>To disconnect from Guilded, use <see cref="AbstractGuildedClient.DisconnectAsync"/></para>
        /// </remarks>
        /// <param name="authToken">Token to be used for authorization</param>
        /// <exception cref="ArgumentException">When passed argument <paramref name="authToken"/> is null, empty or whitespace</exception>
        public async Task ConnectAsync(string authToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                throw new ArgumentException($"{nameof(authToken)} cannot be null, full of whitespace or empty.");
            // Give authentication token to Guilded
            AdditionalHeaders.Add("Authorization", $"Bearer {authToken}");
            Rest.AddDefaultHeaders(AdditionalHeaders);

            await base.ConnectAsync().ConfigureAwait(false);

            ConnectedEvent?.Invoke(this, EventArgs.Empty);

            // Gets all of the required info
            //Me = await GetThisUserAsync();
            PreparedEvent?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Connects to Guilded using <see cref="AuthToken"/>.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new connection to Guilded using set property <see cref="AuthToken"/>.</para>
        /// <para>To disconnect from Guilded, use <see cref="AbstractGuildedClient.DisconnectAsync"/></para>
        /// </remarks>
        public override Task ConnectAsync() =>
            ConnectAsync(AuthToken);
    }
}