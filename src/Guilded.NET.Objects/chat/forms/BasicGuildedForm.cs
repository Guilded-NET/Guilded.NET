using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms
{
    /// <summary>
    /// A poll or a form created in Guilded.
    /// </summary>
    public class BasicGuildedForm : ClientObject
    {
        /// <summary>
        /// A poll or a form created in Guilded.
        /// </summary>
        public BasicGuildedForm() =>
            (TeamId, Title, Description, IsPublic, Type) = (null, "Form title", "", true, FormType.Form);
        /// <summary>
        /// A poll or a form created in Guilded.
        /// </summary>
        /// <param name="sections">Sections to generate form spec and form from</param>
        public BasicGuildedForm(params FormSection[] sections) : this() =>
            Specs = FormSpecs.Generate(sections);
        /// <summary>
        /// A poll or a form created in Guilded.
        /// </summary>
        /// <param name="fields">Fields to generate form spec and form from</param>
        public BasicGuildedForm(params FormField[] fields) : this() =>
            Specs = FormSpecs.Generate(fields);
        /// <summary>
        /// ID of the team where the form/poll is in.
        /// </summary>
        /// <value>Optional team ID</value>
        [JsonProperty("teamId", NullValueHandling = NullValueHandling.Ignore)]
        public GId TeamId
        {
            get; set;
        }
        /// <summary>
        /// Title of the form/poll.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title
        {
            get; set;
        }
        /// <summary>
        /// Description of the form/poll describing the form/poll.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.Always)]
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// If the form/poll is publicly available.
        /// </summary>
        /// <value>Public</value>
        [JsonProperty("isPublic")]
        public bool IsPublic
        {
            get; set;
        }
        /// <summary>
        /// Type of the form(form, poll).
        /// </summary>
        /// <value>Form type</value>
        [JsonProperty("type", Required = Required.Always)]
        public FormType Type
        {
            get; set;
        }
        /// <summary>
        /// A form content specification.
        /// </summary>
        /// <value>Form content</value>
        [JsonProperty("formSpecs", Required = Required.Always)]
        public FormSpecs Specs
        {
            get; set;
        }
    }
}