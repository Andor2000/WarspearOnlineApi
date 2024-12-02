namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Dto-модель класса игрока.
    /// </summary>
    public class wo_Class
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// Интерсекция класса и фракции.
        /// </summary>
        public ICollection<wo_ClassFraction> ClassFractions { get; set; } = new List<wo_ClassFraction>();

        /// <summary>
        /// Игроки.
        /// </summary>
        public ICollection<wo_Player> Players { get; set; } = new List<wo_Player>();
    }
}
