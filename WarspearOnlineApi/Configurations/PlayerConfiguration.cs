﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Player"/>.
    /// </summary>
    public class PlayerConfiguration : IEntityTypeConfiguration<wo_Player>
    {
        public void Configure(EntityTypeBuilder<wo_Player> builder)
        {
            builder.ToTable("wo_Player");
            builder.HasKey(m => m.PlayerID);

            builder.Property(m => m.PlayerID).HasColumnName("PlayerID").UseIdentityColumn();
            builder.Property(m => m.Nick).HasColumnName("Nick").HasDefaultValue("");

            builder.Property(m => m.rf_ServerID).HasColumnName("rf_ServerID").HasDefaultValue(0);
            builder.HasOne(x => x.Server).WithMany(x => x.Players).HasForeignKey(x => x.rf_ServerID);

            builder.Property(m => m.rf_FractionID).HasColumnName("rf_FractionID").HasDefaultValue(0);
            builder.HasOne(x => x.Fraction).WithMany(x => x.Players).HasForeignKey(x => x.rf_FractionID);
        }
    }
}
