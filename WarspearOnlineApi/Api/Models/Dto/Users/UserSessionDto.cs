using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель сессии пользователя.
    /// </summary>
    public class UserSessionDto : NameBaseModel
    {
        /// <summary>
        /// Токен.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время истечения срока действия токена.
        /// </summary>
        public DateTime DateExpiresAt { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public CodeNameBaseModel AccessLevel { get; set; } = new CodeNameBaseModel();

        /// <summary>
        /// Роли.
        /// </summary>
        public CodeNameBaseModel[] Roles { get; set; } = Array.Empty<CodeNameBaseModel>();
    }
}
