using System.Collections.Generic;
using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// KeyValuePair for RestRequests.
    /// </summary>
    /// <typeparam name="TKey">Key type of the pair</typeparam>
    /// <typeparam name="TValue">Value type of the pair</typeparam>
    public abstract class RestPair<TKey, TValue>: RestValue<TValue> {
        /// <summary>
        /// Key of this pair.
        /// </summary>
        /// <value>Given key</value>
        public TKey Key {
            get; set;
        }
        /// <summary>
        /// A KeyValuePair for RestRequest.
        /// </summary>
        /// <param name="key">Pair key</param>
        /// <param name="value">Pair value</param>
        protected RestPair(TKey key, TValue value): base(value) => Key = key;
        /// <summary>
        /// Casts RestPair to KeyValuePair.
        /// </summary>
        /// <param name="pair">Pair to be casted</param>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <returns>New pair</returns>
        public static implicit operator KeyValuePair<TKey, TValue>(RestPair<TKey, TValue> pair) =>
            new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
    }
}