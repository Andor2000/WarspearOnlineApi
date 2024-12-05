using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Models.Entity;
using WarspearOnlineApi.Models.Entity.Users;

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

        /* Общие */

        /// <summary>
        /// Набор данных серверов.
        /// </summary>
        public DbSet<wo_Server> wo_Server { get; set; }

        /// <summary>
        /// Набор данных фракций.
        /// </summary>
        public DbSet<wo_Fraction> wo_Fraction { get; set; }

        /// <summary>
        /// Набор данных класса.
        /// </summary>
        public DbSet<wo_Class> wo_Class { get; set; }

        /* Интерфейс */

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

        /* Пользователь */

        /// <summary>
        /// Набор данных уровней доступа.
        /// </summary>
        public DbSet<wo_AccessLevel> wo_AccessLevel { get; set; }

        /// <summary>
        /// Набор данных интерсекции моделей уровня доступа и роли.
        /// </summary>
        public DbSet<wo_AccessLevelRole> wo_AccessLevelRole { get; set; }

        /// <summary>
        /// Набор данных ролей.
        /// </summary>
        public DbSet<wo_Role> wo_Role { get; set; }

        /// <summary>
        /// Настройка конфигураций моделей при создании модели.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("datetime2(3)");
                    }
                    if (property.ClrType == typeof(string))
                    {
                        property.SetIsUnicode(true); // Включаем поддержку Unicode
                        if (property.GetMaxLength().IsNullOrDefault())
                        {
                            property.SetColumnType("nvarchar(100)");
                        }
                    }
                }
            }
        }
    }
}
