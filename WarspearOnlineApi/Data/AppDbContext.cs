using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Models.Entity;

namespace WarspearOnlineApi.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных с параметрами.
        /// </summary>
        /// <param name="options">Параметры контекста базы данных.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Набор данных фракций.
        /// </summary>
        public DbSet<FractionEntity> Fractions { get; set; }

        /// <summary>
        /// Набор данных серверов.
        /// </summary>
        public DbSet<ServerEntity> Servers { get; set; }

        /// <summary>
        /// Набор данных гильдий.
        /// </summary>
        public DbSet<GuildEntity> Guilds { get; set; }

        /// <summary>
        /// Набор данных групп.
        /// </summary>
        public DbSet<GroupEntity> Groups { get; set; }

        /// <summary>
        /// Набор данных для связи группы и гильдии.
        /// </summary>
        public DbSet<GroupGuildEntity> GroupGuilds { get; set; }

        /// <summary>
        /// Набор данных игроков.
        /// </summary>
        public DbSet<PlayerEntity> Players { get; set; }

        /// <summary>
        /// Набор данных объектов.
        /// </summary>
        public DbSet<ObjectEntity> Objects { get; set; }

        /// <summary>
        /// Набор данных типов объектов.
        /// </summary>
        public DbSet<ObjectTypeEntity> ObjectTypes { get; set; }

        /// <summary>
        /// Набор данных журнала выпадения предметов.
        /// </summary>
        public DbSet<DropJournalEntity> DropJournals { get; set; }

        /// <summary>
        /// Набор данных для связи журнала выпадений и игроков.
        /// </summary>
        public DbSet<DropJournalPlayerEntity> DropJournalPlayers { get; set; }

        /// <summary>
        /// Настройка конфигураций моделей при создании модели.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Автоматически применяем все конфигурации в текущей сборке
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
