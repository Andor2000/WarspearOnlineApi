using Dapper;
using Microsoft.EntityFrameworkCore;

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
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var colomnKeyName = entityType.FindPrimaryKey()?.Properties?.FirstOrDefault()?.GetColumnName() ?? string.Empty;

                this._сontext.Database.GetDbConnection().Execute($@"
SET IDENTITY_INSERT {tableName} ON
if not exists(select 1 from {tableName} where {colomnKeyName} = 0)
begin
    INSERT INTO {tableName}
    ({colomnKeyName})
    VALUES (0)
end
SET IDENTITY_INSERT {tableName} OFF");
            }
        }
    }
}