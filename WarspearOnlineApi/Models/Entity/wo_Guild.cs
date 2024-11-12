namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель гильдии.
    /// </summary>
    public class wo_Guild
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int GuildID { get; set; }

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
