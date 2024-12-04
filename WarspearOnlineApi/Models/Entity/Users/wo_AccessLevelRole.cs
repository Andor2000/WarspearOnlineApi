namespace WarspearOnlineApi.Models.Entity.Users
{
    /// <summary>
    /// Entity-модель интерсекции моделей уровня доступа и роли.
    /// </summary>
    public class wo_AccessLevelRole
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int AccessLevelRoleID { get; set; }

        /// <summary>
        /// Идентификатор уровня доступа.
        /// </summary>
        public int rf_AccessLevelID { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public wo_AccessLevel rf_AccessLevel { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public int rf_RoleID { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        public wo_Role rf_Role { get; set; }
    }
}
