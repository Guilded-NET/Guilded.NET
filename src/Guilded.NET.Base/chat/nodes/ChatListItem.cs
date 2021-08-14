using System;
using System.Linq;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// An item of an ordered or unordered list.
    /// </summary>
    /// <remarks>
    /// An item for <see cref="ChatList"/> node.
    /// </remarks>
    /// <seealso cref="ChatList"/>
    public class ChatListItem : ContainerNode<ChatListItem>
    {
        const string linebreak_indent = "  ";
        
        #region Constructors
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public ChatListItem(IList<ChatElement> nodes) : base(NodeType.ListItem, ElementType.Block, nodes) { } 
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="nodes">The array of message objects this node holds</param>
        public ChatListItem(params ChatElement[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="leaves">The list of leaves of the text container, which list item should hold</param>
        public ChatListItem(params Leaf[] leaves) : this(new TextContainer(leaves)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        public ChatListItem(string content) : this(new TextContainer(content)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public ChatListItem(string content, params Mark[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="content">The text that should be converted to text container</param>
        /// <param name="formatting">The formatting of the text</param>
        public ChatListItem(string content, params MarkType[] formatting) : this(new TextContainer(content, formatting)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public ChatListItem(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public ChatListItem(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        /// <param name="content">The contents that should be converted to text container</param>
        public ChatListItem(object content) : this(new TextContainer(content)) { }
        /// <summary>
        /// An item of an ordered or unordered list.
        /// </summary>
        public ChatListItem() : this(new List<ChatElement>()) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Converts <see cref="ChatListItem"/> to its string equivalent with bullet prefix.
        /// </summary>
        /// <returns><see cref="ChatListItem"/> as string</returns>
        public override string ToString() => ToString(false, "");
        /// <summary>
        /// Converts <see cref="ChatListItem"/> to its string equivalent with bullet prefix.
        /// </summary>
        /// <param name="isOrdered">Whether the list item is numbered or bulleted</param>
        /// <param name="indent">The indent of this list item</param>
        /// <param name="index">The index of the list item in the list</param>
        /// <returns><see cref="ChatListItem"/> as string</returns>
        public string ToString(bool isOrdered, string indent, int index = 0)
        {
            // Turns itself to a string
            string str = string.Concat(Nodes);
            // Start of the list item
            string start = isOrdered ? $"{index + 1}. " : "- ";
            // Join it all together
            return $"{indent}{start}{str.ToString().Replace("\n", '\n' + indent + linebreak_indent)}\n";
        }
        #endregion
    }
}