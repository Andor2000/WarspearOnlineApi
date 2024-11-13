using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WarspearOnlineApi.Configurations;
using WarspearOnlineApi.Enums;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных с параметрами.
        /// </summary>
        /// <param name="options">Параметры контекста базы данных.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Набор данных фракций.
        /// </summary>
        public DbSet<wo_Fraction> wo_Fraction { get; set; }

        /// <summary>
        /// Набор данных серверов.
        /// </summary>
        public DbSet<wo_Server> wo_Server { get; set; }

        /// <summary>
        /// Набор данных гильдий.
        /// </summary>
        public DbSet<wo_Guild> wo_Guild { get; set; }

        /// <summary>
        /// Набор данных групп.
        /// </summary>
        public DbSet<wo_Group> wo_Group { get; set; }

        /// <summary>
        /// Набор данных для связи группы и гильдии.
        /// </summary>
        public DbSet<wo_GroupGuild> wo_GroupGuild { get; set; }

        /// <summary>
        /// Набор данных игроков.
        /// </summary>
        public DbSet<wo_Player> wo_Player { get; set; }

        /// <summary>
        /// Набор данных объектов.
        /// </summary>
        public DbSet<wo_Object> wo_Object { get; set; }

        /// <summary>
        /// Набор данных типов объектов.
        /// </summary>
        public DbSet<wo_ObjectType> wo_ObjectType { get; set; }

        /// <summary>
        /// Набор данных журнала выпадения предметов.
        /// </summary>
        public DbSet<wo_Drop> wo_Drop { get; set; }

        /// <summary>
        /// Набор данных для связи журнала выпадений и игроков.
        /// </summary>
        public DbSet<wo_DropPlayer> wo_DropPlayer { get; set; }

        /// <summary>
        /// Настройка конфигураций моделей при создании модели.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Применяем конфигурации для всех сущностей из текущей сборки
            //modelBuilder.ApplyConfiguration(new DropConfiguration());
            //modelBuilder.ApplyConfiguration(new DropPlayerConfiguration());
            //modelBuilder.ApplyConfiguration(new FractionConfiguration());
            //modelBuilder.ApplyConfiguration(new GroupConfiguration());
            //modelBuilder.ApplyConfiguration(new GroupGuildConfiguration());
            //modelBuilder.ApplyConfiguration(new GuildConfiguration());
            //modelBuilder.ApplyConfiguration(new ObjectConfiguration());
            //modelBuilder.ApplyConfiguration(new ObjectTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            //modelBuilder.ApplyConfiguration(new ServerConfiguration());


            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
