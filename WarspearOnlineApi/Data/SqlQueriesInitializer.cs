namespace WarspearOnlineApi.Data
{
    /// <summary>
    /// Sql-запросы для инициализации базы данных.
    /// </summary>
    public static class SqlQueriesInitializer
    {
        public static readonly string CreateBaseRecords = $@"
wo_Role
wo_AccessLevelRole
wo_AccessLevel

wo_Class
wo_Fraction

";
    }
}
