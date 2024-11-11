using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="GuildEntity"/>.
    /// </summary>
    public class GuildConfiguration : IEntityTypeConfiguration<GuildEntity>
    {
        public void Configure(EntityTypeBuilder<GuildEntity> builder)
        {
            builder.ToTable("Guild");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("GuildID");
            builder.Property(m => m.Name).HasColumnName("Name");
            builder.Property(m => m.ServerId).HasColumnName("rf_ServerID");
            builder.Property(m => m.FractionId).HasColumnName("rf_FractionID");
        }
    }
}
