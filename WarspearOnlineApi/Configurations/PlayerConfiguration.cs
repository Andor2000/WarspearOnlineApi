using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Player"/>.
    /// </summary>
    public class PlayerConfiguration : IEntityTypeConfiguration<wo_Player>
    {
        public void Configure(EntityTypeBuilder<wo_Player> builder)
        {
            builder.ToTable("wo_Player");
            builder.HasKey(m => m.PlayerID);

            builder.Property(m => m.ServerId).HasColumnName("rf_ServerID");
            builder.Property(m => m.FractionId).HasColumnName("rf_FractionID");
        }
    }
}
