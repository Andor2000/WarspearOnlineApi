using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель группы.
    /// </summary>
    public class wo_Group
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        [MaxLength(SizeFieldEnum.GroupName)]
        public string GroupName { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int rf_ServerID { get; set; }

        /// <summary>
        /// Сервер.
        /// </summary>
        public wo_Server rf_Server { get; set; }

        /// <summary>
        /// Идентификтаор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int rf_UserID { get; set; }

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();

        /// <summary>
        /// Интерсекиция группы и гильдии.
        /// </summary>
        public ICollection<wo_GroupGuild> GroupGuilds { get; set; } = new List<wo_GroupGuild>();

        /// <summary>
        /// Интерсекция пользователя и группы.
        /// </summary>
        public ICollection<wo_UserGroup> UserGroups = new List<wo_UserGroup>();
    }
}
