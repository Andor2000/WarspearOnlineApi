using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<wo_Class>
    {
        public void Configure(EntityTypeBuilder<wo_Class> builder)
        {
            builder.ToTable("wo_Class");
            builder.HasKey(m => m.ClassID);

            builder.Property(m => m.ClassID).UseIdentityColumn();
            builder.Property(m => m.ClassCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.ClassName).HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Classes).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
