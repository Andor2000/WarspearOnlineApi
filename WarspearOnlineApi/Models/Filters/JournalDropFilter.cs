namespace WarspearOnlineApi.Models.Filters
{
    /// <summary>
    /// Фильтр для получения журнала дропа.
    /// </summary>
    public class DropFilter
    {
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
