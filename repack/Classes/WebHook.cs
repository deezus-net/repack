using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using repack.Entities;

namespace repack.Classes
{
    public class WebHook : TaskContentExecuter
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebHook(JObject post, IHttpClientFactory httpClientFactory) : base(post)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<(string, HttpResponseMessage)> Send(TaskContent content)
        {
            if (!CheckCondition(content))
            {
                return (null, null);
            }


            var body = content.Body;
            foreach (var (text, path) in GetPropertyName(content.Body))
            {
                var value = Post.Search(path);
                body = body.Replace(text, value);
            }

            if (string.IsNullOrEmpty(content.Url) || string.IsNullOrEmpty(body)) return (body, null);
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                return (body,
                    await httpClient.PostAsync(content.Url,
                        new StringContent(body, Encoding.UTF8, "application/json")));
            }
            catch (Exception e)
            {
            }


            return (body, null);
        }

        private bool CheckCondition(TaskContent content)
        {
            if (string.IsNullOrWhiteSpace(content.ConditionKey) || string.IsNullOrWhiteSpace(content.ConditionType) ||
                string.IsNullOrWhiteSpace(content.ConditionValue))
            {
                return true;
            }

            var (text, path) = GetPropertyName(content.ConditionKey).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            var value = Post.Search(path);
            switch (content.ConditionType)
            {
                case Define.TaskConditionType.Equal:
                    return value == content.ConditionValue;
                    break;
                case Define.TaskConditionType.NotEqual:
                    return value != content.ConditionValue;
                    break;
                default:
                    return true;
            }
        }
    }
}