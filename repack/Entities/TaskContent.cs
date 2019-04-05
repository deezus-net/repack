using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repack.Entities
{
    public class TaskContent
    {
        public string Type { get; set; }
        
        public string ConditionKey { get; set; }
        public string ConditionType { get; set; }
        public string ConditionValue { get; set; }
        
        [Required(ErrorMessage = "RequiredTaskURL")]
        public string Url { get; set; }
        public string Method { get; set; }
        [Required(ErrorMessage = "RequiredTaskRequestBody")]
        public string Body { get; set; }

    }
}