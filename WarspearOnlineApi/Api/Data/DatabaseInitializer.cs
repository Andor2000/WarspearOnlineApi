using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WarspearOnlineApi.Api.SqlQueries;

namespace WarspearOnlineApi.Api.Data
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
            var queries = new StringBuilder();

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

        /// <summary>
        /// Создание таблиц истории.
        /// </summary>
        public void CreateHistoryTables()
        {
            var entityTypes = this._сontext.Model.GetEntityTypes();
            var queries = new StringBuilder();

            foreach (var entityType in entityTypes)
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("life_")) continue;

                var historyTableName = $"life_{tableName}";
                var primaryKeyColumn = entityType.FindPrimaryKey()?.Properties.FirstOrDefault()?.GetColumnName();

                if (primaryKeyColumn == null) continue;

                // Создаем SQL для таблицы истории
                var columns = entityType.GetProperties()
                    .Select(p => $"[{p.GetColumnName()}] {p.GetColumnType()} NULL")
                    .ToList();
                columns.Add("[x_DateTime] DATETIME NOT NULL");
                columns.Add("[x_Operation] CHAR(1) NOT NULL");
                columns.Add("[x_UserID] INT NULL");

                var createTableQuery = $@"
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{historyTableName}')
BEGIN
    CREATE TABLE [{historyTableName}] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        {string.Join(",\n        ", columns)}
    )
END;";
                queries.Append(createTableQuery);

                // Создаем SQL для триггера
                var triggerQuery = $@"
IF OBJECT_ID('trg_{tableName}_ChangeLog', 'TR') IS NULL
BEGIN
    EXEC('CREATE TRIGGER trg_{tableName}_ChangeLog
    ON [{tableName}]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;

        DECLARE @currentUserId INT;
        SET @currentUserId = CONVERT(INT, SESSION_CONTEXT(N''UserID''));

        -- Вставленные записи
        INSERT INTO [{historyTableName}] ({string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, x_DateTime, x_Operation, x_UserID)
        SELECT {string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, GETDATE(), ''i'', COALESCE(@currentUserId, 0)
        FROM INSERTED;

        -- Обновленные записи
        INSERT INTO [{historyTableName}] ({string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, x_DateTime, x_Operation, x_UserID)
        SELECT {string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, GETDATE(), ''u'', COALESCE(@currentUserId, 0)
        FROM INSERTED;

        -- Удаленные записи
        INSERT INTO [{historyTableName}] ({string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, x_DateTime, x_Operation, x_UserID)
        SELECT {string.Join(", ", entityType.GetProperties().Select(p => $"[{p.GetColumnName()}]"))}, GETDATE(), ''d'', COALESCE(@currentUserId, 0)
        FROM DELETED;
    END;')
END;";
                queries.Append(triggerQuery);
            }

            // Выполнение всех запросов
            var finalQuery = queries.ToString();
            this._сontext.Database.ExecuteSqlRaw(finalQuery);
        }


        /// <summary>
        /// Добавление базовых записей в бд.
        /// </summary>
        public void AddBaseRecords()
        {
            this._сontext.Database.GetDbConnection().Execute(SqlQueriesInitializer.CreateBaseRecords);
        }

        /// <summary>
        /// Ожидает, пока база данных станет доступной.
        /// </summary>
        public void WaitForDatabaseConnection()
        {
            const int maxRetries = 30; // Максимальное количество попыток
            const int delayMilliseconds = 1000; // Задержка между попытками в миллисекундах

            for (int i = 0; i < maxRetries; i++)
            {
                if (this._сontext.Database.CanConnect())
                {
                    // Console.WriteLine("Соединение с базой данных установлено");
                    return;
                }

                // Console.WriteLine("Не удалось подключиться к базе данных. Повторная попытка через 1 секунду..");
                Thread.Sleep(delayMilliseconds);
            }

            throw new Exception("Не удалось подключиться к базе данных после нескольких попыток");
        }
    }
}