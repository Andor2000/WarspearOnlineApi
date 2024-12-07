using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Configurations
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

            builder.Property(m => m.DropID).UseIdentityColumn();
            builder.Property(m => m.Drop_Date).HasDefaultValue(DefaultsDates.MinDate);
            builder.Property(m => m.Price).HasDefaultValue(0);

            builder.Property(m => m.rf_ObjectID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Object).WithMany(x => x.Drops).HasForeignKey(x => x.rf_ObjectID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_GroupID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Group).WithMany(x => x.Drops).HasForeignKey(x => x.rf_GroupID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_ServerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Drops).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Drops).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
