using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="GroupEntity"/>.
    /// </summary>
    public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("GroupID");
            builder.Property(m => m.Name).HasColumnName("Name");
        }
    }
}
