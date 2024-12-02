using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель связи дропа с игроком.
    /// </summary>
    public class DropPlayerDto : BaseModel
    {
        /// <summary>
        /// Доля.
        /// </summary>
        public int Part { get; set; }

        /// <summary>
        /// Признак выплаты.
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Класс
        /// </summary>
        public PlayerDto Player { get; set; } = new PlayerDto();
    }
}
