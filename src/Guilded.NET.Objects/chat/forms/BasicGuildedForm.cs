using Newtonsoft.Json;

namespace Guilded.NET.Objects.Forms {
    /// <summary>
    /// A poll or a form created in Guilded.
    /// </summary>
    public class BasicGuildedForm: ClientObject {
        /// <summary>
        /// A poll or a form created in Guilded.
        /// </summary>
        public BasicGuildedForm() =>
            (TeamId, IsPublic) = (null, true);
        /// <summary>
        /// ID of the team where the form/poll is in.
        /// </summary>
        /// <value>Optional team ID</value>
        [JsonProperty("teamId")]
        public GId TeamId {
            get; set;
        }
        /// <summary>
        /// Title of the form/poll.
        /// </summary>
        /// <value>Title</value>
        [JsonProperty("title", Required = Required.Always)]
        public string Title {
            get; set;
        }
        /// <summary>
        /// Description of the form/poll describing the form/poll.
        /// </summary>
        /// <value>Description</value>
        [JsonProperty("description", Required = Required.Always)]
        public string Description {
            get; set;
        }
        /// <summary>
        /// If the form/poll is publicly available.
        /// </summary>
        /// <value>Public</value>
        [JsonProperty("isPublic")]
        public bool IsPublic {
            get; set;
        }
        /// <summary>
        /// Type of the form(form, poll).
        /// </summary>
        /// <value>Form type</value>
        [JsonProperty("type", Required = Required.Always)]
        public FormType Type {
            get; set;
        }
    }
}