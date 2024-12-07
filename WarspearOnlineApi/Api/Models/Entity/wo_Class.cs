using System.ComponentModel.DataAnnotations;

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
        /// Интерсекция класса и фракции.
        /// </summary>
        public ICollection<wo_ClassFraction> ClassFractions { get; set; } = new List<wo_ClassFraction>();

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } = new List<wo_Player>();
    }
}
