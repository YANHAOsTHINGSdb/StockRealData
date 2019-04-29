using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Common
{
    public static class XmlSerializeExtension
    {
        public static string XmlSerialize<T>(this T obj)
        {
            return Xml.Net.XmlConvert.SerializeObject(obj);
        }

        public static T XmlDeserialize<T>(this string xml) where T : new()
        {
            return Xml.Net.XmlConvert.DeserializeObject<T>(xml);
        }
    }
}
