using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    using Teams;
    /// <summary>
    /// Role, user, @everyone or @here mention.
    /// </summary>
    public class Mention: ContainerNode<IMessageObject> {
        public Mention() {
            Type = NodeType.Mention;
            Object = MsgObject.Inline;
        }
        /// <summary>
        /// Gets mention data.
        /// </summary>
        /// <value>Data of the mention</value>
        [JsonIgnore]
        public MentionData MentionData {
            get {
                // Get mention data
                object data = GetDataProperty("mention");
                // If it's null, return null
                if(data == null) return null;
                // Check if it's JObject or MentionData
                if(data is MentionData mention) return mention;
                else if(data is JObject obj) return obj.ToObject<MentionData>();
                // If it's neither, return null
                else return null;
            }
        }
        /// <summary>
        /// Turns mention to string.
        /// </summary>
        /// <returns>Mention as string</returns>
        public override string ToString() {
            MentionData data = MentionData;
            if(data == null) return "@???";
            else return $"@{data.Name}";
        }
        /// <summary>
        /// Generates mention.
        /// </summary>
        /// <param name="data">Mention data</param>
        /// <returns>Mention</returns>
        public static Mention Generate(MentionData data) =>
            new Mention {
                Data = new Dictionary<string, object> {
                    {"mention", data}
                },
                Type = NodeType.Mention,
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText($"@{data.Name}")
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