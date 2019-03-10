using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("stacks")]
    public class Stack
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("token")]
        public string Token { get; set; }
        
        [Column("modified")]
        public DateTime Modified { get; set; }
        
        [Column("created")]
        public DateTime Created { get; set; }
        
        public List<Task> Tasks { get; set; }
        
        public List<ReceivedLog> ReceivedLogs { get; set; }
    }
}