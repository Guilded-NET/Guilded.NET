namespace Guilded.NET.Objects.Chat {
    /// <summary>
    /// Markdown mark type.
    /// </summary>
    public enum MarkType {
        /// <summary>
        /// Produces a line which goes over whole text.
        /// </summary>
        Strikethrough,
        /// <summary>
        /// Produces a line which goes below text.
        /// </summary>
        Underline,
        /// <summary>
        /// Makes text heavier/bigger.
        /// </summary>
        Bold,
        /// <summary>
        /// Makes a text slightly tilted to the right side.
        /// </summary>
        Italic,
        /// <summary>
        /// Puts a text in a small container with a darker background.
        /// </summary>
        InlineCode,
        /// <summary>
        /// Makes text invisible until it's clicked on.
        /// </summary>
        Spoiler
    }
}