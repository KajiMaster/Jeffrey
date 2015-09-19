using System;
using System.Collections.Generic;
using RallyNow.Service.Utils;
using RestSharp;

namespace RallyNow.Service
{
    public class MoxtraService
    {
        private IRestfulClient _client;

        public MoxtraService(string moxtraUri)
        {
            _client = new RestfulClient(new RestClient(moxtraUri));
        }

        public void Login()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id", "hhCr3RxKPmo");
            parameters.Add("client_secret", "mI-Dc2ftJ0w");
            parameters.Add("grant_type", "http://www.moxtra.com/auth_uniqueid");
            parameters.Add("uniqueid", "Jeffrey@Rallyteam.com");
            parameters.Add("timestamp", (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString());
            parameters.Add("firstname", "Jeffrey");
            parameters.Add("lastname", "Rallyteam");
            var response = _client.FormPost("oauth/token", parameters);
            Console.Out.WriteLine(response.Content);
        }

        private class Moxtra
        {
            public string ClientId => "hhCr3RxKPmo";
            public string ClientSecret => "mI-Dc2ftJ0w";
        }
    }
}