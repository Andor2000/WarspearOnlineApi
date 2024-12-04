namespace WarspearOnlineApi.Models.Entity.Users
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
        /// Название уровня доступа.
        /// </summary>
        public string AccessLevelName { get; set; }

        /// <summary>
        /// Интерсекции моделей уровня доступа и роли.
        /// </summary>
        public ICollection<wo_AccessLevelRole> AccessLevelRoles = new List<wo_AccessLevelRole>();
    }
}
