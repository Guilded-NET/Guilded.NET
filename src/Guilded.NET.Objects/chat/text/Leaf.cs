using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents text leaf in Guilded messages.
    /// </summary>
    public class Leaf: BaseObject, IMessageObject {
        /// <summary>
        /// Object type of the leaf.
        /// </summary>
        /// <value>MsgObject.Leaf</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object {
            get; set;
        } = MsgObject.Leaf;
        /// <summary>
        /// Piece of text in this leaf.
        /// </summary>
        /// <value></value>
        [JsonProperty("text", Required = Required.Always)]
        public string Text {
            get; set;
        }
        /// <summary>
        /// List of markdown marks in this leaf.
        /// </summary>
        /// <value>List of marks</value>
        [JsonProperty("marks", Required = Required.Always)]
        public IList<Mark> Marks {
            get; set;
        }
        /// <summary>
        /// Turns leaf to string.
        /// </summary>
        /// <returns>Leaf as a string</returns>
        public override string ToString() {
            // Gets all marks and turns them to their symbol representations
            IEnumerable<string> marks = Marks.Select(x => Mark.MarkSymbols[x.Type]);
            // Joins them
            string marksym = string.Concat(marks);
            // Returns content with mark symbols
            return string.Concat(marks) + Text + string.Concat(marks.Reverse());
        }
        /// <summary>
        /// Generates leaf from given text and marks.
        /// </summary>
        /// <param name="text">Text of the leaf</param>
        /// <param name="marks">Markdown marks</param>
        /// <returns>Message leaf</returns>
        public static Leaf Generate(string text, params MarkType[] marks) =>
            new Leaf {
                Text = text,
                Marks = marks.Select(x => new Mark {
                    Type = x
                }).ToArray()
            };
    }
}