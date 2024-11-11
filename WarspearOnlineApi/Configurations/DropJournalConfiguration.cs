using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="DropJournalEntity"/>.
    /// </summary>
    public class DropJournalConfiguration : IEntityTypeConfiguration<DropJournalEntity>
    {
        public void Configure(EntityTypeBuilder<DropJournalEntity> builder)
        {
            builder.ToTable("DropJournal");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("DropJournalID");
            builder.Property(m => m.DropDate).HasColumnName("Drop_Date");
            builder.Property(m => m.Price).HasColumnName("Price");
            builder.Property(m => m.ObjectId).HasColumnName("rf_ObjectID");
            builder.Property(m => m.GroupId).HasColumnName("rf_GroupID");
        }
    }
}
