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
               var result =  redis.Get(key);
                return result;
            }
        }
    }
}
