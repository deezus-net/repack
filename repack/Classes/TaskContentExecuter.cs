using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace repack.Classes
{
    public class TaskContentExecuter
    {
        protected JObject Post { get; }

        public TaskContentExecuter(JObject post)
        {
            Post = post;
        }

        protected static List<(string, string)> GetPropertyName(string text)
        {
            return Regex.Matches(text, "{{(.*)}}").Select(m => (m.Groups[0].Value, m.Groups[1].Value)).ToList();

        }
    }
}