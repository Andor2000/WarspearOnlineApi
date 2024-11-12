namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель игрока.
    /// </summary>
    public class wo_Player
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Ник игрока.
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int FractionId { get; set; }
    }
}
