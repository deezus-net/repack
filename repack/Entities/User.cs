using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace repack.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
        
        [Column("modified")]
        public DateTime Modified { get; set; }
        
        [Column("created")]
        public  DateTime Created { get; set; }
        
        [NotMapped]
        public bool IsAdmin { get; set; }

    }
}