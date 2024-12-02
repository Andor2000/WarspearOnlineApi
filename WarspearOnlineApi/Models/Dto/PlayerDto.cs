using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель игрока
    /// </summary>
    public class PlayerDto : BaseModel
    {
        /// <summary>
        /// Ник.
        /// </summary>
        public string Nick { get; set; } = string.Empty;

        /// <summary>
        /// Класс.
        /// </summary>
        public ClassDto Class { get; set; } = new ClassDto();

        /// <summary>
        /// Сервер.
        /// </summary>
        public ServerDto Server { get; set; } = new ServerDto();

        /// <summary>
        /// Фракция.
        /// </summary>
        public FractionDto Fraction { get; set; } = new FractionDto();
    }
}
