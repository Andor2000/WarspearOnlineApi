using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
    [RoleAuthorize(nameof(RoleEnum.AddGuild))]
    [ApiController]
    [Route("api/Admin/[controller]")]
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
            this._guildService = guildService;
        }

        /// <summary>
        /// Получение списка гильдий.
        /// </summary>
        /// <returns>Список гильдий.</returns>
        [HttpGet("List")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetGuilds()
        {
            return Ok(await this._guildService.GetGuilds());
        }

        /// <summary>
        /// Добавление гильдии.
        /// </summary>
        /// <param name="guildName">Наименование гильдии.</param>
        /// <returns>Гильдия.</returns>
        [HttpPost]
        public async Task<ActionResult<CodeNameBaseModel>> AddGuild(string guildName)
        {
            return Ok(await this._guildService.AddGuild(guildName));
        }
    }
}
