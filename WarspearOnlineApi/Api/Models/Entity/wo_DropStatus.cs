using System.ComponentModel.DataAnnotations;

namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель статуса выпавшего дропа.
    /// </summary>
    public class wo_DropStatus
    {
        /// <summary>
        /// Идентификатор статуса выпавшего дропа.
        /// </summary>
        public int DropStatusID { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        [MaxLength(10)]
        public string DropStatusCode { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [MaxLength(50)]
        public string DropStatusName { get; set; }

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();
    }
}
