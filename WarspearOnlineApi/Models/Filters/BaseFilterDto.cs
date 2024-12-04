namespace WarspearOnlineApi.Models.Filters
{
    /// <summary>
    /// Базовый фильтр.
    /// </summary>
    public class BaseFilterDto
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
        /// Количесво записей.
        /// </summary>
        public int Take { get; set; } = 20;

        /// <summary>
        /// Смещение.
        /// </summary>
        public int Skip { get; set; }
    }
}
