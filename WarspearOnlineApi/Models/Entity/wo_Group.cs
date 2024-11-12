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
        public string Name { get; set; }

        /// <summary>
        /// Список дропа.
        /// </summary>
        public ICollection<wo_Drop> Drops { get; set; } = new List<wo_Drop>();
    }
}
