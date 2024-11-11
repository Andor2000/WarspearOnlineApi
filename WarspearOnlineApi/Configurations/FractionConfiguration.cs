using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="FractionEntity"/>.
    /// </summary>
    public class FractionConfiguration : IEntityTypeConfiguration<FractionEntity>
    {
        public void Configure(EntityTypeBuilder<FractionEntity> builder)
        {
            builder.ToTable("Fraction");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("Id");
            builder.Property(m => m.Name).HasColumnName("FractionName");
        }
    }
}
