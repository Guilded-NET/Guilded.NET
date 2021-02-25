using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A form or a poll posted in the chat.
    /// </summary>
    public class Form: ContainerNode<IMessageObject> {
        /// <summary>
        /// A form or a poll posted in the chat.
        /// </summary>
        public Form() =>
            Type = NodeType.Form;
        /// <summary>
        /// ID of this form/poll.
        /// </summary>
        /// <value>Form ID</value>
        [JsonIgnore]
        public uint? FormId {
            get => GetDataProperty<uint>("customFormId");
        }
        /// <summary>
        /// Generates a form node. This is not a way to create a form. This is for creating a node in a message for a form.
        /// </summary>
        /// <param name="customFormId">ID of the form</param>
        /// <returns>New form</returns>
        public static Form Generate(uint customFormId) =>
            new Form {
                Data = JObject.FromObject(new { customFormId }),
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText("")
                }
            };
    }
}