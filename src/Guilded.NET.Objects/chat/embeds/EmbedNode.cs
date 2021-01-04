using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Node containing embeds. A.k.a. webhook message in Guilded.
    /// </summary>
    public class EmbedNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Node containing embeds. A.k.a. webhook message in Guilded.
        /// </summary>
        public EmbedNode() =>
            (Type, Object) = (NodeType.Embed, MsgObject.Block);
        /// <summary>
        /// List of embeds in this embed node.
        /// </summary>
        /// <value>List of embeds</value>
        [JsonIgnore]
        public IList<Embed> Embeds {
            get => GetDataProperty<IList<Embed>>("embeds");
        }
        /// <summary>
        /// Generates embed node from given embed data.
        /// </summary>
        /// <param name="embeds">List of embed datas</param>
        /// <returns>Embed node</returns>
        public static EmbedNode Generate(params Embed[] embeds) =>
            new EmbedNode {
                Data = JObject.FromObject(new { embeds })
            };
        /// <summary>
        /// Turns embed to string.
        /// </summary>
        /// <returns>Embed as string</returns>
        public override string ToString() => "[Embeds: ToString not supported]";
    }
}