namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель для успешной авторизации.
    /// </summary>
    public class UserSuccessAuthorizeDto
    {
        /// <summary>
        /// Токен.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Дата и время истечения срока действия токена.
        /// </summary>
        public DateTime DateExpiresAt { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserDto User { get; set; } = new UserDto();
    }
}
