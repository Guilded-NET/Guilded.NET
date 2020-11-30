using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Object addable to RestRequest.
    /// </summary>
    public interface IReqAddable {
        /// <summary>
        /// Adds this to RestRequest.
        /// </summary>
        /// <param name="client">API Request</param>
        /// <returns>Given RestRequest</returns>
        IRestRequest AddTo(RestRequest req);
    }
}