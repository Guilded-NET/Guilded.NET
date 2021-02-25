namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Line of code block in Guilded.
    /// </summary>
    public class CodeLine : ContainerNode<IMessageObject>
    {
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
            new CodeLine
            {
                Nodes = objs
            };
        /// <summary>
        /// Generates code block line.
        /// </summary>
        /// <param name="line">String to create line from</param>
        /// <returns>Code block line</returns>
        public static CodeLine Generate(string line) =>
            Generate(TextObj.GenerateText(line));
    }
}