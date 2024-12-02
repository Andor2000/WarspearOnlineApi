namespace WarspearOnlineApi.Models.Filters
{
    /// <summary>
    /// Фильтр для получения журнала дропа.
    /// </summary>
    public class DropFilter
    {
        /// <summary>
        /// Идентификатор сервера.
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Идентификатор фракции.
        /// </summary>
        public int FractionId { get; set; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Идентификатор типа объекта.
        /// </summary>
        public int ObjectTypeId { get; set; }

        /// <summary>
        /// Количесво записей.
        /// </summary>
        public int Take { get; set; } = 20;

        /// <summary>
        /// Смещение.
        /// </summary>
        public int Skip { get; set; }
    }
}
