using System;

using Newtonsoft.Json;

namespace Guilded.NET.Objects
{
    /// <summary>
    /// Interface for DM channels, normal channels and categories.
    /// </summary>
    public abstract class BaseChannel : ClientObject
    {
        /// <summary>
        /// When the channel was created.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("createdAt", Required = Required.Always)]
        public DateTime CreatedAt
        {
            get; set;
        }
        /// <summary>
        /// When the channel was updated.
        /// </summary>
        /// <value>Date</value>
        [JsonProperty("updatedAt", Required = Required.AllowNull)]
        public DateTime? UpdatedAt
        {
            get; set;
        }
    }
    /// <summary>
    /// Interface for DM channels, normal channels and categories.
    /// </summary>
    /// <typeparam name="T">Type of channel's ID</typeparam>
    public class BaseChannel<T> : BaseChannel
    {
        /// <summary>
        /// ID of this channel.
        /// </summary>
        /// <value>Channel ID</value>
        [JsonProperty("id", Required = Required.Always)]
        public T Id
        {
            get; set;
        }
        /// <summary>
        /// Gets channel hashcode.
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode() => Id.GetHashCode() - 21;
    }
}