using System;
using System.Threading.Tasks;
using RestSharp;

namespace Guilded.NET
{
    /// <summary>
    /// Guilded bot that was logged in with auth token.
    /// </summary>
    public partial class GuildedBotClient : BasicGuildedClient
    {
        /// <summary>
        /// Authentication token used to log into the bot in Guilded.
        /// </summary>
        /// <value>Token</value>
        protected string AuthToken
        {
            get; private set;
        }
        /// <summary>
        /// Guilded bot that was logged in with auth token.
        /// </summary>
        /// <param name="token">Authentication token used to log into the bot in Guilded</param>
        public GuildedBotClient(string token) : base()
        {
            // Checks if token isn't empty
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException($"{nameof(token)} cannot be null, full of whitespace or empty.");
            // Assign the property
            AuthToken = token;
        }
        /// <summary>
        /// Connects to Guilded using authentication token.
        /// </summary>
        /// <param name="authToken">Token to be used for authorization</param>
        public Task ConnectAsync(string authToken)
        {
            GuildedLogger.Information("Logging in");
            // Adds authentication token as a header
            AdditionalHeaders.Add("Authorization", $"Bearer {authToken}");
            Rest.AddDefaultHeaders(AdditionalHeaders);
            // Executes base
            BasicConnectAsync();
            // Invokes login event
            ConnectedEvent?.Invoke(this, EventArgs.Empty);
            //GuildedLogger.Verbose("Getting /me");
            // Gets this user and sets as .Me
            //Me = await GetThisUserAsync();
            // Tells that the client is prepared
            PreparedEvent?.Invoke(this, EventArgs.Empty);
            // Completed task
            return Task.CompletedTask;
        }
        /// <summary>
        /// Connects to Guilded using authentication token.
        /// </summary>
        public override Task ConnectAsync() =>
            ConnectAsync(AuthToken);
        /// <summary>
        /// Disconnects from Guilded.
        /// </summary>
        public override Task DisconnectAsync()
        {
            // Invoke disconnection event
            BasicDisconnect();
            GuildedLogger.Debug("Successfully disconnected");
            // Completed task
            return Task.CompletedTask;
        }
    }
}