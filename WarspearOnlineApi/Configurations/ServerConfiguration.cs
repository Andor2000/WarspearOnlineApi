using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Configurations
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
        }
    }
}
