namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// What type of form field it is.
    /// </summary>
    public enum FormFieldType {
        /// <summary>
        /// A small text box/input which can be used for anything.
        /// </summary>
        Text,
        /// <summary>
        /// A big text box/input which can be used to create long paragraphs and descriptions.
        /// </summary>
        TextArea,
        /// <summary>
        /// A selectable list of items. One can be selected.
        /// </summary>
        Radios,
        /// <summary>
        /// A list of items with checkboxes. Multiple can be checked.
        /// </summary>
        Checkboxes,
        /// <summary>
        /// A dropdown of all items which can be selected. One can be selected. Similar to <see cref="Radios"/>.
        /// </summary>
        Dropdown
    }
}