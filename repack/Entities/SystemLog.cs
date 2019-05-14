using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("system_logs")]
    public class SystemLog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("type")]
        public string Type { get; set; }
        
        [Column("message")]
        public string Message { get; set; }
        
        [Column("created")]
        public DateTime Created { get; set; }
    }
}