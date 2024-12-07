using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarspearOnlineApi.Api.Models.Entity;

namespace WarspearOnlineApi.Api.Configurations
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

            builder.Property(m => m.GroupID).UseIdentityColumn();
            builder.Property(m => m.GroupName).HasDefaultValue(string.Empty);

            builder.Property(m => m.rf_ServerID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Server).WithMany(x => x.Groups).HasForeignKey(x => x.rf_ServerID).OnDelete(DeleteBehavior.NoAction);

            builder.Property(m => m.rf_FractionID).HasDefaultValue(0);
            builder.HasOne(x => x.rf_Fraction).WithMany(x => x.Groups).HasForeignKey(x => x.rf_FractionID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
