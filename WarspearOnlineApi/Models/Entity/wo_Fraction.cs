namespace WarspearOnlineApi.Models.Entity
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
        /// Название фракции.
        /// </summary>
        public string FractionName { get; set; }

        /// <summary>
        /// Гильдии.
        /// </summary>
        public ICollection<wo_Guild> Guilds { get; set; } = new List<wo_Guild>();

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } = new List<wo_Player>();
    }
}
