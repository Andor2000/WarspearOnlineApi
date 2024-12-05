using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Fraction"/>.
    /// </summary>
    public class FractionConfiguration : IEntityTypeConfiguration<wo_Fraction>
    {
        public void Configure(EntityTypeBuilder<wo_Fraction> builder)
        {
            builder.ToTable("wo_Fraction");
            builder.HasKey(m => m.FractionID);

            builder.Property(m => m.FractionID).UseIdentityColumn();
            builder.Property(m => m.FractionCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.FractionName).HasDefaultValue(string.Empty);
        }
    }
}
