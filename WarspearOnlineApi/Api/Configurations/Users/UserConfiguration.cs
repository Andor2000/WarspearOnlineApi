using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<wo_User>
    {
        public void Configure(EntityTypeBuilder<wo_User> builder)
        {
            builder.ToTable("wo_User");
            builder.HasKey(m => m.UserId);

            builder.Property(m => m.UserId).UseIdentityColumn();
            builder.Property(m => m.Login).HasDefaultValue(string.Empty);
            builder.Property(m => m.Password).HasDefaultValue(string.Empty);
            builder.Property(m => m.RangeAccessLevel).HasDefaultValue(0);

            builder.Property(m => m.rf_AccessLevelID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_AccessLevel).WithMany(x => x.Users).HasForeignKey(x => x.rf_AccessLevelID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
