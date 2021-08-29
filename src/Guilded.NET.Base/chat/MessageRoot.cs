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
        public abstract T Add(IList<Node> nodes);
        /// <summary>
        /// Adds a node to the message document.
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>This</returns>
        public abstract T Add(Node node);
        #endregion

        #region Paragraph
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="nodes">The array of elements to use in paragraph</param>
        /// <returns>This</returns>
        public T AddParagraph(params ChatElement[] nodes) =>
            Add(new Paragraph(nodes));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="leaves">The array of leaves to use in paragraph</param>
        /// <returns>This</returns>
        public T AddParagraph(params Leaf[] leaves) =>
            AddParagraph(new TextContainer(leaves));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <returns>This</returns>
        public T AddParagraph(string content) =>
            AddParagraph(new TextContainer(content));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddParagraph(string content, params Mark[] formatting) =>
            AddParagraph(new TextContainer(content, formatting));
        /// <summary>
        /// Adds a paragraph to the message document.
        /// </summary>
        /// <param name="content">The contents of the paragraph</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddParagraph(string content, params MarkType[] formatting) =>
            AddParagraph(new TextContainer(content, formatting));
        #endregion

        #region Quote
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="nodes">The array of quote lines to use</param>
        /// <returns>This</returns>
        public T AddQuote(params LineQuote[] nodes) =>
            Add(new BlockQuote(nodes));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The text that should be converted to quote lines</param>
        /// <returns>This</returns>
        public T AddQuote(params string[] content) =>
            Add(new BlockQuote(content));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The contents of the quote line</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddQuote(string content, params Mark[] formatting) =>
            AddQuote(new LineQuote(content, formatting));
        /// <summary>
        /// Adds a block quote to the message document.
        /// </summary>
        /// <param name="content">The contents of the quote line</param>
        /// <param name="formatting">The formatting of the text</param>
        /// <returns>This</returns>
        public T AddQuote(string content, params MarkType[] formatting) =>
            AddQuote(new LineQuote(content, formatting));
        #endregion

        #region Images
        /// <summary>
        /// Adds an image to the messsage document.
        /// </summary>
        /// <param name="url">The URL to image's source</param>
        /// <returns>This</returns>
        public T AddImage(Uri url) =>
            Add(new Image(url));
        /// <summary>
        /// Adds an image to the messsage document.
        /// </summary>
        /// <param name="url">The URL to image's source</param>
        /// <returns>This</returns>
        public T AddImage(string url) =>
            AddImage(new Uri(url));
        #endregion

        #region Markdown
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="nodes">The list of message objects Markdown plain text holds</param>
        /// <returns>This</returns>
        public T AddMarkdown(params TextContainer[] nodes) =>
            Add(new MarkdownText(nodes));
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="leaves">The array of leaves to use in Markdown plain text</param>
        /// <returns>This</returns>
        public T AddMarkdown(params Leaf[] leaves) =>
            AddMarkdown(new TextContainer(leaves));
        /// <summary>
        /// Adds a Markdown plain text to the message document.
        /// </summary>
        /// <param name="text">The text document that will be used in Markdown node</param>
        /// <returns>This</returns>
        public T AddMarkdown(string text) =>
            AddMarkdown(new TextContainer(text));
        #endregion
        
        #endregion
    }
}