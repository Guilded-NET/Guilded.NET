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
            Marks.Any(mark => formatting.Contains(mark.Type));
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
            new Leaf(Text, Marks.Select(mark => (Mark)mark.Clone()).ToArray());
        /// <summary>
        /// Creates a new leaf with similar properties.
        /// </summary>
        /// <param name="text">The text of the leaf</param>
        /// <returns>Cloned leaf</returns>
        public Leaf Clone(string text) =>
            new Leaf(text, Marks.Select(mark => (Mark)mark.Clone()).ToArray());
        /// <summary>
        /// Returns the text of the leaf with the string equivalent to leaf's formatting.
        /// </summary>
        /// <returns>Text with formatting</returns>
        public override string ToString()
        {
            // Gets all marks and turns them to their symbol representations
            IEnumerable<string> marks = Marks.Select(mark => Mark.MarkSymbols[mark.Type]);

            return string.Concat(marks)
                + GetEscaped(Text)
                + string.Concat(marks.Reverse());
        }
        private string GetEscaped(string text) =>
            text
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