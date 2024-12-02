using WarspearOnlineApi.Models.BaseModels;

namespace WarspearOnlineApi.Models.Dto
{
    /// <summary>
    /// Dto-модель группы.
    /// </summary>
    public class GroupDto : NameBaseModel
    {
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
