using WarspearOnlineApi.Api.Enums;

namespace WarspearOnlineApi.Api.Models.Filters
{
    /// <summary>
    /// Фильтр для получения журнала дропа.
    /// </summary>
    public class JournalDropFilter : BaseFilterDto
    {
        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Идентификатор типа объекта.
        /// </summary>
        public int ObjectTypeId { get; set; }

        /// <summary>
        /// Идентификатор игрока.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Статус выплаты игроку.
        /// </summary>
        public DropPaymentStatus DropPaymentStatusPlayer { get; set; } = DropPaymentStatus.All;
    }
}
