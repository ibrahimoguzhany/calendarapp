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
    public class RoleConfiguration : BaseConfiguration<ROLE>
    {
        public override void Configure(EntityTypeBuilder<ROLE> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(50);
            builder.ToTable("Roles");
        }
    }
}
