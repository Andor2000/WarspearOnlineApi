using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Models.Entity.Users;

namespace WarspearOnlineApi.Configurations.Users
{
    public class AccessLevelConfiguration : IEntityTypeConfiguration<wo_AccessLevel>
    {
        public void Configure(EntityTypeBuilder<wo_AccessLevel> builder)
        {
            builder.ToTable("wo_AccessLevelф");
            builder.HasKey(m => m.AccessLevelID);

            builder.Property(m => m.AccessLevelID).UseIdentityColumn();
            builder.Property(m => m.AccessLevelName).HasDefaultValue(string.Empty);
        }
    }
}
