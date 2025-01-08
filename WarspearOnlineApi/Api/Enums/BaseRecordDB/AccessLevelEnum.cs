namespace WarspearOnlineApi.Api.Enums.BaseRecordDB
{
    /// <summary>
    /// Уровень доступа.
    /// </summary>
    public static class AccessLevelEnum
    {
        /// <summary>
        /// Главный администратор.
        /// </summary>
        public static readonly string MainAdmin = "Главный админ";

        /// <summary>
        /// Админ сервера.
        /// </summary>
        public static readonly string AdminServer = "Админ сервера";

        /// <summary>
        /// Администратор.
        /// </summary>
        public static readonly string Admin = "Админ";

        /// <summary>
        /// Модератор.
        /// </summary>
        public static readonly string Moderator = "Модератор";

        /// <summary>
        /// Получить уровень доступа.
        /// </summary>
        /// <param name="name">Название роли.</param>
        /// <returns>Уровень доступа.</returns>
        public static int LevelValue(this string name)
        {
            return name switch
            {
                var x when x == nameof(Moderator) => 1,
                var x when x == nameof(Admin) => 2,
                var x when x == nameof(AdminServer) => 3,
                var x when x == nameof(MainAdmin) => 4,
                _ => throw new Exception("Новой роли нужно выдать числовой уровень доступа"),
            };
        }
    }
}
