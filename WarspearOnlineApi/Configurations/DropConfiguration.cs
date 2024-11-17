using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Enums;
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

            builder.Property(m => m.DropID).HasColumnName("DropID").UseIdentityColumn();
            builder.Property(m => m.Drop_Date).HasColumnName("Drop_Date").HasDefaultValue(DefaultsDates.MinDate);
            builder.Property(m => m.Price).HasColumnName("Price").HasDefaultValue(0);

            builder.Property(m => m.rf_ObjectID).HasColumnName("rf_ObjectID").HasDefaultValue(0);
            builder.HasOne(x => x.rf_Object).WithMany(x => x.Drops).HasForeignKey(x => x.rf_ObjectID);

            builder.Property(m => m.rf_GroupID).HasColumnName("rf_GroupID").HasDefaultValue(0);
            builder.HasOne(x => x.rf_Group).WithMany(x => x.Drops).HasForeignKey(x => x.rf_GroupID);

            builder.Property(m => m.rf_ServerID).HasColumnName("rf_ServerID").HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Drops).HasForeignKey(x => x.rf_ServerID);
        }
    }
}
