using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UnAuthorization
{
    public class RequestCaller
    {

        private BlackHouse blackHouse;
        private CustomHttpClient client;
        public RequestCaller()
        {
            blackHouse = new BlackHouse();
            client = new CustomHttpClient();
        }
        public void getVehicle(MockRequest request)
        {
            var key = GenerateKey(request);
            if (blackHouse.CheckInBlack(key))
            {
                return;
            }
            var response = Call(request.Url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    RefreshToken();
                }
                blackHouse.Handle(key, request);
            }
            else
            {
                blackHouse.Remove(key);
            }
        }
        private void RefreshToken()
        {
            Console.WriteLine("refresh_token");
        }

        private HttpResponseMessage Call(string url)
        {
            var response = client.Get(url).Result;
            return response;
        }

        private static string GenerateKey(MockRequest request)
        {
            return request.Method + request.Url;
        }

    }

}