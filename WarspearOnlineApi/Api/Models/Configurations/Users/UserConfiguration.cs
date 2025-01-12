using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Configurations.Users
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
            builder.Property(m => m.UserName).HasDefaultValue(string.Empty);
            builder.Property(m => m.RangeAccessLevel).HasDefaultValue(0);

            builder.Property(m => m.rf_AccessLevelID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_AccessLevel).WithMany(x => x.Users).HasForeignKey(x => x.rf_AccessLevelID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_ServerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Users).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Users).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
