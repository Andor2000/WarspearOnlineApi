using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="PlayerEntity"/>.
    /// </summary>
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder.ToTable("Player");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("PlayerID");
            builder.Property(m => m.Nick).HasColumnName("Nick");
            builder.Property(m => m.ServerId).HasColumnName("rf_ServerID");
            builder.Property(m => m.FractionId).HasColumnName("rf_FractionID");
        }
    }
}
