using System;
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
        public async Task<bool> Send(TaskContent content)
        {
            var result = false;
            var body = content.Body;
            foreach (var (text, path) in GetPropertyName(content.Body))
            {
                var value = Post.Search(path);
                body = body.Replace(text, value);
            }

            if (string.IsNullOrEmpty(content.Url) || string.IsNullOrEmpty(body)) return result;
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var res = await httpClient.PostAsync(content.Url, new StringContent(body, Encoding.UTF8, "application/json"));
                if (res.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                }
            }
            catch (Exception e)
            {
            }


            return result;
        }
    }
}