using System;
using System.Net;

namespace RallyNow.Service.Utils
{
    public interface IRestfulResponse
    {
        byte[] RawBytes { get; }
        string ContentType { get; }
        string Content { get; }
        string Uri { get; }
        bool IsStatusOk { get; }
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
        Exception ErrorException { get; }
        T GetContent<T>();
    }
}