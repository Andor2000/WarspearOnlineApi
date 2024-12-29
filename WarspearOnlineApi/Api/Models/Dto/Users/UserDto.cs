using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    ///  Dto-модель пользователя.
    /// </summary>
    public class UserDto : NameBaseModel
    {
        /// <summary>
        /// Ранг уровня доступа.
        /// </summary>
        public int RangeAccessLevel { get; set; }

        /// <summary>
        /// Dto-модель уровня доступа.
        /// </summary>
        public AccessLevelDto AccessLevel { get; set; } = new AccessLevelDto();

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
