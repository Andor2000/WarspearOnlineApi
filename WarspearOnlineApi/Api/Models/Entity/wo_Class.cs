using System.ComponentModel.DataAnnotations;
using WarspearOnlineApi.Api.Models.Entity.Intersections;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Dto-модель класса игрока.
    /// </summary>
    public class wo_Class
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        [MaxLength(20)]
        public string ClassCode { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [MaxLength(20)]
        public string ClassName { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } = new List<wo_Player>();
    }
}
