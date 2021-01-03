using Newtonsoft.Json;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// A form or a poll posted in the chat.
    /// </summary>
    public class Form: ContainerNode<IMessageObject> {
        /// <summary>
        /// A form or a poll posted in the chat.
        /// </summary>
        public Form() =>
            (Type, Object) = (NodeType.Form, MsgObject.Block);
        /// <summary>
        /// ID of this form/poll.
        /// </summary>
        /// <value>Form ID</value>
        [JsonIgnore]
        public uint? FormId {
            get {
                // Get form ID
                object id = GetDataProperty("customFormId");
                // If it's null
                if(id == null) return null;
                // If it's not, cast it as a form ID
                return id as uint?;
            }
        }
        /// <summary>
        /// Generates a form node. This is not a way to create a form. This is for creating a node in a message for a form.
        /// </summary>
        /// <param name="formId">ID of the form</param>
        /// <returns>New form</returns>
        public static Form Generate(uint formId) =>
            new Form {
                Data = new Dictionary<string, object>() {
                    { "customFormId", formId }
                },
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText("")
                }
            };
    }
}