using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Guilded.NET {
    using System.Data;
    using API;
    using Objects;
    /// <summary>
    /// Logged-in user in Guilded.
    /// </summary>
    public partial class GuildedUserClient: BasicGuildedClient {
        string pass;
        /// <summary>
        /// User's email.
        /// </summary>
        /// <value>Email</value>
        public string Email {
            get;
            internal set;
        }
        /// <summary>
        /// User's password.
        /// </summary>
        /// <value>Password</value>
        protected string Password {
            get => pass;
            set => SetPassword(value);
        }
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        /// <param name="config">Configuration of the bot</param>
        public GuildedUserClient(string email, string password, GuildedClientConfig config): base(config) {
            // Checks if email and password are right
            if(string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"{nameof(email)} cannot be null, full of whitespace or empty.");
            else if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException($"{nameof(password)} cannot be null, full of whitespace or empty.");
            // Assign the property & the field
            Email = email;
            pass = password;
        }
        /// <param name="email">Email of the user</param>
        /// <param name="password">Password of the user</param>
        public GuildedUserClient(string email, string password): this(email, password, new GuildedClientConfig(null)) {}
        /// <summary>
        /// Connects to Guilded using password and email.
        /// </summary>
        /// <returns>Task</returns>
        public override async Task<IRestResponse<object>> ConnectAsync() {
            // Creates login details to send to Guilded
            var login = new { email = Email, password = Password };
            // Sends login details to Guilded
            var executed = await ExecuteRequest(Endpoint.LOGIN, false, new JsonBody(login));
            // Set login cookies
            SetCookies(executed.Cookies);
#pragma warning disable 0618
            // Executes base
            await BasicConnectAsync();
#pragma warning restore 0618
            // If the content is empty
            if(string.IsNullOrWhiteSpace(executed.Content)) throw new Exception("Unknown error occurred. Login gave empty string.");
            // Parses the login
            JToken token = JToken.Parse(executed.Content);
            // If it's not object
            if(token.Type != JTokenType.Object) throw new DataException($"Expected object from Guilded login, got {token.Type} instead. Full JSON ahead:\n{token}");
            // Gets it as object
            JObject obj = (JObject)token;
            // If it doesn't contain property `user`
            if(!obj.ContainsKey("user") && obj.ContainsKey("code") && obj.ContainsKey("message"))
                throw new GuildedException {
                    Code = obj["code"].Value<string>(),
                    ErrorMessage = obj["message"].Value<string>()
                };
            // Turn it into current user
            CurrentUser = obj["user"].ToObject<User>(GuildedSerializer);
            // Invokes login event
            ConnectedEvent?.Invoke(this, EventArgs.Empty);
            return executed;
        }
        /// <summary>
        /// Disconnects from Guilded.
        /// </summary>
        /// <returns>Task</returns>
        public override async Task<IRestResponse<object>> DisconnectAsync() {
            // Disconnect
            var executed = await ExecuteRequest(Endpoint.LOGOUT);
            // Invoke disconnection event
            await BasicDisconnectAsync();
            // Return response
            return executed;
        }
        /// <summary>
        /// Sets a new password for this user.
        /// </summary>
        /// <param name="password">New password</param>
        /// <returns>Async task</returns>
        public async Task SetPasswordAsync(string password) {
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