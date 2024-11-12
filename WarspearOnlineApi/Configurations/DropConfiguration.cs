using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Drop"/>.
    /// </summary>
    public class DropConfiguration : IEntityTypeConfiguration<wo_Drop>
    {
        public void Configure(EntityTypeBuilder<wo_Drop> builder)
        {
            builder.ToTable("wo_Drop");
            builder.HasKey(m => m.DropID);

            builder.HasOne(x => x.Object).WithMany(x => x.Drops).HasForeignKey(x => x.rf_ObjectID);
            builder.HasOne(x => x.Group).WithMany(x => x.Drops).HasForeignKey(x => x.rf_GroupID);
        }
    }
}
