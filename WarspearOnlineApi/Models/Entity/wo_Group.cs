namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель группы.
    /// </summary>
    public class wo_Group
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int rf_ServerID { get; set; }

        /// <summary>
        /// Сервер.
        /// </summary>
        public wo_Server rf_Server { get; set; }

        /// <summary>
        /// Идентификтаор фракции.
        /// </summary>
        public int rf_FractionID { get; set; }

        /// <summary>
        /// Фракция.
        /// </summary>
        public wo_Fraction rf_Fraction { get; set; }

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();

        /// <summary>
        /// Интерсекиция группы и гильдии.
        /// </summary>
        public ICollection<wo_GroupGuild> GroupGuilds { get; set; } = new List<wo_GroupGuild>();
    }
}
