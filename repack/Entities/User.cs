using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace repack.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
        
        [Column("modified")]
        public DateTime Modified { get; set; }
        
        [Column("created")]
        public  DateTime Created { get; set; }
    }
}