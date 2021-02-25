using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    using Teams;
    /// <summary>
    /// Role, user, @everyone or @here mention.
    /// </summary>
    public class Mention: ContainerNode<IMessageObject> {
        /// <summary>
        /// Role, user, @everyone or @here mention.
        /// </summary>
        public Mention() =>
            Type = NodeType.Mention;
        /// <summary>
        /// Gets mention data.
        /// </summary>
        /// <value>Data of the mention</value>
        [JsonIgnore]
        public MentionData MentionData {
            get => GetDataProperty<MentionData>("mention");
        }
        /// <summary>
        /// Turns mention to string.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString() =>
            $"@{MentionData?.Name}";
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="mention">Mention data</param>
        /// <returns>Mention</returns>
        public static Mention Generate(MentionData mention) =>
            new Mention {
                Data = JObject.FromObject(new { mention }),
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText($"@{mention.Name}")
                }
            };
        /// <summary>
        /// Generates user mention.
        /// </summary>
        /// <param name="user">User to mention</param>
        /// <returns>User mention</returns>
        public static Mention Generate(User user) => Generate(MentionData.Generate(user));
        /// <summary>
        /// Generates role mention.
        /// </summary>
        /// <param name="role">Role to mention</param>
        /// <returns>Role mention</returns>
        public static Mention Generate(TeamRole role) => Generate(MentionData.Generate(role));
        /// <summary>
        /// Generates @everyone mention.
        /// </summary>
        /// <returns>Everyone mention</returns>
        public static Mention GenerateEveryone() => Generate(MentionData.GenerateEveryone());
        /// <summary>
        /// Generates @here mention.
        /// </summary>
        /// <returns>Here mention</returns>
        public static Mention GenerateHere() => Generate(MentionData.GenerateHere());
    }
}