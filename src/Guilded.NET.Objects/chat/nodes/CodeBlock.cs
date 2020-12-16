using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's code block node.
    /// </summary>
    public class CodeBlock: ContainerNode<CodeLine> {
        /// <summary>
        /// Represents Guilded's code block node.
        /// </summary>
        public CodeBlock() =>
            (Object, Type) = (MsgObject.Block, NodeType.CodeContainer);
        /// <summary>
        /// Generates code block node.
        /// </summary>
        /// <param name="objs">List of code lines</param>
        /// <returns>Code block node</returns>
        public static CodeBlock Generate(string language = null, params CodeLine[] objs) =>
            new CodeBlock {
                Nodes = objs.ToList(),
                // Sets a language. If it's null, then set it as unformatted
                Data = new Dictionary<string, object> {
                    { "language", string.IsNullOrWhiteSpace(language) ? "unformatted" : language.ToLower() }
                }
            };
        /// <summary>
        /// Generates code block node.
        /// </summary>
        /// <param name="lines">List of code lines</param>
        /// <returns>Code block node</returns>
        public static CodeBlock Generate(string language = null, params string[] lines) =>
            Generate(language, lines.Select(line => CodeLine.Generate(line)).ToArray());
        /// <summary>
        /// Turns code block to string.
        /// </summary>
        /// <returns>Code block as a string</returns>
        public override string ToString() => string.Join('\n', Nodes);
    }
}