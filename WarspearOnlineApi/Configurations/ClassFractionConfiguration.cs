using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    public class ClassFractionConfiguration : IEntityTypeConfiguration<wo_ClassFraction>
    {
        public void Configure(EntityTypeBuilder<wo_ClassFraction> builder)
        {
            builder.ToTable("wo_ClassFraction");
            builder.HasKey(m => m.ClassFractionID);

            builder.Property(m => m.ClassFractionID).UseIdentityColumn();

            builder.Property(m => m.rf_ClassID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Class).WithMany(x => x.ClassFractions).HasForeignKey(x => x.rf_ClassID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.ClassFractions).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
