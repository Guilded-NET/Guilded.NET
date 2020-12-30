using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Value for RestRequests.
    /// </summary>
    /// <typeparam name="T">Value's type</typeparam>
    public abstract class RestValue<T>: IReqAddable {
        /// <summary>
        /// Value of the request object.
        /// </summary>
        /// <value>Given value</value>
        public T Value {
            get; set;
        }
        /// <summary>
        /// Value for RestRequest.
        /// </summary>
        /// <param name="value">Pair value</param>
        protected RestValue(T value) => Value = value;
        /// <inheritdoc/>
        public abstract IRestRequest AddTo(RestRequest req);
        /// <summary>
        /// Casts RestPair to KeyValuePair.
        /// </summary>
        /// <param name="restvalue">Pair to be casted</param>
        /// <returns>New pair</returns>
        public static implicit operator T(RestValue<T> restvalue) =>
            restvalue.Value;
    }
}