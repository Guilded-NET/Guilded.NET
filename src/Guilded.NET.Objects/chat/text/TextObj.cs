using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents text object which holds leaves.
    /// </summary>
    public class TextObj: BaseObject, IMessageObject {
        /// <summary>
        /// Represents text object which holds leaves.
        /// </summary>
        public TextObj() =>
            Object = MsgObject.Text;
        /// <summary>
        /// Object type of the text object.
        /// </summary>
        /// <value>MsgObject.Text</value>
        [JsonProperty("object", Required = Required.Always)]
        public MsgObject Object {
            get; set;
        }
        /// <summary>
        /// List of leaves in this text object.
        /// </summary>
        /// <value>List of leaves</value>
        [JsonProperty("leaves", Required = Required.Always)]
        public IList<Leaf> Leaves {
            get; set;
        }
        /// <summary>
        /// Generates normal text object.
        /// </summary>
        /// <param name="leaves">List of leaves</param>
        /// <returns>Text object</returns>
        public static TextObj GenerateText(params Leaf[] leaves) =>
            new TextObj {
                Leaves = leaves
            };
        /// <summary>
        /// Generates normal text object.
        /// </summary>
        /// <param name="content">String to generate leaves from</param>
        /// <returns>Text object</returns>
        public static TextObj GenerateText(string content) =>
            GenerateText(Leaf.Generate(content));
        /// <summary>
        /// Turns text object to string.
        /// </summary>
        /// <returns>Text object as a string</returns>
        public override string ToString() => string.Concat(Leaves);
    }
}