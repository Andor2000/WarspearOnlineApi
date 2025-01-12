using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Configurations.Users
{
    public class AccessLevelRoleConfiguration : IEntityTypeConfiguration<wo_AccessLevelRole>
    {
        public void Configure(EntityTypeBuilder<wo_AccessLevelRole> builder)
        {
            builder.ToTable("wo_AccessLevelRole");
            builder.HasKey(m => m.AccessLevelRoleID);

            builder.Property(m => m.AccessLevelRoleID).UseIdentityColumn();

            builder.Property(m => m.rf_AccessLevelID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_AccessLevel).WithMany(x => x.AccessLevelRoles).HasForeignKey(x => x.rf_AccessLevelID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_RoleID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Role).WithMany(x => x.AccessLevelRoles).HasForeignKey(x => x.rf_RoleID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
