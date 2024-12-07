using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Object"/>.
    /// </summary>
    public class ObjectConfiguration : IEntityTypeConfiguration<wo_Object>
    {
        public void Configure(EntityTypeBuilder<wo_Object> builder)
        {
            builder.ToTable("wo_Object");
            builder.HasKey(m => m.ObjectID);

            builder.Property(m => m.ObjectID).UseIdentityColumn();
            builder.Property(m => m.ObjectCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.ObjectName).HasDefaultValue(string.Empty);
            builder.Property(m => m.Image).HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_ObjectTypeID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_ObjectType).WithMany(x => x.Objects).HasForeignKey(x => x.rf_ObjectTypeID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
