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

            builder.Property(m => m.PlayerID).UseIdentityColumn();
            builder.Property(m => m.Nick).HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_ServerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Players).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Players).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_ClassID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Class).WithMany(x => x.Players).HasForeignKey(x => x.rf_ClassID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
