// using System.Collections.Generic;
// using System.Linq;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Forms
// {
//     /// <summary>
//     /// Specifications of forms and polls. Contains fields.
//     /// </summary>
//     public class FormSpecs : BaseObject
//     {
//         #region JSON properties
//         /// <summary>
//         /// A list of all form sections.
//         /// </summary>
//         /// <value>List of sections</value>
//         [JsonProperty(Required = Required.Always)]
//         public IList<FormSection> Sections
//         {
//             get; set;
//         }
//         /// <summary>
//         /// If all forum fields are valid.
//         /// </summary>
//         /// <value>Valid</value>
//         [JsonProperty(Required = Required.Always)]
//         public bool IsValid
//         {
//             get; set;
//         } = true;
//         #endregion

//         #region Constructors
//         /// <summary>
//         /// Specifications of forms and polls. Contains fields.
//         /// </summary>
//         /// <param name="sections">The array of sections of the forms</param>
//         public FormSpecs(IList<FormSection> sections) =>
//             Sections = sections;
//         /// <summary>
//         /// Specifications of forms and polls. Contains fields.
//         /// </summary>
//         /// <param name="fields">The array of fields of the sections</param>
//         public FormSpecs(params FormField[] fields) : this(fields.Select(field => new FormSection(field)).ToArray()) { }
//         #endregion
//     }
// }