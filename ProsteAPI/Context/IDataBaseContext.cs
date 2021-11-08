using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProsteAPI.Entities.User;
using ProsteAPI.Entities.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Context
{
    public interface IDataBaseContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<UserPermissionDbModel> Permits { get; set; }

        public int SaveChanges();
        public ChangeTracker ChangeTracker { get; }
    }
}
