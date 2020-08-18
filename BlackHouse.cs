using System;
using System.Collections.Generic;

namespace UnAuthorization
{
    public class BlackHouse
    {
        private TimeSpan STAYTIME = TimeSpan.FromMinutes(10.0);
        private TimeSpan CONFIRMTIME = TimeSpan.FromMinutes(5.0);
        private static Dictionary<string, MockRequest> children = new Dictionary<string, MockRequest>();
        public void Add(string key, MockRequest child)
        {
            children.Add(key, child);
        }

        //当接口调用成功时
        public void Remove(string key)
        {
            if (children.ContainsKey(key))
            {
                children.Remove(key);
            }
            children.Remove(key);
        }

        public bool CheckInBlack(string key)
        {
            if (children.ContainsKey(key))
            {
                if (children[key].IsConfirmed)
                {
                    if (children[key].ExpireTime >= DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        children.Remove(key);
                    }
                }
            }
            return false;
        }



        //是否要refresh_token
        public void Handle(string key, MockRequest request)
        {
            if (children.ContainsKey(key))
            {
                if (DateTime.Now - children[key].AddTime < CONFIRMTIME)
                {
                    children[key].IsConfirmed = true;
                    children[key].ExpireTime = DateTime.Now.Add(STAYTIME);
                }
            }
            else
            {
                request.AddTime = DateTime.Now;
                children.Add(key, request);
            }
        }
    }

    public class MockRequest
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool IsConfirmed { get; set; }

    }
}