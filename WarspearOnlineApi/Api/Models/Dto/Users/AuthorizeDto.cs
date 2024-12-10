namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель для авторизации.
    /// </summary>
    public class AuthorizeDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get;set; } = string.Empty;

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
