using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Common
{
    public static class EncryptExtensions
    {
        private static System.Security.Cryptography.MD5 _md5Hash = System.Security.Cryptography.MD5.Create();

        public static string MD5(this string str)
        {
            var data = _md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return data.ToHexString();
        }

        private static System.Security.Cryptography.SHA1 _sha1 = System.Security.Cryptography.SHA1.Create();
        public static string SHA1(this string str)
        {
            var data = _sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return data.ToHexString();
        }


        public static string ToHexString(this byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private const string hexDigits = "0123456789ABCDEF";
        public static byte[] FromHexString(this string hexString)
        {
            byte[] bytes = new byte[hexString.Length >> 1];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                int highDigit = hexDigits.IndexOf(Char.ToUpperInvariant(hexString[i]));
                int lowDigit = hexDigits.IndexOf(Char.ToUpperInvariant(hexString[i + 1]));
                if (highDigit == -1 || lowDigit == -1)
                {
                    throw new ArgumentException("The string contains an invalid digit.", "s");
                }
                bytes[i >> 1] = (byte)((highDigit << 4) | lowDigit);
            }
            return bytes;
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] FromBase64String(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        private static byte[] AES_KEY;
        private static byte[] AES_IV;
        static EncryptExtensions()
        {
            AES_KEY = Encoding.UTF8.GetBytes("loowootechaeskey");
            AES_IV = AES_KEY;
        }

        public static byte[] AESEncrypt(this string plainText)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = AES_KEY;
                aes.IV = AES_IV;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        public static string AESDecrypt(this string encrypted)
        {
            var bytes = Encoding.UTF8.GetBytes(encrypted);
            return bytes.AESDecrypt();
        }

        public static string AESDecrypt(this byte[] encryptedBytes)
        {
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = AES_KEY;
                aes.IV = AES_IV;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
