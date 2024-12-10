using System.Text.Json.Serialization;
using WarspearOnlineApi.Api.Models.BaseModels;

namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    ///  Dto-модель пользователя.
    /// </summary>
    public class UserDto : NameBaseModel
    {
        /// <summary>
        /// Уровень доступа.
        /// </summary>
        public CodeNameBaseModel AccessLevel { get; set; } = new CodeNameBaseModel();

        /// <summary>
        /// Роли.
        /// </summary>
        public CodeNameBaseModel[] Roles { get; set; } = Array.Empty<CodeNameBaseModel>();

        /// <summary>
        /// Логин.
        /// </summary>
        [JsonIgnore]
        public string Login { get; set; } = string.Empty;
    }
}
