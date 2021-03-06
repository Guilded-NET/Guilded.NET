using RestSharp;

namespace Guilded.NET.API
{
    /// <summary>
    /// Header for RestRequest.
    /// </summary>
    public class Header : RestPair<string, string>
    {
        /// <summary>
        /// Header for RestRequest.
        /// </summary>
        /// <param name="name">Name/Key of the header</param>
        /// <param name="value">Header's value</param>
        /// <returns></returns>
        public Header(string name, string value) : base(name, value) { }
        /// <inheritdoc/>
        public override IRestRequest AddTo(RestRequest req) => req.AddHeader(Key, Value);
    }
}