
using System.Threading.Tasks;

namespace RestClient.Client
{
    interface IRestClient
    {
        Task<T> Get<T>(string url, params string[] args);
    }
}
