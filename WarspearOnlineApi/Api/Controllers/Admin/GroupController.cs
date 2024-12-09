using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Services.Admin;

namespace WarspearOnlineApi.Api.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с группами.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin/[controller]")]
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
        /// Добавление группы.
        /// </summary>
        [RoleAuthorize(nameof(RoleEnum.AddDeleteGroup))]
        [HttpPost]
        public async Task<ActionResult> AddGroup()
        {
            return Ok(await this._groupService.AddGroup());
        }
    }
}
