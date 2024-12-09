using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Entity.Intersections
{
    /// <summary>
    /// Entity-модель связи пользователя и сервера.
    /// </summary>
    public class wo_UserServer
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int UserServerID { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int rf_UserID { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public wo_User rf_User { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int rf_ServerID { get; set; }

        /// <summary>
        /// Сервер.
        /// </summary>
        public wo_Server rf_Server { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }
    }
}
