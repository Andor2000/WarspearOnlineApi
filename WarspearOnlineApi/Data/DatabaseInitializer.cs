using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WarspearOnlineApi.Enums;

namespace WarspearOnlineApi.Data
{
    public class DatabaseInitializer
    {
        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly AppDbContext _сontext;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dbContext">Контекст данных.</param>
        /// <param name="configuration">Конфигуратор.</param>
        public DatabaseInitializer(AppDbContext dbContext, IConfiguration configuration)
        {
            _сontext = dbContext;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddEmptyRecords()
        {
            var entityTypes = _сontext.Model.GetEntityTypes().Skip(1);
            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                var ColomnKeyName = entityType.FindPrimaryKey()?.Properties?.FirstOrDefault()?.GetColumnName() ?? string.Empty;

                this._сontext.Database.GetDbConnection().Execute($@"
SET IDENTITY_INSERT {tableName} ON
if not exists(select 1 from {tableName} where {ColomnKeyName} = 0)
begin
    INSERT INTO {tableName} ({ColomnKeyName}) VALUES (0)
end
SET IDENTITY_INSERT {tableName} OFF");
            }
        }
    }
}