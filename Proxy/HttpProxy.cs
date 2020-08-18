using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnAuthorization
{
    public class CustomHttpClient
    {
        public async Task<HttpResponseMessage> Get(string url)
        {
            var client = CreateHttpClient();
            return await client.GetAsync(url);
        }

        static HttpClient CreateHttpClient(){
            var client = new HttpClient();
            return client;
        }
    }

}