namespace WarspearOnlineApi.Models.Entity
{
    /// <summary>
    /// Entity-модель журнала дропа.
    /// </summary>
    public class DropJournalEntity : BaseModel
    {
        /// <summary>
        /// Дата дропа.
        /// </summary>
        public DateTime DropDate { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int GroupId { get; set; }
    }
}
