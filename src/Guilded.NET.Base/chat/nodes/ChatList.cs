using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A list with numbers or bullets.
    /// </summary>
    /// <remarks>
    /// A node type that contains list items with bullets or numbers at the start.
    /// </remarks>
    /// <example>
    /// <para>List from strings:</para>
    /// <code>
    /// ChatList list = new ChatList("List item #1", "List item #2", "List item #3");
    /// </code>
    /// <para>List with other lists and list items:</para>
    /// <code>
    /// ChatList list = new ChatList
    /// (
    ///     new ChatListItem("List item #1"),
    ///     new ChatList("Sub-list item #1", "Sub-list item #2"),
    ///     new ChatListItem("List item #3")
    /// );
    /// </code>
    /// <para>Numerated list:</para>
    /// <code>
    /// ChatList list = new ChatList(new List&lt;string&gt; { "List item #1", "List item #2" }, true);
    /// </code>
    /// </example>
    /// <seealso cref="ChatListItem"/>
    public class ChatList : ContainerNode<Node, ChatList>
    {
        const string sublist_indent = "    ";
        private const string Ordered = "ordered-list";

        #region Properties
        /// <summary>
        /// Whether the list is with numbers or with bullets. 
        /// </summary>
        [JsonIgnore]
        public bool IsOrdered => Type == NodeType.OrderedList;
        #endregion

        #region Constructors
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="nodes">The list of list items or lists</param>
        /// <param name="isOrdered">Whether the list is with numbers or with bullets</param>
        public ChatList(IList<Node> nodes, bool isOrdered = false) : base(isOrdered ? NodeType.OrderedList : NodeType.UnorderedList, ElementType.Block, nodes) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="nodes">The array of list items or lists</param>
        public ChatList(params Node[] nodes) : this(nodes.ToList()) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="nodes">The list of text containers that will be used in list items</param>
        /// <param name="isOrdered">Whether the list is with numbers or with bullets</param>
        public ChatList(IEnumerable<TextContainer> nodes, bool isOrdered = false) : this(nodes.Select(x => new ChatListItem(x)).ToList<Node>(), isOrdered) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="nodes">The array of text containers that will be used in list items</param>
        public ChatList(params TextContainer[] nodes) : this(nodes, false) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="content">The list of texts that the list should hold</param>
        /// <param name="isOrdered">Whether the list is with numbers or with bullets</param>
        public ChatList(IEnumerable<string> content, bool isOrdered = false) : this(content.Select(x => new ChatListItem(x)).ToList<Node>(), isOrdered) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="content">The array of texts that the list should hold</param>
        public ChatList(params string[] content) : this(content, false) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="isOrdered">Whether the list is with numbers or with bullets</param>
        public ChatList(bool isOrdered = false) : this(new List<Node>(), isOrdered) { }
        /// <summary>
        /// A list with numbers or bullets.
        /// </summary>
        /// <param name="type">The type of the node used</param>
        [JsonConstructor]
        public ChatList([JsonProperty(Required = Required.Always)] string type) : this(type == Ordered) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a list item to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="nodes">The array of chat elements the list item has</param>
        /// <returns>This</returns>
        public ChatList AddItem(params ChatElement[] nodes) =>
            Add(new ChatListItem(nodes));
        /// <summary>
        /// Adds a list item to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="leaves">The array of leaves the list item has</param>
        /// <returns>This</returns>
        public ChatList AddItem(params Leaf[] leaves) =>
            AddItem(new TextContainer(leaves));
        /// <summary>
        /// Adds a list item to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="content">The contents of the list item</param>
        /// <returns>This</returns>
        public ChatList AddItem(string content) =>
            AddItem(new TextContainer(content));
        /// <summary>
        /// Adds a list item to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="content">The contents of the list item</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public ChatList AddItem(string content, params Mark[] formatting) =>
            AddItem(new TextContainer(content, formatting));
        /// <summary>
        /// Adds a list item to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="content">The contents of the list item</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public ChatList AddItem(string content, params MarkType[] formatting) =>
            AddItem(new TextContainer(content, formatting));
        /// <summary>
        /// Adds a sub-list to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="nodes">The array of nodes that will be part of sub-list</param>
        /// <param name="isOrdered">Whether the list is with numbers or with bullets. </param>
        /// <returns>This</returns>
        public ChatList AddList(IList<Node> nodes, bool isOrdered) =>
            Add(new ChatList(nodes, isOrdered));
        /// <summary>
        /// Adds a sub-list to the <see cref="Node"/> list.
        /// </summary>
        /// <param name="nodes">The array of nodes that will be part of sub-list</param>
        /// <returns>This</returns>
        public ChatList AddList(params Node[] nodes) =>
            AddList(nodes, false);
        #endregion

        #region Overrides
        /// <summary>
        /// Converts <see cref="ChatList"/> to its Markdown equivalent.
        /// </summary>
        /// <param name="indent">The indent of the list items</param>
        /// <returns><see cref="ChatList"/> as string</returns>
        public string ToString(string indent) =>
            string.Concat(Nodes.Select((x, i) => x is ChatListItem item ? item.ToString(IsOrdered, indent, i) : ((ChatList)x).ToString(indent + sublist_indent)));
        /// <summary>
        /// Converts <see cref="ChatList"/> to its Markdown equivalent.
        /// </summary>
        /// <returns><see cref="ChatList"/> as string</returns>
        public override string ToString() =>
            ToString("");
        #endregion
    }
}