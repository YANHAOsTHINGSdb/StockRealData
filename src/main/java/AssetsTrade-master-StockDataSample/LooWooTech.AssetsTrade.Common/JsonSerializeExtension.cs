using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Common
{
    public static class JsonSerializeExtension
    {
        public static string JsonSerialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T JsonDeserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
