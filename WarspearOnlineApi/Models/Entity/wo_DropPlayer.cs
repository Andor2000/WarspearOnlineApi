namespace WarspearOnlineApi.Models.Entity
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
        /// Идентификатор записи в журнале дропа.
        /// </summary>
        public int rf_DropID { get; set; }

        /// <summary>
        /// Идентификатор игрока.
        /// </summary>
        public int rf_PlayerID { get; set; }
    }
}
