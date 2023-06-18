using CalendarApp.ENTITIES.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.MAP.Configurations
{
    public class UserRoleConfiguration : BaseConfiguration<USER_ROLE>
    {
        public override void Configure(EntityTypeBuilder<USER_ROLE> builder)
        {
            base.Configure(builder);
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            builder.ToTable("UserRoles");
        }
    }
}
