using Newtonsoft.Json;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// 
    /// </summary>
    public class TeamSubscription: ClientObject {
        /// <summary>
        /// Type of the subscription in this server.
        /// </summary>
        /// <value>Subscription type</value>
        [JsonProperty("type", Required = Required.Always)]
        public SubscriptionType Type {
            get; set;
        }
        /// <summary>
        /// How many months subscription will last.
        /// </summary>
        /// <value>Months</value>
        [JsonProperty("monthsRemaining", Required = Required.Always)]
        public uint MonthsRemaining {
            get; set;
        }
        /// <summary>
        /// Team this subscription is in.
        /// </summary>
        /// <value>Team ID</value>
        [JsonProperty("teamId", Required = Required.Always)]
        public GId TeamId {
            get; set;
        }
    }
}