using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="ObjectTypeEntity"/>.
    /// </summary>
    public class ObjectTypeConfiguration : IEntityTypeConfiguration<ObjectTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ObjectTypeEntity> builder)
        {
            builder.ToTable("ObjectType");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("ObjectTypeID");
            builder.Property(m => m.Name).HasColumnName("ObjectTypeName");
        }
    }
}
