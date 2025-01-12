using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Models.Configurations
{
    public class DropStatusConfiguration : IEntityTypeConfiguration<wo_DropStatus>
    {
        public void Configure(EntityTypeBuilder<wo_DropStatus> builder)
        {
            builder.ToTable("wo_DropStatus");
            builder.HasKey(m => m.DropStatusID);

            builder.Property(m => m.DropStatusID).UseIdentityColumn();
            builder.Property(m => m.DropStatusCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.DropStatusName).HasDefaultValue(string.Empty);
        }
    }
}
