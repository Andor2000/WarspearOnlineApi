namespace WarspearOnlineApi.Api.Models.Entity
{
    /// <summary>
    /// Entity-модель связи выпавшего дропа и игрока.
    /// </summary>
    public class wo_DropPlayer
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int DropPlayerID { get; set; }

        /// <summary>
        /// Доля.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Признак выплаты.
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Идентификатор дропа.
        /// </summary>
        public int rf_DropID { get; set; }

        /// <summary>
        /// Дроп.
        /// </summary>
        public wo_Drop rf_Drop { get; set; }

        /// <summary>
        /// Идентификатор игрока.
        /// </summary>
        public int rf_PlayerID { get; set; }

        /// <summary>
        /// Игрок.
        /// </summary>
        public wo_Player rf_Player { get; set; }
    }
}
