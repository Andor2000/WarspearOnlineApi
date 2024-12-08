namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель для авторизации.
    /// </summary>
    public class UserAuthorizeDto
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get;set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get;set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get;set; }
    }
}
