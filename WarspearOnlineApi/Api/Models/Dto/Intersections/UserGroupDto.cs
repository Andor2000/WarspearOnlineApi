using WarspearOnlineApi.Api.Models.Dto.Users;

namespace WarspearOnlineApi.Api.Models.Dto.Intersections
{
    /// <summary>
    /// Связь пользователя с группой.
    /// </summary>
    public class UserGroupDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserDto User { get; set; } = new UserDto();
    }
}
