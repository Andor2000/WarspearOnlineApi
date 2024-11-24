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

            builder.Property(m => m.DropPlayerID).UseIdentityColumn();
            builder.Property(m => m.Part).HasDefaultValue(0);

            builder.Property(m => m.rf_DropID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Drop).WithMany(x => x.DropPlayers).HasForeignKey(x => x.rf_DropID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_PlayerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Player).WithMany(x => x.DropPlayers).HasForeignKey(x => x.rf_PlayerID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
