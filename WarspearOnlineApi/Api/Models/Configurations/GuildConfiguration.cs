using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Models.Configurations
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

            builder.Property(m => m.GuildID).UseIdentityColumn();
            builder.Property(m => m.GuildName).HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_ServerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Guilds).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Guilds).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_UserID).HasDefaultValue(0);
        }
    }
}
