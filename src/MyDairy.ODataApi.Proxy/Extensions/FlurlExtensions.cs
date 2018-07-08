using Flurl.Http;
using MyDiary.ODataApi.Proxy.Models;
using System;
using System.Threading.Tasks;

namespace MyDiary.ODataApi.Proxy.Extensions
{
    public static class FlurlClientExtensions
    {
        public static async Task<T> GetODataValueAsync<T>(this FlurlClient client)
        {

            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

           var odataResult = await client.GetJsonAsync<ODataClientResult<T>>().ConfigureAwait(false);

           return odataResult.Value;

        }
    }
}