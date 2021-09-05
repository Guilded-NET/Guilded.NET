using System.ComponentModel;
using Newtonsoft.Json;

namespace Guilded.NET.Base.Forms
{
    /// <summary>
    /// An option in radio, check list and dropdown fields.
    /// </summary>
    public class FieldOption
    {
        #region JSON properties
        /// <summary>
        /// A name of the field option.
        /// </summary>
        /// <value>Name</value>
        [JsonProperty(Required = Required.Always)]
        public string Label
        {
            get; set;
        }
        /// <summary>
        /// A default option for this field.
        /// </summary>
        /// <value>Default option</value>
        public string DefaultValue
        {
            get; set;
        }
        /// <summary>
        /// A value of this field.
        /// </summary>
        /// <value>Form name</value>
        public FormId Value
        {
            get; set;
        }
        /// <summary>
        /// A name of the field this option is in.
        /// </summary>
        /// <value>Form name</value>
        public FormId FieldName
        {
            get; set;
        }
        /// <summary>
        /// A name of this option.
        /// </summary>
        /// <value>Form name</value>
        public FormId OptionName
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// An option in radio, check list and dropdown fields.
        /// </summary>
        /// <param name="label">The name of the field option</param>
        public FieldOption(string label)
        {
            FormId rand = FormId.Random;

            (FieldName, Label, OptionName, DefaultValue, Value) = (FormId.Random, label, rand, "Option 1", rand);
        }
        #endregion
    }
}