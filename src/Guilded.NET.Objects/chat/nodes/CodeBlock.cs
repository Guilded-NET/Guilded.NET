using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Represents Guilded's code block node.
    /// </summary>
    public class CodeBlock: ContainerNode<CodeLine> {
        /// <summary>
        /// Represents Guilded's code block node.
        /// </summary>
        public CodeBlock() =>
            Type = NodeType.CodeContainer;
        /// <summary>
        /// Language this codeblock is highlighted with.
        /// </summary>
        /// <value>Highlighting language</value>
        [JsonIgnore]
        public string Language {
            get => GetDataProperty<string>("language");
        }
        /// <summary>
        /// Generates code block node.
        /// </summary>
        /// <param name="language">A language it should highlight as</param>
        /// <param name="objs">List of code lines</param>
        /// <returns>Code block node</returns>
        public static CodeBlock Generate(string language = null, params CodeLine[] objs) =>
            new CodeBlock {
                Nodes = objs,
                // Sets a language. If it's null, then set it as unformatted
                Data = JObject.FromObject(
                    new { language = string.IsNullOrWhiteSpace(language) ? "unformatted" : language.ToLower() }
                )
            };
        /// <summary>
        /// Generates code block node.
        /// </summary>
        /// <param name="language">A language it should highlight as</param>
        /// <param name="lines">List of code lines</param>
        /// <returns>Code block node</returns>
        public static CodeBlock Generate(string language = null, params string[] lines) =>
            Generate(language, lines.Select(line => CodeLine.Generate(line)).ToArray());
        /// <summary>
        /// Turns code block to string.
        /// </summary>
        /// <returns>Code block as a string</returns>
        public override string ToString() => $"```{(Language != "unformatted" ? Language : "")}\n{string.Join('\n', Nodes)}\n```";
    }
}