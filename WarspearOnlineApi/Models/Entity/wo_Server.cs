namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель сервера.
    /// </summary>
    public class wo_Server
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ServerID { get; set; }

        /// <summary>
        /// Название сервера.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Гильдии.
        /// </summary>
        public ICollection<wo_Guild> Guilds { get; set; } = new List<wo_Guild>();

        /// <summary>
        /// Группы.
        /// </summary>
        public ICollection<wo_Group> Groups { get; set; } = new List<wo_Group>();

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } =  new List<wo_Player>();

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();
    }
}
