﻿using RallyNow.Service.Utils;

namespace RallyNow.Service.Utils
{
    public interface IRestfulClient
    {
        IRestfulResponse Get(string uri);
        IRestfulResponse Put(string uri, object obj);
        IRestfulResponse Post(string uri, object obj);
    }
}