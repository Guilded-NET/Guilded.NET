using System.Collections.Generic;

using Newtonsoft.Json;

namespace Guilded.NET.Base.Chat
{
    /// <summary>
    /// A form or a poll that can be used to submit applications or be used for voting.
    /// </summary>
    public class Form : ContainerNode<TextContainer, Form>
    {
        #region Properties
        /// <summary>
        /// The identifier of this form/poll.
        /// </summary>
        /// <value>Form ID</value>
        [JsonIgnore]
        public uint? FormId
        {
            get => Data.FormId;
            set => Data.FormId = value;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// A form or a poll that can be used to submit applications or be used for voting.
        /// </summary>
        public Form(uint formId) : base(NodeType.Form, ElementType.Block, new TextContainer("")) =>
            FormId = formId;
        #endregion
    }
}