using RestSharp;

namespace Guilded;

internal static class RequestUtil
{
    internal static RestRequest AddOptionalQuery<T>(this RestRequest request, string name, T? value, bool encode = true) where T : struct
    {
        if (value is not null) request.AddQueryParameter(name, (T)value, encode);
        return request;
    }
}