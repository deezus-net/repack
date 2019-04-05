using Microsoft.EntityFrameworkCore;
using Npgsql;
using repack.Entities;

namespace repack.Test
{
    public class BaseTest
    {
        protected Db Db
        {
            get
            {
                if (_db != null) return _db;
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = TestSettings.RepackDbHost,
                    Port = TestSettings.RepackDbPort,
                    Username = TestSettings.RepackDbUser,
                    Password = TestSettings.RepackDbPassword,
                    Database = TestSettings.RepackDbName
                };
                var connectionString = builder.ToString();
                _db = new Db(new DbContextOptionsBuilder().UseNpgsql(connectionString).Options);

                return _db;
            }
        }

        protected AppSetting AppSetting
        {
            get
            {
                if (_appSetting != null) return _appSetting;
                _appSetting = new AppSetting()
                {
                    Salt = TestSettings.RepackSalt,
                    AdminId = TestSettings.RepackAdminId,
                    AdminPassword = TestSettings.RepackAdminPassword
                };
                return _appSetting;
            }
        }

        private AppSetting _appSetting;

        private Db _db;
    }
}