// using System.ComponentModel;
// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Forms
// {
//     /// <summary>
//     /// A poll or a form created in Guilded.
//     /// </summary>
//     public class BasicGuildedForm : ClientObject
//     {
//         /// <summary>
//         /// ID of the team where the form/poll is in.
//         /// </summary>
//         /// <value>Optional team ID</value>
//         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
//         public GId TeamId
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Title of the form/poll.
//         /// </summary>
//         /// <value>Title</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Title
//         {
//             get; set;
//         } = "Form title";
//         /// <summary>
//         /// Description of the form/poll describing the form/poll.
//         /// </summary>
//         /// <value>Description</value>
//         [JsonProperty(Required = Required.Always)]
//         public string Description
//         {
//             get; set;
//         } = "";
//         /// <summary>
//         /// If the form/poll is publicly available.
//         /// </summary>
//         /// <value>Public form</value>
//         public bool IsPublic
//         {
//             get; set;
//         }
//         /// <summary>
//         /// Type of the form(form, poll).
//         /// </summary>
//         /// <value>Form type</value>
//         [JsonProperty(Required = Required.Always)]
//         public FormType Type
//         {
//             get; set;
//         } = FormType.Form;
//         /// <summary>
//         /// A form content specification.
//         /// </summary>
//         /// <value>Form content</value>
//         [JsonProperty("formSpecs", Required = Required.Always)]
//         public FormSpecs Specs
//         {
//             get; set;
//         }
//         /// <summary>
//         /// A poll or a form created in Guilded.
//         /// </summary>
//         /// <param name="specs">A form content specification.</param>
//         public BasicGuildedForm(FormSpecs specs) =>
//             Specs = specs;
//         /// <summary>
//         /// A poll or a form created in Guilded.
//         /// </summary>
//         /// <param name="sections">Sections to generate form spec and form from</param>
//         public BasicGuildedForm(params FormSection[] sections) =>
//             Specs = new FormSpecs(sections);
//         /// <summary>
//         /// A poll or a form created in Guilded.
//         /// </summary>
//         /// <param name="fields">Fields to generate form spec and form from</param>
//         public BasicGuildedForm(params FormField[] fields) =>
//             Specs = new FormSpecs(fields);
//     }
// }