using System.Linq;
using Newtonsoft.Json.Linq;

namespace repack.Classes
{
    public static class JObjectUtil
    {
        public static string Search(this JObject obj, string path)
        {
            var tmp = path.Split(".")
                .Aggregate<string, JToken>(null, (current, p) => current == null ? obj[p] : current[p]);

            var result = tmp?.Value<string>() ?? "";
            result = result.Replace("\\", "\\\\");
            result = result.Replace("\r\n", "\\r\\n");
            result = result.Replace("\n", "\\n");

            return result;
        }
    }
}