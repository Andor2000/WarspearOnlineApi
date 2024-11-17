using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_ObjectType"/>.
    /// </summary>
    public class ObjectTypeConfiguration : IEntityTypeConfiguration<wo_ObjectType>
    {
        public void Configure(EntityTypeBuilder<wo_ObjectType> builder)
        {
            builder.ToTable("wo_ObjectType");
            builder.HasKey(m => m.ObjectTypeID);

            builder.Property(m => m.ObjectTypeID).HasColumnName("ObjectTypeID").UseIdentityColumn();
            builder.Property(m => m.ObjectTypeName).HasColumnName("ObjectTypeName").HasDefaultValue(string.Empty);
        }
    }
}
