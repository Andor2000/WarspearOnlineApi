using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="GroupGuildEntity"/>.
    /// </summary>
    public class GroupGuildConfiguration : IEntityTypeConfiguration<GroupGuildEntity>
    {
        public void Configure(EntityTypeBuilder<GroupGuildEntity> builder)
        {
            builder.ToTable("GroupGuild");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("GroupGuildID");
            builder.Property(m => m.GroupId).HasColumnName("rf_GroupID");
            builder.Property(m => m.GuildId).HasColumnName("rf_GuildID");
        }
    }
}
