using Microsoft.EntityFrameworkCore;

namespace repack.Entities
{
    public class Db : DbContext
    {
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<Task>Tasks { get; set; }
        public DbSet<ReceivedLog> ReceivedLogs { get; set; }
        public DbSet<SentLog> SentLogs { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<SystemLog> SystemLogs { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
 
        }
    }
}