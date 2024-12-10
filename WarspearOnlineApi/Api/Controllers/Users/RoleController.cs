using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Users;

// СКОРЕЕ ВСЕГО УДАЛИТЬ
namespace WarspearOnlineApi.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер для работы с ролями.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с ролями.
        /// </summary>
        private readonly RoleService _roleService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userService">Сервис для работы с ролями.</param>
        public RoleController(RoleService userService) {
            this._roleService = userService;
        }

        /// <summary>
        /// Получение списка ролей.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Roles")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetRoles()
        {
            return Ok(await this._roleService.GetRoleCodes());
        }
    }
}
