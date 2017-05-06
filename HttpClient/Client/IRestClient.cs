using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestClient.Client
{
    interface IRestClient
    {
        Task<T> Get<T>(string url, params string[] args);
    }
}
