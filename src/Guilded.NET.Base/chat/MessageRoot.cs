using System;
using System.Collections.Generic;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// The contents of messages, list items, etc. based on rich text markup.
    /// </summary>
    public abstract class MessageRoot<T> : ChatElement where T : MessageRoot<T>
    {
        #region Constructors
        /// <summary>
        /// The contents of messages, list items, etc. based on rich text markup.
        /// </summary>
        /// <param name="type">The type of the element it is</param>
        public MessageRoot(ElementType type) : base(type) { }
        #endregion

        #region Additional

        #region Abstract
        /// <summary>
        /// Adds a list of nodes to the message document.
        /// </summary>
        /// <param name="nodes">The list of nodes to add</param>
        /// <returns>This</returns>
        public abstract T With(IList<Node> nodes);
        /// <summary>
        /// Adds a node to the message document.
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>This</returns>
        public abstract T With(Node node);
        #endregion

        #region Paragraph
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="nodes">The array of elements to use in paragraph</param>
        /// <returns>This</returns>
        public T WithParagraph(params ChatElement[] nodes) =>
            With(new Paragraph(nodes));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="leaves">The array of leaves to use in paragraph</param>
        /// <returns>This</returns>
        public T WithParagraph(params Leaf[] leaves) =>
            WithParagraph(new TextContainer(leaves));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <returns>This</returns>
        public T WithParagraph(string content) =>
            WithParagraph(new TextContainer(content));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T WithParagraph(string content, params Mark[] formatting) =>
            WithParagraph(new TextContainer(content, formatting));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T WithParagraph(string content, params MarkType[] formatting) =>
            WithParagraph(new TextContainer(content, formatting));
        #endregion

        #region Quote
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="nodes">The array of quote lines to use</param>
        /// <returns>This</returns>
        public T WithQuote(params LineQuote[] nodes) =>
            With(new BlockQuote(nodes));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The text that should be converted to quote lines</param>
        /// <returns>This</returns>
        public T WithQuote(params string[] content) =>
            With(new BlockQuote(content));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The contents of the quote line</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T WithQuote(string content, params Mark[] formatting) =>
            WithQuote(new LineQuote(content, formatting));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The contents of the quote line</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T WithQuote(string content, params MarkType[] formatting) =>
            WithQuote(new LineQuote(content, formatting));
        #endregion

        #region Images
        /// <summary>
        /// Adds an image to the messsage document.
        /// </summary>
        /// <param name="url">The URL to image's source</param>
        /// <returns>This</returns>
        public T WithImage(Uri url) =>
            With(new Image(url));
        /// <summary>
        /// Adds an image to the messsage document.
        /// </summary>
        /// <param name="url">The URL to image's source</param>
        /// <returns>This</returns>
        public T WithImage(string url) =>
            WithImage(new Uri(url));
        #endregion

        #region Markdown
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="nodes">The list of message objects Markdown plain text holds</param>
        /// <returns>This</returns>
        public T WithMarkdown(params TextContainer[] nodes) =>
            With(new MarkdownText(nodes));
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="leaves">The array of leaves to use in Markdown plain text</param>
        /// <returns>This</returns>
        public T WithMarkdown(params Leaf[] leaves) =>
            WithMarkdown(new TextContainer(leaves));
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="text">The text document that will be used in Markdown node</param>
        /// <returns>This</returns>
        public T WithMarkdown(string text) =>
            WithMarkdown(new TextContainer(text));
        #endregion
        
        #endregion
    }
}