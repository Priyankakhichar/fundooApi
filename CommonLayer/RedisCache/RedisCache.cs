using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RedisCache
{
    public class RedisCache
    {
        public dynamic GetRedis(string key)
        {
            using(var redis = new RedisClient())
            {
               var result = System.Text.Encoding.UTF8.GetString(redis.Get(key)).ToString().Replace("\"", "");
                return result;
            }
        }
    }
}
