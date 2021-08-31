using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A piece of text with formatting in a paragraph or any other node.
    /// </summary>
    /// <seealso cref="MarkType"/>
    /// <seealso cref="Mark"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="ContainerNode{T}"/>
    public partial class Leaf : ChatElement, ICloneable
    {
        #region JSON properties
        /// <summary>
        /// The piece of text this leaf holds.
        /// </summary>
        /// <value>Text</value>
        [JsonProperty(Required = Required.Always)]
        public string Text
        {
            get; set;
        }
        /// <summary>
        /// The formatting of this leaf.
        /// </summary>
        /// <value>List of marks</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Mark> Marks
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as <paramref name="text"/> and formatting based on <paramref name="formatting"/>.
        /// </summary>
        /// <param name="text">The piece of text this leaf holds</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string text, IList<Mark> formatting) : base(ElementType.Leaf) =>
            (Text, Marks) = (text, formatting);
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as <paramref name="text"/> and formatting based on <paramref name="formatting"/>.
        /// </summary>
        /// <param name="text">The piece of text this leaf holds</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string text, params Mark[] formatting) : this(text, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as <paramref name="text"/> and formatting based on <paramref name="formatting"/>.
        /// </summary>
        /// <param name="text">The piece of text this leaf holds</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string text, IList<MarkType> formatting) : this(text, formatting.Select(x => new Mark(x)).ToList()) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as <paramref name="text"/> and formatting based on <paramref name="formatting"/>.
        /// </summary>
        /// <param name="text">The piece of text this leaf holds</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string text, params MarkType[] formatting) : this(text, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as <paramref name="text"/> without any formatting.
        /// </summary>
        /// <param name="text">The piece of text this leaf holds</param>
        public Leaf(string text) : this(text, new List<Mark>()) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string format, object[] args, IList<Mark> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string format, object[] args, IList<MarkType> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string format, object[] args, params Mark[] formatting) : this(format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(string format, object[] args, params MarkType[] formatting) : this(format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with arguments <paramref name="args"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Leaf(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(IFormatProvider provider, string format, object[] args, IList<Mark> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(IFormatProvider provider, string format, object[] args, IList<MarkType> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(IFormatProvider provider, string format, object[] args, params Mark[] formatting) : this(provider, format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(IFormatProvider provider, string format, object[] args, params MarkType[] formatting) : this(provider, format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content as formatted string with arguments <paramref name="args"/> and provider <paramref name="provider"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public Leaf(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content <paramref name="content"/> as a string.
        /// </summary>
        /// <param name="content">The contents of the leaf to convert to string</param>
        public Leaf(object content) : this(content.ToString()) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf to convert to string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(object content, IList<Mark> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf to convert to string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(object content, params Mark[] formatting) : this(content, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf to convert to string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(object content, IList<MarkType> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Creates a new <see cref="Leaf"/> instance with content <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf to convert to string</param>
        /// <param name="formatting">The formatting of the text in this leaf</param>
        public Leaf(object content, params MarkType[] formatting) : this(content, (IList<MarkType>)formatting) { }
        #endregion
    }
}