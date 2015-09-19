using System;
using System.Net;
using Newtonsoft.Json;
using RallyNow.Service.Utils;
using RestSharp;

namespace RallyNow.Service.Utils
{
    public class RestfulResponse : IRestfulResponse
    {
        private readonly IRestResponse _response;

        public RestfulResponse(IRestResponse response, string uri)
        {
            Uri = uri;
            _response = response;
        }

        public string Uri { get; }

        public string Content
        {
            get { return _response.Content; }
        }

        public byte[] RawBytes
        {
            get { return _response.RawBytes; }
        }

        public string ContentType
        {
            get { return _response.ContentType; }
        }

        public bool IsStatusOk
        {
            get { return StatusCode == HttpStatusCode.OK; }
        }


        public HttpStatusCode StatusCode
        {
            get { return _response.StatusCode; }
        }

        public string ErrorMessage
        {
            get { return _response.ErrorMessage; }
        }

        public Exception ErrorException
        {
            get { return _response.ErrorException; }
        }

        public T GetContent<T>()
        {
            return JsonConvert.DeserializeObject<T>(_response.Content);
        }
    }
}