using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using repack.Classes;
using repack.Entities;
using repack.Models;

namespace repack.Controllers
{
    // curl https://localhost:5001/webhook/607371638402482b9f0c6e55b2cbc061 -X POST -H "Content-Type: application/json" -d '{"key": "value"}' -k
    [Route("webhook")]
    public class WebHookController : Controller
    {
        private readonly StackModel _stackModel;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public WebHookController(Db db, IHttpClientFactory httpClientFactory)
        {
            _stackModel = new StackModel(db);
            _httpClientFactory = httpClientFactory;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost("{token}")]
        public async Task<ApiResponse> Index(string token, [FromBody] JObject post)
        {
            var result = new ApiResponse();
            var stack = await _stackModel.GetByToken(token);
            if (stack == null)
            {
                result.Error = "Invalid token";
            }
            else
            {
                var webHook = new WebHook(post, _httpClientFactory);
                
                foreach (var task in stack.Tasks)
                {
                    await webHook.Send(JsonConvert.DeserializeObject<TaskContent>(task.Content));
                }
                result.Result = "OK";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static List<(string, string)> GetPropertyName(string text)
        {
            return Regex.Matches(text, "{{(.*)}}").Select(m => (m.Groups[0].Value, m.Groups[1].Value)).ToList();
            
        } 
    }
}