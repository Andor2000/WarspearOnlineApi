using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_GroupGuild"/>.
    /// </summary>
    public class GroupGuildConfiguration : IEntityTypeConfiguration<wo_GroupGuild>
    {
        public void Configure(EntityTypeBuilder<wo_GroupGuild> builder)
        {
            builder.ToTable("wo_GroupGuild");
            builder.HasKey(m => m.GroupGuildID);

            builder.Property(m => m.GroupId).HasColumnName("rf_GroupID");
            builder.Property(m => m.GuildId).HasColumnName("rf_GuildID");
        }
    }
}
