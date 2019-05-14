using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;

namespace repack.Classes
{
    public static class JObjectUtil
    {
        public static string Search(this JObject obj, string path)
        {
            var tmp = path.Split(".")
                .Aggregate<string, JToken>(null, (current, p) => current == null ? obj[p] : current[p]);
            var result = "";
            if (tmp == null) return result;
            if (tmp is JValue)
            {
                result = tmp.Value<string>();
                result = result.Replace("\\", "\\\\");
                result = result.Replace("\r\n", "\\r\\n");
                result = result.Replace("\n", "\\n");
            }
            else
            {
                result = tmp.ToString();
            }

            return result;
        }
    }
}