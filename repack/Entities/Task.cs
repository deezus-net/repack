using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("tasks")]
    public class Task
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [ForeignKey("Stack")]
        [Column("stack_id")]
        public int StackId { get; set; }
        
        [Required(ErrorMessage = "RequiredTaskName")]
        [Column("name")]
        public string Name { get; set; }
        
        [Column("order_no")]
        public int OrderNo { get; set; }
        
        [Column("content")]
        public string Content { get; set; }
        
        [Column("enabled")]
        public bool Enabled { get; set; }
        
        [Column("modified")]
        public DateTime Modified { get; set; }
        
        [Column("created")]
        public DateTime Created { get; set; }
        
        public Stack Stack { get; set; }
   

    }
}