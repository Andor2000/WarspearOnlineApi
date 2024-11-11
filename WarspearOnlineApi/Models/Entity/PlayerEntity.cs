namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель игрока.
    /// </summary>
    public class PlayerEntity : BaseModel
    {
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
