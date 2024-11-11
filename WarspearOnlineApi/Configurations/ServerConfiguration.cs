using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="ServerEntity"/>.
    /// </summary>
    public class ServerConfiguration : IEntityTypeConfiguration<ServerEntity>
    {
        public void Configure(EntityTypeBuilder<ServerEntity> builder)
        {
            builder.ToTable("Server");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("ServerID");
            builder.Property(m => m.Name).HasColumnName("ServerName");
        }
    }
}
