// using System;

// using Newtonsoft.Json;

// namespace Guilded.NET.Base.Forms
// {
//     /// <summary>
//     /// A filled out form response.
//     /// </summary>
//     public class FormResponse : BasicFormResponse
//     {
//         /// <summary>
//         /// ID of this response.
//         /// </summary>
//         /// <value>Response ID</value>
//         [JsonProperty(Required = Required.Always)]
//         public uint Id { get; set; }
//         /// <summary>
//         /// ID of the form this response is filling out.
//         /// </summary>
//         /// <value>Form ID</value>
//         [JsonProperty("customFormId", Required = Required.Always)]
//         public uint FormId { get; set; }
//         /// <summary>
//         /// Who is filling out this response.
//         /// </summary>
//         /// <value>Created by</value>
//         [JsonProperty(Required = Required.Always)]
//         public GId CreatedBy { get; set; }
//         /// <summary>
//         /// When this form response was created.
//         /// </summary>
//         /// <value>Created at</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime CreatedAt { get; set; }
//         /// <summary>
//         /// When this form response was updated/edited.
//         /// </summary>
//         /// <value>Updated at</value>
//         [JsonProperty(Required = Required.Always)]
//         public DateTime UpdatedAt { get; set; }
//     }
// }