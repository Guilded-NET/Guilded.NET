using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Line of code block in Guilded.
    /// </summary>
    public class CodeLine: ContainerNode<IMessageObject> {
        /// <summary>
        /// Line of code block in Guilded.
        /// </summary>
        public CodeLine() =>
            Type = NodeType.CodeLine;
        /// <summary>
        /// Generates code block line.
        /// </summary>
        /// <param name="objs">Text objects to create line from</param>
        /// <returns>Code block line</returns>
        public static CodeLine Generate(params TextObj[] objs) =>
            new CodeLine {
                Nodes = objs
            };
        /// <summary>
        /// Generates code block line.
        /// </summary>
        /// <param name="line">String to create line from</param>
        /// <returns>Code block line</returns>
        public static CodeLine Generate(string line) =>
            Generate(TextObj.GenerateText(line));
        /// <summary>
        /// Turns code block line to string.
        /// </summary>
        /// <returns>Code block line as a string</returns>
        public override string ToString() => string.Concat(Nodes);
    }
}