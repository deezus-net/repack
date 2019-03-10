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

            return tmp?.Value<string>() ?? "";
        }
    }
}