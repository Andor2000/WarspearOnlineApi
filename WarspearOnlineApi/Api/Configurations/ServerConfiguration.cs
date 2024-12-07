using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Configurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="wo_Server"/>.
    /// </summary>
    public class ServerConfiguration : IEntityTypeConfiguration<wo_Server>
    {
        public void Configure(EntityTypeBuilder<wo_Server> builder)
        {
            builder.ToTable("wo_Server");
            builder.HasKey(m => m.ServerID);

            builder.Property(m => m.ServerID).UseIdentityColumn();
            builder.Property(m => m.ServerCode).HasDefaultValue(string.Empty);
            builder.Property(m => m.ServerName).HasDefaultValue(string.Empty);
        }
    }
}
