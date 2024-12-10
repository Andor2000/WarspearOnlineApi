using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Configurations.Users
{
    public class AccessLevelConfiguration : IEntityTypeConfiguration<wo_AccessLevel>
    {
        public void Configure(EntityTypeBuilder<wo_AccessLevel> builder)
        {
            builder.ToTable("wo_AccessLevel");
            builder.HasKey(m => m.AccessLevelID);

            builder.Property(m => m.AccessLevelID).UseIdentityColumn();
            builder.Property(m => m.AccessLevelCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.AccessLevelName).HasDefaultValue(string.Empty);
            builder.Property(m => m.AccessLevelInt).HasDefaultValue(0);

            builder.Property(m => m.rf_ParentAccessLevelID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_ParentAccessLevel).WithMany(x => x.ChildAccessLevels).HasForeignKey(x => x.rf_ParentAccessLevelID).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
