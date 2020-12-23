using Newtonsoft.Json;
using System;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// Data of a poll or a form created in Guilded.
    /// </summary>
    public class GuildedForm: BasicGuildedForm {
        /// <summary>
        /// Data of a poll or a form created in Guilded.
        /// </summary>
        public GuildedForm() =>
            UpdatedAt = null;
        /// <summary>
        /// Author who created this form/poll.
        /// </summary>
        /// <value>Author</value>
        [JsonProperty("createdBy", Required = Required.Always)]
        public GId CreatedBy {
            get; set;
        }
        /// <summary>
        /// When the form/poll was created.
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When the form/poll was created.
        /// </summary>
        /// <value>Creation time</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt {
            get; set;
        }
        /// <summary>
        /// How many people have responded to that form/poll.
        /// </summary>
        /// <value>Form/poll response count</value>
        [JsonProperty("responceCount", Required = Required.Always)]
        public uint ResponseCount {
            get; set;
        }
    }
}