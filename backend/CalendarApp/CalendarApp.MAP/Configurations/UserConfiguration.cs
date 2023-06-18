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
    public class UserConfiguration : BaseConfiguration<USER>
    {
        public override void Configure(EntityTypeBuilder<USER> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(15);
            builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11);
            builder.ToTable("Users");

            builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            builder.HasOne<USER_REFRESH_TOKEN>(u => u.RefreshToken)
            .WithOne(rt => rt.User)
            .HasForeignKey<USER_REFRESH_TOKEN>(rt => rt.UserId)
            .IsRequired();
        }
    }
}
