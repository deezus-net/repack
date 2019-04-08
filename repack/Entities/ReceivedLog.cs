using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("received_logs")]
    public class ReceivedLog
    {
        [Column("id")]
        public int Id { get; set; }
        
        [ForeignKey("Stack")]
        [Column("stack_id")]
        public int StackId { get; set; }
        
        [Column("method")]
        public string Method { get; set; }
        
        [Column("header")]
        public string Header { get; set; }
        
        [Column("body")]
        public string Body { get; set; }
        
        [Column("created")]
        public DateTime Created { get; set; }
        
        public Stack Stack { get; set; }
    }
}