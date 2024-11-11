using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="DropJournalPlayerEntity"/>.
    /// </summary>
    public class DropJournalPlayerConfiguration : IEntityTypeConfiguration<DropJournalPlayerEntity>
    {
        public void Configure(EntityTypeBuilder<DropJournalPlayerEntity> builder)
        {
            builder.ToTable("DropJournalPlayer");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("DropJournalPlayerID");
            builder.Property(m => m.DropJournalId).HasColumnName("rf_DropJournalID");
            builder.Property(m => m.PlayerId).HasColumnName("rf_PlayerID");
        }
    }
}
