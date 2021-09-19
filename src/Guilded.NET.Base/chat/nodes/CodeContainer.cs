using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A block with code syntax highlighting.
    /// </summary>
    /// <remarks>
    /// A code block/container with language associated.
    /// </remarks>
    /// <example>
    /// <para>Basic code container:</para>
    /// <code lang="csharp">
    /// CodeContainer code = new CodeContainer("csharp", "using System;\nusing System.Linq;");
    /// </code>
    /// <para>Code container with strings as lines:</para>
    /// <code lang="csharp">
    /// CodeContainer code = new CodeContainer("csharp", new string[]
    /// {
    ///     "using System;",
    ///     "using System.Linq;"
    /// });
    /// </code>
    /// <para>Code container with code lines:</para>
    /// <code lang="csharp">
    /// CodeContainer code = new CodeContainer("csharp", new List&lt;CodeLine&gt;
    /// {
    ///     new CodeLine("using System;"),
    ///     new CodeLine("using System.Linq;")
    /// });
    /// </code>
    /// <para>Unformatted code container:</para>
    /// <code lang="csharp">
    /// CodeContainer code = new CodeContainer(new string[]
    /// {
    ///     "This is unformatted code container.",
    ///     "Nothing will be formatted here."
    /// });
    /// </code>
    /// </example>
    /// <seealso cref="CodeLine"/>
    public class CodeContainer : ContainerNode<CodeLine, CodeContainer>
    {
        #region Properties
        /// <summary>
        /// The language this codeblock is highlighted as.
        /// </summary>
        /// <value>Highlighting language?</value>
        [JsonIgnore]
        public string Language => Data.Language;
        #endregion

        #region Constructors
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="language">The language this codeblock is highlighted as.</param>
        /// <param name="nodes">The list of message objects this node holds</param>
        public CodeContainer(string language, IList<CodeLine> nodes) : base(NodeType.CodeContainer, ElementType.Block, nodes) =>
            Data.Language = language;
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="nodes">The list of message objects this node holds</param>
        public CodeContainer(IList<CodeLine> nodes) : this("unformatted", nodes) { }
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="language">The language this codeblock is highlighted as.</param>
        /// <param name="lines">The enumerable of string code lines</param>
        public CodeContainer(string language, IEnumerable<string> lines) : this(language, lines.Select(line => new CodeLine(line)).ToList()) { }
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="lines">The enumerable of string code lines</param>
        public CodeContainer(IEnumerable<string> lines) : this("unformatted", lines) { }
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="language">The language this codeblock is highlighted as.</param>
        /// <param name="content">The contents of the codeblock</param>
        public CodeContainer(string language, string content) : this(language, content.Split('\n')) { }
        /// <summary>
        /// A block with code syntax highlighting.
        /// </summary>
        /// <param name="content">The contents of the codeblock</param>
        public CodeContainer(string content) : this("unformatted", content) { }
        #endregion

        #region Additional
        /// <summary>
        /// Adds a line to the code container based on given text container.
        /// </summary>
        /// <param name="text">The text container to use in code line</param>
        /// <returns>This</returns>
        public CodeContainer AddLine(TextContainer text) =>
            Add(new CodeLine(text));
        /// <summary>
        /// Adds a line to the code container based on given leaves.
        /// </summary>
        /// <param name="leaves">The array of leaves to use in the code line</param>
        /// <returns>This</returns>
        public CodeContainer AddLine(params Leaf[] leaves) =>
            AddLine(new TextContainer(leaves));
        /// <summary>
        /// Adds a line to the code container.
        /// </summary>
        /// <param name="content">The content of the code line</param>
        /// <returns>This</returns>
        public CodeContainer AddLine(string content) =>
            AddLine(new TextContainer(content));
        /// <summary>
        /// Adds an array of lines to the code container.
        /// </summary>
        /// <param name="content">The array of code lines</param>
        /// <returns>This</returns>
        public CodeContainer AddLines(params string[] content) =>
            Add(content.Select(line => new CodeLine(line)));
        /// <summary>
        /// Adds an array of lines to the code container.
        /// </summary>
        /// <param name="content">The content to add to the code container</param>
        /// <returns>This</returns>
        public CodeContainer AddLines(string content) =>
            AddLines(content.Split('\n'));
        #endregion

        #region Overrides
        /// <summary>
        /// Converts <see cref="CodeContainer"/> to its Markdown equivalent.
        /// </summary>
        /// <returns><see cref="CodeContainer"/> as string</returns>
        public override string ToString() =>
            $"```{(Language != "unformatted" ? Language : "")}\n{base.ToString()}```";
        #endregion
    }
}