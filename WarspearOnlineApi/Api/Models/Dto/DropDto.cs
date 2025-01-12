using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto
{
    /// <summary>
    /// Dto-модель дропа.
    /// </summary>
    public class DropDto : BaseModel
    {
        /// <summary>
        /// Дата дропа.
        /// </summary>
        public DateTime Date { get; set; } = DefaultsDates.MinDate;

        /// <summary>
        /// Цена.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество игроков.
        /// </summary>
        public int PlayersCount { get; set; }

        /// <summary>
        /// Доля \ часть денег.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Объект.
        /// </summary>
        public ObjectDto Object { get; set; } = new ObjectDto();

        /// <summary>
        /// Группа.
        /// </summary>
        public GroupDto Group { get; set; } = new GroupDto();

        /// <summary>
        /// Статус.
        /// </summary>
        public CodeNameBaseModel Status { get; set; } = new CodeNameBaseModel();
    }
}
