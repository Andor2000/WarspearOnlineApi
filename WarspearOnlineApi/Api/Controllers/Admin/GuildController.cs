using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Admin;

namespace WarspearOnlineApi.Api.Controllers.Admin
{
    /// <summary>
    /// Контроллер для работы с гильдиями.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class GuildController : Controller
    {
        /// <summary>
        /// Сервис для работы с гильдиями.
        /// </summary>
        private readonly GuildService _guildService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="guildService">Сервис для работы с гильдиями.</param>
        public GuildController(GuildService guildService)
        {
            _guildService = guildService;
        }

        /// <summary>
        /// Получение списка гильдий.
        /// </summary>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Список гильдий.</returns>
        [RoleAuthorize(nameof(RoleEnum.AddGuild))]
        [HttpGet("List")]
        public async Task<ActionResult<CodeNameBaseModel>> GetGuilds(int serverId, int fractionId)
        {
            return Ok(await this._guildService.GetGuilds(serverId, fractionId));
        }
    }
}
