using System;
using System.Threading.Tasks;

namespace Guilded.NET
{
    using API;
    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public partial class GuildedUserClient : BasicGuildedClient
    {
        string pass;
        /// <summary>
        /// User's email.
        /// </summary>
        /// <value>Email</value>
        public string Email
        {
            get;
            internal set;
        }
        /// <summary>
        /// User's password.
        /// </summary>
        /// <value>Password</value>
        protected string Password
        {
            get => pass;
            set => SetPassword(value);
        }
        /// <summary>
        /// Logged-in user in Guilded.
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        /// <param name="config">Configuration of the bot</param>
        public GuildedUserClient(string email, string password, GuildedClientConfig config) : base(config)
        {
            // Checks if email and password are right
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"{nameof(email)} cannot be null, full of whitespace or empty.");
            else if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException($"{nameof(password)} cannot be null, full of whitespace or empty.");
            // Assign the property & the field
            (Email, pass) = (email, password);
        }
        /// <summary>
        /// Logged-in user in Guilded.
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        public GuildedUserClient(string email, string password) : this(email, password, new GuildedClientConfig()) { }
        /// <summary>
        /// Connects to Guilded using password and email.
        /// </summary>
        public override async Task ConnectAsync()
        {
            // Sends login details to Guilded
            var executed = await ExecuteRequest(Endpoint.LOGIN, false, new JsonBody(new { email = Email, password = Password }));
            // Set login cookies
            LoginCookies = executed.Cookies;
            // Executes base
            await BasicConnectAsync();
            // Invokes login event
            ConnectedEvent?.Invoke(this, EventArgs.Empty);
            // Gets this user and sets as .Me
            Me = await GetThisUserAsync();
            // Tells that the client is prepared
            PreparedEvent?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Disconnects from Guilded.
        /// </summary>
        public override async Task DisconnectAsync()
        {
            // Disconnect
            await ExecuteRequest(Endpoint.LOGOUT);
            // Invoke disconnection event
            await BasicDisconnectAsync();
        }
        /// <summary>
        /// Sets a new password for this user.
        /// </summary>
        /// <param name="password">New password</param>
        /// <returns>Async task</returns>
        public async Task SetPasswordAsync(string password)
        {
            // Changes the password
            await ExecuteRequest(Endpoint.PASSWORD, new JsonBody(new { newPassword = password }));
            // Sets pass field, because password was changed
            pass = password;
        }
        /// <summary>
        /// Sets a new password for this user. Sync version of <see cref="SetPasswordAsync"/>.
        /// </summary>
        /// <param name="password">New password</param>
        public void SetPassword(string password) =>
            SetPasswordAsync(password).GetAwaiter().GetResult();
    }
}