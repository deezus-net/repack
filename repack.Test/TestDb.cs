using Microsoft.EntityFrameworkCore;
using Npgsql;
using repack.Entities;

namespace repack.Test
{
    public class TestDb
    {
        public Db Db { get; private set; }

        public TestDb()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Environment.RepackDbHost,
                Port = Environment.RepackDbPort,
                Username = Environment.RepackDbUser,
                Password = Environment.RepackDbPassword,
                Database = Environment.RepackDbName
            };
            var connectionString = builder.ToString();

            var option = new DbContextOptionsBuilder<Db>()
                .UseNpgsql(connectionString);
            Db = new Db(option.Options);

        }
    }
}