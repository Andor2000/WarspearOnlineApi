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

            builder.Property(m => m.GuildID).HasColumnName("GuildID").UseIdentityColumn();
            builder.Property(m => m.GuildName).HasColumnName("GuildName").HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_ServerID).HasColumnName("rf_ServerID").HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Guilds).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasColumnName("rf_FractionID").HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Guilds).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
