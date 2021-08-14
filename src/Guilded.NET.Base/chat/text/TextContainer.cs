using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A container holding a list of text leaves.
    /// </summary>
    /// <seealso cref="Leaf"/>
    /// <seealso cref="Node"/>
    /// <seealso cref="ChatElement"/>
    public class TextContainer : ChatElement
    {
        #region JSON properties
        /// <summary>
        /// The list of leaves this text container holds.
        /// </summary>
        /// <value>List of leaves</value>
        [JsonProperty(Required = Required.Always)]
        public IList<Leaf> Leaves
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on given list of <see cref="Leaf"/> instances.
        /// </summary>
        /// <param name="leaves">The list of leaves this text container holds</param>
        public TextContainer(IList<Leaf> leaves) : base(ElementType.Text) =>
            Leaves = leaves;
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on given array of <see cref="Leaf"/> instances.
        /// </summary>
        /// <param name="leaves">The array of leaves this text container holds</param>
        public TextContainer(params Leaf[] leaves) : this((IList<Leaf>)leaves) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on <paramref name="content"/> that will be converted to a singular leaf.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        public TextContainer(string content) : this(new Leaf(content)) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on <paramref name="content"/> and formatting <paramref name="formatting"/> that will be converted to a singular leaf.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, IList<Mark> formatting) : this(new Leaf(content, formatting)) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on <paramref name="content"/> and formatting <paramref name="formatting"/> that will be converted to a singular leaf.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, params Mark[] formatting) : this(new Leaf(content, formatting)) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on <paramref name="content"/> and formatting <paramref name="formatting"/> that will be converted to a singular leaf.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, IList<MarkType> formatting) : this(content, formatting.Select(x => new Mark(x)).ToArray()) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> based on <paramref name="content"/> and formatting <paramref name="formatting"/> that will be converted to a singular leaf.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, params MarkType[] formatting) : this(content, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, IList<Mark> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, params Mark[] formatting) : this(format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, IList<MarkType> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, params MarkType[] formatting) : this(format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public TextContainer(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, IList<Mark> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, params Mark[] formatting) : this(provider, format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, IList<MarkType> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string and leaf formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, params MarkType[] formatting) : this(provider, format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as a formatting string.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public TextContainer(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as <paramref name="content"/> as a string.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        public TextContainer(object content) : this(content.ToString()) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, IList<Mark> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, params Mark[] formatting) : this(content, (IList<Mark>)formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, IList<MarkType> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Creates a new <see cref="TextContainer"/> with content as <paramref name="content"/> as a string and formatting <paramref name="formatting"/>.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, params MarkType[] formatting) : this(content, (IList<MarkType>)formatting) { }
        #endregion

        #region Overrides
        /// <summary>
        /// Gets string equivalents of all leaves and joins them together.
        /// </summary>
        /// <returns>List of leaves as string</returns>
        public override string ToString() => string.Concat(Leaves);
        #endregion
    }
}