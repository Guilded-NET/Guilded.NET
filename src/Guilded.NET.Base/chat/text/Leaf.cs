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
    public class Leaf : ChatElement, ICloneable
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


        #region Additional
        /// <summary>
        /// Returns sub-leaf from the given range.
        /// </summary>
        /// <returns>Sub-leaf</returns>
        public Leaf this[Range range] =>
            Clone(Text[range]);

        /// <summary>
        /// Adds a mark to the leaf.
        /// </summary>
        /// <param name="mark">The mark to add</param>
        /// <returns>This</returns>
        public Leaf Add(Mark mark)
        {
            Marks.Add(mark);
            return this;
        }
        /// <summary>
        /// Adds a mark to the leaf.
        /// </summary>
        /// <param name="mark">The mark to add</param>
        /// <returns>This</returns>
        public Leaf Add(MarkType mark) =>
            Add(new Mark(mark));

        #region Contains
        /// <summary>
        /// Returns whether the leaf contains a <see cref="Mark"/> with specific <see cref="MarkType"/>.
        /// </summary>
        /// <param name="formatting">The formatting that this leaf may hold</param>
        /// <returns>Contains formatting</returns>
        public bool Contains(params MarkType[] formatting) =>
            Marks.FirstOrDefault(x => formatting.Contains(x.Type)) != default;
        /// <summary>
        /// Returns whether leaf's text has specificed part of the string.
        /// </summary>
        /// <param name="value">Value that leaf's text potentially holds</param>
        /// <returns>If the string exist in the text</returns>
        public bool Contains(string value) =>
            Text.Contains(value);
        /// <summary>
        /// Returns whether leaf's text has specificed part of the string.
        /// </summary>
        /// <param name="value">Value that leaf's text potentially holds</param>
        /// <param name="comparisonType">Specifies how to search the string</param>
        /// <returns>If the string exist in the text</returns>
        public bool Contains(string value, StringComparison comparisonType) =>
            Text.Contains(value, comparisonType);
        /// <summary>
        /// Returns whether leaf's text has specificed character.
        /// </summary>
        /// <param name="value">Value that leaf's text potentially holds</param>
        /// <returns>If the string exist in the text</returns>
        public bool Contains(char value) =>
            Text.Contains(value);
        /// <summary>
        /// Returns whether leaf's text has specificed character.
        /// </summary>
        /// <param name="value">Value that leaf's text potentially holds</param>
        /// <param name="comparisonType">Specifies how to search the string</param>
        /// <returns>If the string exist in the text</returns>
        public bool Contains(char value, StringComparison comparisonType) =>
            Text.Contains(value, comparisonType);
        #endregion
        
        #region Split
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <param name="count">How many splits to do</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(char separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, count, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(char separator, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(params char[] separator) =>
            Text.Split(separator).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <param name="count">How many splits to do</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(char[] separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, count, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <param name="count">How many splits to do</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(string separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, count, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The separator where the split will happen</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(string separator, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The array of separators where the split will happen</param>
        /// <param name="count">How many splits to do</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(string[] separator, int count, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, count, options).Select(Clone).ToArray();
        /// <summary>
        /// Splits a leaf by separator and creates clone sub-leaves.
        /// </summary>
        /// <param name="separator">The array of separators where the split will happen</param>
        /// <param name="options">Splitting options</param>
        /// <returns>Array of leaves</returns>
        public Leaf[] Split(string[] separator, StringSplitOptions options = StringSplitOptions.None) =>
            Text.Split(separator, options).Select(Clone).ToArray();
        #endregion

        #region StartsWith
        /// <summary>
        /// Returns whether the leaf's text starts with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the start</param>
        /// <returns>Text starts with string</returns>
        public bool StartsWith(char value) =>
            Text.StartsWith(value);
        /// <summary>
        /// Returns whether the leaf's text starts with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the start</param>
        /// <returns>Text starts with string</returns>
        public bool StartsWith(string value) =>
            Text.StartsWith(value);
        /// <summary>
        /// Returns whether the leaf's text starts with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the start</param>
        /// <param name="ignoreCase">Whether the case should be ignored at the start</param>
        /// <param name="culture">Determines how to check by the given culture</param>
        /// <returns>Text starts with string</returns>
        public bool StartsWith(string value, bool ignoreCase, CultureInfo culture) =>
            Text.StartsWith(value, ignoreCase, culture);
        /// <summary>
        /// Returns whether the leaf's text starts with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the start</param>
        /// <param name="comparisonType">Specifies how to search the string</param>
        /// <returns>Text starts with string</returns>
        public bool StartsWith(string value, StringComparison comparisonType) =>
            Text.StartsWith(value, comparisonType);
        #endregion

        #region EndsWith
        /// <summary>
        /// Returns whether the leaf's text ends with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the end</param>
        /// <returns>Text ends with string</returns>
        public bool EndsWith(char value) =>
            Text.EndsWith(value);
        /// <summary>
        /// Returns whether the leaf's text ends with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the end</param>
        /// <returns>Text ends with string</returns>
        public bool EndsWith(string value) =>
            Text.EndsWith(value);
        /// <summary>
        /// Returns whether the leaf's text ends with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the end</param>
        /// <param name="ignoreCase">Whether the case should be ignored at the end</param>
        /// <param name="culture">Determines how to check by the given culture</param>
        /// <returns>Text ends with string</returns>
        public bool EndsWith(string value, bool ignoreCase, CultureInfo culture) =>
            Text.EndsWith(value, ignoreCase, culture);
        /// <summary>
        /// Returns whether the leaf's text ends with given string.
        /// </summary>
        /// <param name="value">Value that is potentially at the end</param>
        /// <param name="comparisonType">Specifies how to search the string</param>
        /// <returns>Text ends with string</returns>
        public bool EndsWith(string value, StringComparison comparisonType) =>
            Text.EndsWith(value, comparisonType);
        #endregion

        #endregion


        #region Overrides
        /// <summary>
        /// Creates a new leaf with similar properties.
        /// </summary>
        /// <returns>Cloned object</returns>
        public object Clone() =>
            new Leaf(Text, Marks.Select(x => (Mark)x.Clone()).ToArray());
        /// <summary>
        /// Creates a new leaf with similar properties.
        /// </summary>
        /// <param name="text">The text of the leaf</param>
        /// <returns>Cloned leaf</returns>
        public Leaf Clone(string text) =>
            new Leaf(text, Marks.Select(x => (Mark)x.Clone()).ToArray());
        /// <summary>
        /// Returns the text of the leaf with the string equivalent to leaf's formatting.
        /// </summary>
        /// <returns>Text with formatting</returns>
        public override string ToString()
        {
            // Gets all marks and turns them to their symbol representations
            IEnumerable<string> marks = Marks.Select(x => Mark.MarkSymbols[x.Type]);
            // Returns content with mark symbols
            return string.Concat(marks)
                + GetEscaped(Text)
                + string.Concat(marks.Reverse());
        }
        private string GetEscaped(string text) => text
            .Replace("\\", "\\\\")
            .Replace("*", "\\*")
            .Replace("_", "\\_")
            .Replace("~", "\\~")
            .Replace("`", "\\`")
            .Replace("||", "\\|\\|");
        #endregion


        #region Static methods
        /// <summary>
        /// Gets whether the leaf itself, the text is null or is empty.
        /// </summary>
        /// <param name="leaf">Leaf to check</param>
        /// <returns>Leaf is null or ""</returns>
        public static bool IsNullOrEmpty(Leaf leaf) =>
            string.IsNullOrEmpty(leaf?.Text);
        /// <summary>
        /// Gets whether the leaf itself, the text is null, is empty or only has whitespace characters.
        /// </summary>
        /// <param name="leaf">Leaf to check</param>
        /// <returns>Leaf is null, "" or whitespaces only</returns>
        public static bool IsNullOrWhiteSpace(Leaf leaf) =>
            string.IsNullOrWhiteSpace(leaf?.Text);
        #endregion
    }
}