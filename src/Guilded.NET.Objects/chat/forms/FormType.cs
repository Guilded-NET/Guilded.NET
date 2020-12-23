namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// Type of the form(form, poll)
    /// </summary>
    public enum FormType {
        /// <summary>
        /// A form which can have multiple fields and optional fields. <br/>
        /// Useful for making applications.
        /// </summary>
        Form,
        /// <summary>
        /// A form which can only have 1 field and they can't be optional. <br/>
        /// Useful for making quick questions.
        /// </summary>
        Poll
    }
}