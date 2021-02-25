namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Represents Guilded's small heading node.
    /// </summary>
    public class SmallTitleNode : ContainerNode<IMessageObject>
    {
        /// <summary>
        /// Represents Guilded's small heading node.
        /// </summary>
        public SmallTitleNode() =>
            Type = NodeType.HeadingSmall;
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="objs">List of text objects</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode Generate(params IMessageObject[] objs) =>
            new SmallTitleNode
            {
                Nodes = objs
            };
        /// <summary>
        /// Generates small heading node.
        /// </summary>
        /// <param name="leaves">List of message leaves</param>
        /// <returns>Small heading node</returns>
        public static SmallTitleNode GenerateFromLeaves(params Leaf[] leaves) => Generate(TextObj.GenerateText(leaves));
        /// <summary>
        /// Turns small heading node to string.
        /// </summary>
        /// <returns>Small heading node</returns>
        public override string ToString() => $"## {base.ToString()}";
    }
}