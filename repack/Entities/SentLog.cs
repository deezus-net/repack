using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("sent_logs")]
    public class SentLog
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("task_id")]
        public int TaskId { get; set; }
        
        [Column("url")]
        public string Url { get; set; }
        
        [Column("content")]
        public string Content { get; set; }
        
        [Column("response")]
        public string Response { get; set; }
        
        [Column("created")]
        public DateTime Created { get; set; }
    }
}