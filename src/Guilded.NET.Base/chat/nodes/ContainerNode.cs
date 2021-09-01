using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    using Teams;
    using Users;
    /// <summary>
    /// Node that holds other nodes and message objects.
    /// </summary>
    /// <typeparam name="T">The type of children of <see cref="Nodes"/> property</typeparam>
    /// <typeparam name="R">The child-type of container node</typeparam>
    /// <seealso cref="ContainerNode{T}"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="ChatElement"/>
    public abstract class ContainerNode<T, R> : Node where T : ChatElement where R : ContainerNode<T, R>
    {
        #region JSON properties
        /// <summary>
        /// The list of message objects this node holds.
        /// </summary>
        /// <value>List of <see cref="ChatElement"/></value>
        [JsonProperty(Required = Required.Always)]
        public IList<T> Nodes
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="nodes">The list of nodes this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, IList<T> nodes) : base(type, obj) =>
            Nodes = nodes;
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        /// <param name="nodes">The list of nodes this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data, IList<T> nodes) : base(type, obj, data) =>
            Nodes = nodes;
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data) : this(type, obj, data, new List<T>()) { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        /// <param name="node">The one node this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data, T node) : this(type, obj, data, new List<T> { node })  { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="node">The one node this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, T node) : this(type, obj, new List<T> { node })  { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        protected ContainerNode(NodeType type, ElementType obj = ElementType.Block) : this(type, obj, new List<T>()) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a node to <see cref="Nodes"/> list.
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>This</returns>
        public R Add(T node)
        {
            Nodes.Add(node);
            return (R)this;
        }
        /// <summary>
        /// Adds a node to <see cref="Nodes"/> list.
        /// </summary>
        /// <param name="nodes">The enumerable of nodes to add</param>
        /// <returns>This</returns>
        public R Add(IEnumerable<T> nodes)
        {
            Nodes = Nodes.Concat(nodes).ToList();
            return (R)this;
        }
        /// <summary>
        /// Removes a node from <see cref="Nodes"/> list.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>This</returns>
        public R Remove(T node)
        {
            Nodes.Remove(node);
            return (R)this;
        }
        /// <summary>
        /// Removes a node from <see cref="Nodes"/> list based on index.
        /// </summary>
        /// <param name="index">The index of the node to remove</param>
        /// <returns>This</returns>
        public R Remove(int index)
        {
            Nodes.RemoveAt(index);
            return (R)this;
        }
        /// <summary>
        /// Removes a node from <see cref="Nodes"/> list based on index.
        /// </summary>
        /// <param name="index">The index of the node to remove</param>
        /// <returns>This</returns>
        public R Remove(Index index) =>
            Remove(index.GetOffset(Nodes.Count));
        /// <summary>
        /// Removes a list of nodes from <see cref="Nodes"/> list based on index.
        /// </summary>
        /// <param name="start">The starting index of the nodes to remove</param>
        /// <param name="end">The ending index of the nodes to remove</param>
        /// <returns>This</returns>
        public R Remove(int start, int end)
        {
            Nodes = Nodes.SkipWhile((_, i) => i >= start && i <= end).ToList();
            return (R)this;
        }
        /// <summary>
        /// Removes a list of nodes from <see cref="Nodes"/> list based on indices.
        /// </summary>
        /// <param name="start">The starting index of the nodes to remove</param>
        /// <param name="end">The ending index of the nodes to remove</param>
        /// <returns>This</returns>
        public R Remove(Index start, Index end) =>
            Remove(start.GetOffset(Nodes.Count), end.GetOffset(Nodes.Count));
        /// <summary>
        /// Removes a list of nodes from <see cref="Nodes"/> list based on range.
        /// </summary>
        /// <param name="range">The range of nodes to remove</param>
        /// <returns>This</returns>
        public R Remove(Range range) =>
            Remove(range.Start, range.End);
        #endregion

        #region Overrides
        /// <summary>
        /// Gets string equivalent of node's children and joins them.
        /// </summary>
        /// <returns>List of nodes as string</returns>
        public override string ToString() =>
            string.Concat(Nodes) + "\n";
        #endregion
    }
    /// <summary>
    /// Node that holds other nodes and message objects.
    /// </summary>
    /// <typeparam name="T">The child-type of container node</typeparam>
    /// <seealso cref="Node"/>
    /// <seealso cref="ChatElement"/>
    public class ContainerNode<T> : ContainerNode<ChatElement, T> where T : ContainerNode<T>
    {
        #region Constructors
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="nodes">The list of nodes this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, IList<ChatElement> nodes) : base(type, obj, nodes) { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        /// <param name="nodes">The list of nodes this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data, IList<ChatElement> nodes) : base(type, obj, data, nodes) {}
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data) : base(type, obj, data) { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="data">The data containing information about the element</param>
        /// <param name="node">The one node this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, ElementData data, ChatElement node) : base(type, obj, data, node)  { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        /// <param name="node">The one node this node holds</param>
        protected ContainerNode(NodeType type, ElementType obj, ChatElement node) : base(type, obj, node)  { }
        /// <summary>
        /// Node that holds other nodes and message objects.
        /// </summary>
        /// <param name="type">The type of the node it is</param>
        /// <param name="obj">The type of the message object it is</param>
        protected ContainerNode(NodeType type, ElementType obj = ElementType.Block) : base(type, obj) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a text container based on given leaves.
        /// </summary>
        /// <param name="leaves">The array of leaves to add</param>
        /// <returns>This</returns>
        public T AddText(params Leaf[] leaves) =>
            Add(new TextContainer(leaves));
        /// <summary>
        /// Adds a text container based on given string.
        /// </summary>
        /// <param name="content">The text that text container holds</param>
        /// <returns>This</returns>
        public T AddText(string content) =>
            Add(new TextContainer(content));
        /// <summary>
        /// Adds a text container based on given string.
        /// </summary>
        /// <param name="content">The text that text container holds</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddText(string content, params Mark[] formatting) =>
            Add(new TextContainer(content, formatting));
        /// <summary>
        /// Adds a text container based on given string.
        /// </summary>
        /// <param name="content">The text that text container holds</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddText(string content, params MarkType[] formatting) =>
            Add(new TextContainer(content, formatting));
        /// <summary>
        /// Adds an emote to the node list.
        /// </summary>
        /// <param name="emote">The info of the emote to use</param>
        /// <returns>This</returns>
        public T AddEmote(EmoteInfo emote) =>
            Add(new ChatEmote(emote));
        /// <summary>
        /// Adds an emote to the node list.
        /// </summary>
        /// <param name="emote">The emote to use</param>
        /// <returns>This</returns>
        public T AddEmote(BaseEmote emote) =>
            Add(new ChatEmote(emote));
        /// <summary>
        /// Adds a channel mention based on given data.
        /// </summary>
        /// <param name="data">The data of the channel mention</param>
        /// <returns>This</returns>
        public T AddMention(ChannelMentionData data) =>
            Add(new ChannelMention(data));
        /// <summary>
        /// Adds a channel mention based on given name and identifier.
        /// </summary>
        /// <param name="name">The name of the channel</param>
        /// <param name="channelId">The identifier of the channel</param>
        /// <returns>This</returns>
        public T AddMention(string name, Guid channelId) =>
            AddMention(new ChannelMentionData(name, channelId));
        /// <summary>
        /// Adds a channel mention for the given channel.
        /// </summary>
        /// <param name="channel">The channel to mention</param>
        /// <returns>This</returns>
        public T AddMention(TeamChannel channel) =>
            AddMention(new ChannelMentionData(channel));
        /// <summary>
        /// Adds a member mention based on given data.
        /// </summary>
        /// <param name="data">The data of the member mention</param>
        /// <returns>This</returns>
        public T AddMention(MemberMentionData data) =>
            Add(new MemberMention(data));
        /// <summary>
        /// Adds an @everyone or @here mention.
        /// </summary>
        /// <param name="isHere">Whether it's @here mention or @everyone</param>
        /// <returns>This</returns>
        public T AddMention(bool isHere = false) =>
            AddMention(isHere);
        /// <summary>
        /// Adds a user mention based on given user.
        /// </summary>
        /// <param name="user">The user to mention</param>
        /// <returns>This</returns>
        public T AddMention(BaseUser user) =>
            AddMention(new MemberMentionData(user));
        /// <summary>
        /// Adds a role mention based on given role.
        /// </summary>
        /// <param name="role">The role to mention</param>
        /// <returns>This</returns>
        public T AddMention(TeamRole role) =>
            AddMention(new MemberMentionData(role));
        /// <summary>
        /// Adds a member mention based on given member and their colour.
        /// </summary>
        /// <param name="member">The member to mention</param>
        /// <param name="color">The colour of the member's role</param>
        /// <returns>This</returns>
        public T AddMention(Member member, Color? color = null) =>
            AddMention(new MemberMentionData(member, color));
        #endregion
    }
}