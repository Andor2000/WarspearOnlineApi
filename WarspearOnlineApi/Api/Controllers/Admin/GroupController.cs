using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Services.Admin;

namespace WarspearOnlineApi.Api.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с группами.
    /// </summary>
    [Authorize]
    [RoleAuthorize(nameof(RoleEnum.AddDeleteGroup))]
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class GroupController : Controller
    {
        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        private readonly GroupService _groupService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="guildService">Сервис для работы с группами.</param>
        public GroupController(GroupService groupService)
        {
            this._groupService = groupService;
        }


        /// <summary>
        /// Получение списка групп.
        /// </summary>
        /// <returns>Список групп</returns>
        [HttpGet]
        public async Task<ActionResult<GroupDto[]>> GetGroups()
        {
            return Ok(await this._groupService.GetGroups());
        }

        /// <summary>
        /// Добавление группы.
        /// </summary>
        /// <param name="groupName">Название группы.</param>
        /// <returns>Группа.</returns>
        [HttpPost]
        public async Task<ActionResult<GroupDto>> AddGroup(string groupName)
        {
            return Ok(await this._groupService.AddGroup(groupName));
        }

        /// <summary>
        /// Удаление группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <returns>Строка.</returns>
        [HttpDelete("{groupId}")]
        public async Task<ActionResult<string>> DeleteGroup(int groupId)
        {
            return Ok(await this._groupService.DeleteGroup(groupId));
        }
    }
}
