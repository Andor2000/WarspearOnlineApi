using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер для работы с пользователями.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с пользователем.
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователем.</param>
        public UserController(UserService userService) {
            this._userService = userService;
        }

        /// <summary>
        /// Получение списка ролей.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Roles")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetRoles()
        {
            return Ok(await this._userService.GetRoleCodes());
        }
    }
}
