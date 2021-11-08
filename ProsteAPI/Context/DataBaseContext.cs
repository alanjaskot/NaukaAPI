using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProsteAPI.Entities.User;
using ProsteAPI.Entities.UserPermission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Context
{
    public class DataBaseContext: DbContext, IDataBaseContext
    {

        public DataBaseContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<UserPermissionDbModel> Permits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbName = "sqldb.db";
            string address = AppDomain.CurrentDomain.BaseDirectory;
            string _db = Path.Combine(address, dbName);

            options.UseSqlite($"Data source = {_db}");
        }

        public override ChangeTracker ChangeTracker
        {
            get
            {
                return base.ChangeTracker;
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
