using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Models.Entity.Users;

namespace WarspearOnlineApi.Configurations.Users
{
    public class RoleConfiguration : IEntityTypeConfiguration<wo_Role>
    {
        public void Configure(EntityTypeBuilder<wo_Role> builder)
        {
            builder.ToTable("wo_Role");
            builder.HasKey(m => m.RoleID);

            builder.Property(m => m.RoleID).UseIdentityColumn();
            builder.Property(m => m.RoleCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.RoleName).HasDefaultValue(string.Empty);
        }
    }
}
