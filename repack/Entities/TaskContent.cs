using System.ComponentModel.DataAnnotations;

namespace repack.Entities
{
    public class TaskContent
    {
        public string Type { get; set; }
        [Required]
        public string Url { get; set; }
        public string Method { get; set; }
        [Required]
        public string Body { get; set; }
    }
}