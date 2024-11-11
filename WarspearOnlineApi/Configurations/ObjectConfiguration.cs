using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="ObjectEntity"/>.
    /// </summary>
    public class ObjectConfiguration : IEntityTypeConfiguration<ObjectEntity>
    {
        public void Configure(EntityTypeBuilder<ObjectEntity> builder)
        {
            builder.ToTable("Object");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("ObjectID");
            builder.Property(m => m.Name).HasColumnName("ObjectName");
            builder.Property(m => m.Image).HasColumnName("Image");
            builder.Property(m => m.ObjectTypeId).HasColumnName("rf_ObjectTypeID");
        }
    }
}
