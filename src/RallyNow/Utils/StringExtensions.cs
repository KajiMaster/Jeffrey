using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace RallyNow.Utils
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format))
               throw new Exception("String parsing error");
            return args != null ? string.Format(format, args) : string.Format("{0}".With(format));
        }

        public static T ConvertTo<T>(this string json, JsonSerializerSettings settings = null)
        {
            return settings == null
                ? JsonConvert.DeserializeObject<T>(json)
                : JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static Stream ToStream(this string item)
        {
            var outBytes = Encoding.ASCII.GetBytes(item);
            var stream = new MemoryStream();
            stream.Write(outBytes, 0, outBytes.Length);
            stream.Position = 0;

            return stream;
        }

        public static string Wordify(this string pascalCaseString)
        {
            var regex = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return regex.Replace(pascalCaseString, " ${x}");
        }

       
    }
}