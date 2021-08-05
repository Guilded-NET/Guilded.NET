using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Forms
{
    /// <summary>
    /// Data of a poll or a form created in Guilded.
    /// </summary>
    public class GuildedForm : BasicGuildedForm
    {
        #region JSON properties
        /// <summary>
        /// ID of this form/poll.
        /// </summary>
        /// <value>Form ID</value>
        public uint Id
        {
            get; set;
        }
        /// <summary>
        /// Author who created this form/poll.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty(Required = Required.Always)]
        public GId CreatedBy
        {
            get; set;
        }
        /// <summary>
        /// When the form/poll was created.
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty(Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the form/poll was created.
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty(Required = Required.AllowNull)]
        public DateTime? UpdatedAt
        {
            get; set;
        }
        /// <summary>
        /// How many people have responded to that form/poll.
        /// </summary>
        /// <value>Form/poll response count</value>
        [JsonProperty(Required = Required.Always)]
        public uint ResponseCount
        {
            get; set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Data of a poll or a form created in Guilded.
        /// </summary>
        /// <param name="specs">A form content specification.</param>
        public GuildedForm(FormSpecs specs) : base(specs) { }
        /// <summary>
        /// Data of a poll or a form created in Guilded.
        /// </summary>
        /// <param name="sections">Sections to generate form spec and form from</param>
        public GuildedForm(params FormSection[] sections) : base(sections) { }
        /// <summary>
        /// Data of a poll or a form created in Guilded.
        /// </summary>
        /// <param name="fields">Fields to generate form spec and form from</param>
        public GuildedForm(params FormField[] fields) : base(fields) { }
        #endregion
    }
}