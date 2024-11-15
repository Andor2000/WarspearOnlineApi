﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(m => m.GroupGuildID).HasColumnName("GroupGuildID").UseIdentityColumn();

            builder.Property(m => m.rf_GroupID).HasColumnName("rf_GroupID").HasDefaultValue(0);
            builder.HasOne(x => x.Group).WithMany(x => x.GroupGuilds).HasForeignKey(x => x.rf_GroupID);

            builder.Property(m => m.rf_GuildID).HasColumnName("rf_GuildID").HasDefaultValue(0);
            builder.HasOne(x => x.Guild).WithMany(x => x.GroupGuilds).HasForeignKey(x => x.rf_GuildID);
        }
    }
}
