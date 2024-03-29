﻿using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace RestClient.Client
{
    public class Client
    {
        private DataContractJsonSerializer _serializer;
        private HttpClient _client;
        private string _baseurl;

        public Client(string baseurl)
        {
            _baseurl = baseurl;
            _client = new HttpClient();
        }

        public async Task<T> Get<T>(string suburl, params string[] args)
            where T : class, IHttpObject
        {
            _serializer = new DataContractJsonSerializer(typeof(T));
            var geturl = createUrl(suburl, args);
            var response = await processResponse<T>(
                () =>
                {
                    return _client.GetStreamAsync(geturl);
                });
            return response;
        }

        private string createUrl(string suburl, string[] args)
        {
            var url = _baseurl + $"/{suburl}/";
            foreach (var item in args)
            {
                url += $"{item}/";
            }
            return url;
        }

        private async Task<T> processResponse<T>(Func<Task<Stream>> method)
            where T : class, IHttpObject
        {

            _client.DefaultRequestHeaders.Accept.Clear();

            var resultTask = method();

            var obj =  _serializer.ReadObject(await resultTask) as T;
            obj.Initialize();
            return obj;

        }
    }
}
