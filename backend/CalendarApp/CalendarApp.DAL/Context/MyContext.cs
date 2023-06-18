using CalendarApp.ENTITIES.Models;
using CalendarApp.MAP.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.DAL.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<EVENT> Events { get; set; }
        public DbSet<ROLE> Roles { get; set; }
        public DbSet<USER> Users { get; set; }
        public DbSet<USER_REFRESH_TOKEN> UserRefreshTokens { get; set; }
    }
}
