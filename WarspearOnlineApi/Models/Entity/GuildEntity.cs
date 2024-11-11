namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель гильдии.
    /// </summary>
    public class GuildEntity : BaseModel
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Идентификтаор фракции.
        /// </summary>
        public int FractionId { get; set; }
    }
}
