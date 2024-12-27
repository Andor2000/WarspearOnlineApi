using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Entity.Intersections
{
    /// <summary>
    /// Entity-модель связи пользователя и группы.
    /// </summary>
    public class wo_UserGroup
    {
        /// <summary>
        /// Идентификатор связи.
        /// </summary>
        public int UserGroupID { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int rf_UserID { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public wo_User rf_User { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int rf_GroupID { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public wo_Group rf_Group { get; set; }
    }
}
