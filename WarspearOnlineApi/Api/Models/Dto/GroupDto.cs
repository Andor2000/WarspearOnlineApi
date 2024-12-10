using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto
{
    /// <summary>
    /// Dto-модель группы.
    /// </summary>
    public class GroupDto : NameBaseModel
    {
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
