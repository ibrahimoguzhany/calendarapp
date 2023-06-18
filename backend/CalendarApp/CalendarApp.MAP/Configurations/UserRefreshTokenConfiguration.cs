using CalendarApp.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalendarApp.MAP.Configurations
{
    public class UserRefreshTokenConfiguration : BaseConfiguration<USER_REFRESH_TOKEN>
    {
        public override void Configure(EntityTypeBuilder<USER_REFRESH_TOKEN> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.ToTable("UserRefreshTokens");

            builder.HasOne<USER>(rt => rt.User)
             .WithOne(u => u.RefreshToken)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
