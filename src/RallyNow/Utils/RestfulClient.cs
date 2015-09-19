using System;
using RestSharp;

namespace RallyNow.Utils
{
    public class RestfulClient :  IRestfulClient
    {
        private readonly IRestClient _client;

        public RestfulClient(IRestClient client)
        {
            _client = client;
        }

        public IRestfulResponse Get(string uri)
        {
            var request = new RestRequest(uri, Method.GET);
            var response = new RestfulResponse(_client.Execute(request), uri);
            if (!response.IsStatusOk)
                LogError(response);

            return response;
        }

        public IRestfulResponse Put(string uri, object obj)
        {
            var request = new RestRequest(uri, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(obj);
            var response = new RestfulResponse(_client.Execute(request), uri);
            if (!response.IsStatusOk)
                LogError(response);

            return response;
        }
        public IRestfulResponse Post(string uri, object obj)
        {
            var request = new RestRequest(uri, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(obj);
            var response = new RestfulResponse(_client.Execute(request), uri);
            if (!response.IsStatusOk)
                LogError(response);

            return response;
        }

        private void LogError(IRestfulResponse response)
        {
            Console.Out.WriteLine("There was an error getting data from '{0}'. StatusCode: {1}, ErrorMessage: {2}, Exception: {3}".With(response.Uri, response.StatusCode, response.ErrorMessage, response.ErrorException));
        }
    }
}