namespace WarspearOnlineApi.Api.Models.Entity.Users
{
    /// <summary>
    /// Entity-модель уровня доступа.
    /// </summary>
    public class wo_AccessLevel
    {
        /// <summary>
        /// Идентификатор уровня доступа.
        /// </summary>
        public int AccessLevelID { get; set; }

        /// <summary>
        /// Код уровня доступа.
        /// </summary>
        public string AccessLevelCode { get; set; }

        /// <summary>
        /// Название уровня доступа.
        /// </summary>
        public string AccessLevelName { get; set; }

        /// <summary>
        /// Приоритет уровня доступа.
        /// </summary>
        public int AccessLevelInt { get; set; }

        /// <summary>
        /// Идентификатор родительского уровня доступа.
        /// </summary>
        public int rf_ParentAccessLevelID { get; set; }

        /// <summary>
        /// Родительский уровень доступа.
        /// </summary>
        public wo_AccessLevel rf_ParentAccessLevel { get; set; }

        /// <summary>
        /// Интерсекции моделей уровня доступа и роли.
        /// </summary>
        public ICollection<wo_AccessLevelRole> AccessLevelRoles { get; set; } = new List<wo_AccessLevelRole>();

        /// <summary>
        /// Дочерние уровни доступа.
        /// </summary>
        public ICollection<wo_AccessLevel> ChildAccessLevels { get; set; } = new List<wo_AccessLevel>();

        /// <summary>
        /// Пользователи.
        /// </summary>
        public ICollection<wo_User> Users { get; set; } = new List<wo_User>();
    }
}
