namespace WarspearOnlineApi.Api.Models.Dto.Users
{
    /// <summary>
    /// Dto-модель для создания пользователя.
    /// </summary>
    public class SavingUserDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор уровня доступа.
        /// </summary>
        public int AccessLevelId { get; set; }
    }
}
