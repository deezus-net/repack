using Microsoft.EntityFrameworkCore;

namespace repack.Entities
{
    public class Db : DbContext
    {
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<Task>Tasks { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
 
        }
    }
}