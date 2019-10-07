using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;

namespace repack.Classes
{
    public static class JObjectUtil
    {
        public static string Search(this JObject obj, string path)
        {
            JToken tmp = obj;
            foreach (var propName in path.Split("."))
            {
                var m = Regex.Match(propName, @"\[(.*)\]");
                if (m.Success)
                {
                    int.TryParse(m.Groups[1].Value, out var index);
                    tmp = tmp[propName.Replace(m.Value, "")][index];
                }
                else
                {
                    tmp = tmp[propName];
                }
            }
            
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