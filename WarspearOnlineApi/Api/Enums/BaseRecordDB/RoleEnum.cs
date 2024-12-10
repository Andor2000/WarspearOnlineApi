namespace WarspearOnlineApi.Api.Enums.BaseRecordDB
{
    /// <summary>
    /// Роли.
    /// </summary>
    public class RoleEnum
    {
        /* Модератор */

        /// <summary>
        /// Добавление нового игрока.
        /// </summary>
        public static readonly string AddDeletePlayerInDrop = "Добавление или удаление нового игрока в дропе";

        /// <summary>
        /// Добавление дропа.
        /// </summary>
        public static readonly string AddDrop = "Добавление дропа";

        /* Админ */

        /// <summary>
        /// Удаление дропа.
        /// </summary>
        public static readonly string DeleteDrop = "Удаление дропа";

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        public static readonly string AddUser = "Добавление пользователя";

        /* Админ сервера */

        /// <summary>
        /// Добавление гильдий.
        /// </summary>
        public static readonly string AddGuild = "Добавление гильдий";

        /// <summary>
        /// Добавление групп.
        /// </summary>
        public static readonly string AddDeleteGroup = "Добавление групп";

        /* Главный админ */

        /// <summary>
        /// Добавление объектов.
        /// </summary>
        public static readonly string AddObject = "Добавление объектов";

        /// <summary>
        /// Добавление классов.
        /// </summary>
        public static readonly string AddClass = "Добавление классов";

    }
}
