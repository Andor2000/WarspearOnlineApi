using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Group"/>.
    /// </summary>
    public class GroupConfiguration : IEntityTypeConfiguration<wo_Group>
    {
        public void Configure(EntityTypeBuilder<wo_Group> builder)
        {
            builder.ToTable("wo_Group");
            builder.HasKey(m => m.GroupID);

            builder.Property(m => m.GroupID).HasColumnName("GroupID").UseIdentityColumn();
            builder.Property(m => m.GroupName).HasColumnName("GroupName").HasDefaultValue("");
        }
    }
}
