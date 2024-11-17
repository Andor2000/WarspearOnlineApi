using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace WarspearOnlineApi.Data
{
    public class DatabaseInitializer
    {
        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly AppDbContext _сontext;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbContext">Контекст данных.</param>
        public DatabaseInitializer(AppDbContext dbContext)
        {
            this._сontext = dbContext;
        }

        /// <summary>
        /// Добавление нулевой записи всем таблицам в бд, если ее нет.
        /// </summary>
        public void AddEmptyRecords()
        {
            var entityTypes = this._сontext.Model.GetEntityTypes();
            var queries= new StringBuilder();
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var colomnKeyName = entityType.FindPrimaryKey().Properties.FirstOrDefault().GetColumnName();

                var query = $@"
if not exists(select 1 from {tableName} where {colomnKeyName} = 0)
begin
    SET IDENTITY_INSERT {tableName} ON
    -- Отключаем все ограничения для таблицы
    ALTER TABLE {tableName} NOCHECK CONSTRAINT ALL
        INSERT INTO {tableName}
        ({colomnKeyName})
        VALUES (0)
    -- Включаем все ограничения обратно
    ALTER TABLE {tableName} CHECK CONSTRAINT ALL
    SET IDENTITY_INSERT {tableName} OFF
end
";
                queries.Append(query);
            }

            var resQuery = queries.ToString();
            this._сontext.Database.GetDbConnection().Execute(resQuery);
        }
    }
}