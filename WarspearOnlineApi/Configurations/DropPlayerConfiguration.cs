using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_DropPlayer"/>.
    /// </summary>
    public class DropPlayerConfiguration : IEntityTypeConfiguration<wo_DropPlayer>
    {
        public void Configure(EntityTypeBuilder<wo_DropPlayer> builder)
        {
            builder.ToTable("wo_DropPlayer");
            builder.HasKey(m => m.DropPlayerID);

            builder.Property(m => m.rf_DropID).HasColumnName("rf_DropID");
            builder.Property(m => m.rf_PlayerID).HasColumnName("rf_PlayerID");
        }
    }
}
