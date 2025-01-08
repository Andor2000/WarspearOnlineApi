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
        public AccessLevelDto AccessLevel { get; set; } = new AccessLevelDto();

        /// <summary>
        /// Роли.
        /// </summary>
        public CodeNameBaseModel[] Roles { get; set; } = Array.Empty<CodeNameBaseModel>();
    }
}
