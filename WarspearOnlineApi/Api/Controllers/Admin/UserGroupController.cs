using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto.Intersections;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Admin;

namespace WarspearOnlineApi.Api.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы для работы с пользователем и группой.
    /// </summary>
    [Authorize]
    [RoleAuthorize(nameof(RoleEnum.AddDeleteGroup))]
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class UserGroupController : Controller
    {
        /// <summary>
        /// Сервис для работы с пользователем и группой.
        /// </summary>
        private readonly UserGroupService _userGroupService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userGroupService">Сервис для работы с пользователем и группой.</param>
        public UserGroupController(UserGroupService userGroupService)
        {
            this._userGroupService = userGroupService;
        }

        /// <summary>
        /// Получение списка связи пользователей группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <returns>Список связи пользователей группы.</returns>
        [HttpGet("{groupId}")]
        public async Task<ActionResult<UserGroupDto[]>> GetUserGroup(int groupId)
        {
            return Ok(await this._userGroupService.GetUserGroup(groupId));
        }

        /// <summary>
        /// Добавление связи пользователей группы.
        /// </summary>
        /// <param name="dto">Dto-модель связи пользователя и группы.</param>
        /// <returns>Связь пользователя с группой.</returns>
        public async Task<ActionResult<UserGroupDto>> AddUserGroup([FromBody] UserGroupDto dto)
        {
            return Ok(await this._userGroupService.AddUserGroup(dto));
        }

        /// <summary>
        /// Удаление связи пользователей с группой.
        /// </summary>
        /// <param name="userGroupId">Идентификатор пользователя с группой.</param>
        /// <returns>Строка сообщения.</returns>
        [HttpDelete("{userGroupId}")]
        public async Task<ActionResult<string>> DeleteUserGroup(int userGroupId)
        {
            return Ok(await this._userGroupService.DeleteUserGroup(userGroupId));
        }
    }
}
