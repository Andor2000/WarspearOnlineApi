using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Models.Entity.Intersections;
using WarspearOnlineApi.Api.Models.Entity.Users;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель фракции.
    /// </summary>
    public class wo_Fraction
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int FractionID { get; set; }

        /// <summary>
        /// Код фракции.
        /// </summary>
        [MaxLength(20)]
        public string FractionCode { get; set; }

        /// <summary>
        /// Название фракции.
        /// </summary>
        [MaxLength(20)]
        public string FractionName { get; set; }

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
        /// Интерсекция класса и фракции.
        /// </summary>
        public ICollection<wo_ClassFraction> ClassFractions { get; set; } = new List<wo_ClassFraction>();

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();

        /// <summary>
        /// Пользователи.
        /// </summary>
        public ICollection<wo_User> Users { get; set; } = new List<wo_User>();
    }
}
