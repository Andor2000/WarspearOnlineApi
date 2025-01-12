using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Configurations
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

            builder.Property(m => m.GroupGuildID).UseIdentityColumn();

            builder.Property(m => m.rf_GroupID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Group).WithMany(x => x.GroupGuilds).HasForeignKey(x => x.rf_GroupID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_GuildID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Guild).WithMany(x => x.GroupGuilds).HasForeignKey(x => x.rf_GuildID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
