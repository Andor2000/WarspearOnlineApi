using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель сервера.
    /// </summary>
    public class wo_Server
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ServerID { get; set; }

        /// <summary>
        /// Название сервера.
        /// </summary>
        [MaxLength(20)]
        public string ServerCode { get; set; }

        /// <summary>
        /// Название сервера.
        /// </summary>
        [MaxLength(20)]
        public string ServerName { get; set; }

        /// <summary>
        /// Гильдии.
        /// </summary>
        public ICollection<wo_Guild> Guilds { get; set; } = new List<wo_Guild>();

        /// <summary>
        /// Группы.
        /// </summary>
        public ICollection<wo_Group> Groups { get; set; } = new List<wo_Group>();

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } = new List<wo_Player>();

        /// <summary>
        /// Пользователи.
        /// </summary>
        public ICollection<wo_User> Users { get; set; } = new List<wo_User>();
    }
}
