using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LooWooTech.AssetsTrade.Common
{
    using Dict = ConcurrentDictionary<string, LocalCacheService.CacheValue>;
    using HDict = ConcurrentDictionary<string, ConcurrentDictionary<string, LocalCacheService.CacheValue>>;
    
    public class LocalCacheService
    {
        public class CacheValue
        {
            public CacheValue()
            {
                UpdateTime = DateTime.Now;
            }

            public CacheValue(object obj)
                : this()
            {
                Value = JsonConvert.SerializeObject(obj);
            }

            public DateTime UpdateTime
            {
                get;
                set;
            }

            public string Value { get; set; }
            public TimeSpan? Expiry { get; set; }

            public bool HasExpired()
            {
                if (Expiry.HasValue)
                {
                    return (UpdateTime - DateTime.Now) > Expiry.Value;
                }
                return false;
            }

            public T GetObject<T>()
            {
                return JsonConvert.DeserializeObject<T>(Value);
            }
        }

        private static readonly Dict Data = new Dict();
        private static readonly HDict HData = new HDict();

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            var val = new CacheValue(value) { Expiry = expiry };
            Data.AddOrUpdate(key, val, (k, v) =>
            {
                v.Value = val.Value;
                v.UpdateTime = DateTime.Now;
                return v;
            });
        }

        public T Get<T>(string key)
        {
            CacheValue val = null;
            if (Data.TryGetValue(key, out val))
            {
                if (!val.HasExpired())
                {
                    return val.GetObject<T>();
                }
            }
            return default(T);
        }

        public void HSet(string hashId, string key, object value)
        {
            if (!HData.ContainsKey(hashId))
            {
                HData.TryAdd(hashId, new Dict());
            }
            var hValue = HData[hashId];
            var json = JsonConvert.SerializeObject(value);

            hValue.AddOrUpdate(key, new CacheValue { Value = json }, (k, v) =>
            {
                v.Value = JsonConvert.SerializeObject(value);
                v.UpdateTime = DateTime.Now;
                return v;
            });
        }

        public T HGet<T>(string hashId, string key)
        {
            Dict hValue;
            if (HData.TryGetValue(hashId, out hValue))
            {
                CacheValue val = null;
                if (hValue.TryGetValue(key, out val))
                {
                    return val.GetObject<T>();
                }
            }
            return default(T);
        }

        public void Remove(string key)
        {
            CacheValue val;
            if (!Data.TryRemove(key, out val))
            {
                Dict hVal;
                HData.TryRemove(key, out hVal);
            }
        }

        public void Clear()
        {
            Data.Clear();
            HData.Clear();
        }

        public bool Exists(string key)
        {
            return Data.ContainsKey(key) || HData.ContainsKey(key);
        }

        public void HRemove(string hashId, string key)
        {
            Dict hVal;
            if (HData.TryGetValue(hashId, out hVal))
            {
                CacheValue val;
                hVal.TryRemove(key, out val);
            }
        }


        public List<T> HGetAll<T>(string hashId)
        {
            Dict hVal;
            if (HData.TryGetValue(hashId, out hVal))
            {
                return hVal.Select(kv => kv.Value.GetObject<T>()).ToList();
            }
            return new List<T>();
        }


        public void HSetAll<T>(string hashId, Dictionary<string, T> dict)
        {
            foreach (var kv in dict)
            {
                HSet(hashId, kv.Key, kv.Value);
            }
        }
    }


    public static class CacheServiceExtensions
    {
        public static T GetOrSet<T>(this LocalCacheService cache, string key, Func<T> getValueFunc, TimeSpan? expiry = null)
        {
            var val = cache.Get<T>(key);
            if (val == null)
            {
                val = getValueFunc();
                cache.Set(key, val, expiry);
            }
            return val;
        }

        public static T HGetOrSet<T>(this LocalCacheService cache, string hashId, string key, Func<T> getValueFunc)
        {
            var val = cache.HGet<T>(hashId, key);
            if (val.Equals(default(T)))
            {
                val = getValueFunc();
                cache.HSet(hashId, key, val);
            }
            return val;
        }

    }
}
