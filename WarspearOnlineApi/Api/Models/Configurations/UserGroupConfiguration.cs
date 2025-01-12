using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<wo_UserGroup>
    {
        public void Configure(EntityTypeBuilder<wo_UserGroup> builder)
        {
            builder.ToTable("wo_UserGroup");
            builder.HasKey(m => m.UserGroupID);

            builder.Property(m => m.rf_UserID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_User).WithMany(x => x.UserGroups).HasForeignKey(x => x.rf_UserID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_GroupID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Group).WithMany(x => x.UserGroups).HasForeignKey(x => x.rf_GroupID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
