namespace WarspearOnlineApi.Api.Models.Entity.Users
{
    /// <summary>
    /// Entity-модель роли.
    /// </summary>
    public class wo_Role
    {
        /// <summary>
        /// Иденфтификатор роли.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Код роли.
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Интерсекции моделей уровня доступа и роли.
        /// </summary>
        public ICollection<wo_AccessLevelRole> AccessLevelRoles = new List<wo_AccessLevelRole>();
    }
}
