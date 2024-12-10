using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto
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
        public CodeNameBaseModel Class { get; set; } = new CodeNameBaseModel();

        /// <summary>
        /// Сервер.
        /// </summary>
        public CodeNameBaseModel Server { get; set; } = new CodeNameBaseModel();

        /// <summary>
        /// Фракция.
        /// </summary>
        public CodeNameBaseModel Fraction { get; set; } = new CodeNameBaseModel();
    }
}
