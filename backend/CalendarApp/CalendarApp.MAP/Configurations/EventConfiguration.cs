using CalendarApp.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarApp.MAP.Configurations
{
    public class EventConfiguration : BaseConfiguration<EVENT>
    {
        public override void Configure(EntityTypeBuilder<EVENT> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(50);

            builder.HasOne(e => e.User).WithMany(u => u.Events).HasForeignKey(e => e.UserId);
            builder.ToTable("Events");
        }
    }
}
