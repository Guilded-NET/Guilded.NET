using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// Container that holds text leaves.
    /// </summary>
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
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="leaves">The list of leaves this text container holds</param>
        public TextContainer(IList<Leaf> leaves) : base(ElementType.Text) =>
            Leaves = leaves;
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="leaves">The array of leaves this text container holds</param>
        public TextContainer(params Leaf[] leaves) : this((IList<Leaf>)leaves) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        public TextContainer(string content) : this(new Leaf(content)) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, IList<Mark> formatting) : this(new Leaf(content, formatting)) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, params Mark[] formatting) : this(new Leaf(content, formatting)) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, IList<MarkType> formatting) : this(content, formatting.Select(x => new Mark(x)).ToArray()) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string content, params MarkType[] formatting) : this(content, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, IList<Mark> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, params Mark[] formatting) : this(format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, IList<MarkType> formatting) : this(string.Format(format, args), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(string format, object[] args, params MarkType[] formatting) : this(format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public TextContainer(string format, params object[] args) : this(string.Format(format, args)) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, IList<Mark> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, params Mark[] formatting) : this(provider, format, args, (IList<Mark>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, IList<MarkType> formatting) : this(string.Format(provider, format, args), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(IFormatProvider provider, string format, object[] args, params MarkType[] formatting) : this(provider, format, args, (IList<MarkType>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="provider">The provider that gives the format string information about the culture</param>
        /// <param name="format">The composite format string</param>
        /// <param name="args">The arguments of the format string</param>
        public TextContainer(IFormatProvider provider, string format, params object[] args) : this(string.Format(provider, format, args)) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        public TextContainer(object content) : this(content.ToString()) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, IList<Mark> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, params Mark[] formatting) : this(content, (IList<Mark>)formatting) { }
        /// <summary>
        /// Container that holds text leaves.
        /// </summary>
        /// <param name="content">The contents of the leaf</param>
        /// <param name="formatting">The formatting of the leaf</param>
        public TextContainer(object content, IList<MarkType> formatting) : this(content.ToString(), formatting) { }
        /// <summary>
        /// Container that holds text leaves.
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