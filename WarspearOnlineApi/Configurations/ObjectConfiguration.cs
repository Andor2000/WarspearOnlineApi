using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
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

            builder.Property(m => m.ObjectTypeId).HasColumnName("rf_ObjectTypeID");
        }
    }
}
