using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Admin;

namespace WarspearOnlineApi.Api.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [Authorize]
    [RoleAuthorize(nameof(RoleEnum.AddUser))]
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class UserControlles : Controller
    {
        /// <summary>
        /// Сервис для работы с пользователями.
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UserControlles(UserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для создания пользователя.</param>
        /// <returns>Сообщение о успешном добавлении пользователя.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(SavingUserDto dto)
        {
            return Ok(await this._userService.AddUser(dto));
        }

        /// <summary>
        /// Обновление уровня доступа пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="accessLevelId">Уровень доступа.</param>
        /// <returns></returns>
        [HttpPut("AccessLevel/{userId}")]
        public async Task<ActionResult<UserDto>> UpdateAccessLevel(SavingUserDto dto)
        {
            return Ok(await this._userService.UpdateAccessLevel(dto));
        }
    }
}
