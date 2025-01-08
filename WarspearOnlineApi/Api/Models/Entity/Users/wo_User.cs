using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Entity.Users
{
    /// <summary>
    /// Entity-модель пользователя.
    /// </summary>
    public class wo_User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserLogin)]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [MaxLength(SizeFieldEnum.UserPassword)]
        public string Password { get; set; }

        /// <summary>
        /// Ранг уровня доступа.
        /// </summary>
        public int RangeAccessLevel { get; set; }

        /// <summary>
        /// Идентификатор уровня доступа.
        /// </summary>
        public int rf_AccessLevelID { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public wo_AccessLevel rf_AccessLevel { get; set; }

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

        /// <summary>
        /// Интерсекция пользователя и группы.
        /// </summary>
        public ICollection<wo_UserGroup> UserGroups = new List<wo_UserGroup>();
    }
}
