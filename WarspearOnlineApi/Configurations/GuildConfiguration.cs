using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Guild"/>.
    /// </summary>
    public class GuildConfiguration : IEntityTypeConfiguration<wo_Guild>
    {
        public void Configure(EntityTypeBuilder<wo_Guild> builder)
        {
            builder.ToTable("wo_Guild");
            builder.HasKey(m => m.GuildID);

            builder.Property(m => m.ServerId).HasColumnName("rf_ServerID");
            builder.Property(m => m.FractionId).HasColumnName("rf_FractionID");
        }
    }
}
