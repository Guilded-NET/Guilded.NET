using System.Collections.Generic;
using System;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Interface for DM channels, normal channels and categories.
    /// </summary>
    public interface IChannel {
        /// <summary>
        /// Priority of this channel.
        /// </summary>
        /// <value>Priority</value>
        uint? Priority {
            get; set;
        }
        /// <summary>
        /// Name of this channel.
        /// </summary>
        /// <value>Name</value>
        string Name {
            get; set;
        }
        /// <summary>
        /// When the channel was created.
        /// </summary>
        /// <value>Date</value>
        DateTime CreatedAt {
            get; set;
        }
        /// <summary>
        /// When the channel was updated.
        /// </summary>
        /// <value>Date</value>
        DateTime? UpdatedAt {
            get; set;
        }
    }
}