namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's large heading node.
    /// </summary>
    public class LargeTitleNode: ContainerNode<IMessageObject> {
        /// <summary>
        /// Represents Guilded's large heading node.
        /// </summary>
        public LargeTitleNode() =>
            Type = NodeType.HeadingLarge;
        /// <summary>
        /// Generates large heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Large heading node</returns>
        public static LargeTitleNode Generate(params IMessageObject[] objs) =>
            new LargeTitleNode {
                Nodes = objs
            };
        /// <summary>
        /// Generates large heading node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Large heading node</returns>
        public static LargeTitleNode GenerateFromLeaves(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
        /// <summary>
        /// Turns large heading node to string.
        /// </summary>
        /// <returns>Large heading node</returns>
        public override string ToString() => $"# {base.ToString()}";
    }
}