using System.Collections.Generic;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's paragraph node.
    /// </summary>
    public class MarkDownText : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's paragraph node.
        /// </summary>
        public MarkDownText() =>
            Type = NodeType.MarkdownPlainText;
        /// <summary>
        /// Generates paragraph node.
        /// </summary>
        /// <param name="content">Message content</param>
        /// <returns>Markdown plain text node</returns>
        public static MarkDownText Generate(string content) =>
            new MarkDownText
            {
                Nodes = new List<IMessageObject> {
                    TextObj.GenerateText(content)
                }
            };
    }
}