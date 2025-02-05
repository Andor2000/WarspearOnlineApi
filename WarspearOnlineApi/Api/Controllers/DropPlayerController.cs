﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Services;

namespace WarspearOnlineApi.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с интерсекцией дропа и игрока.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DropPlayerController : Controller
    {
        /// <summary>
        /// Сервис для работы с интерсекцией дропа и игрока.
        /// </summary>
        private readonly DropPlayerService _dropPlayerService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dropPlayerService">Сервис для работы с интерсекцией дропа и игрока.</param>
        public DropPlayerController(DropPlayerService dropPlayerService)
        {
            this._dropPlayerService = dropPlayerService;
        }

        /// <summary>
        /// Получить список игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Список игроков.</returns>
        [HttpGet("List/{dropId}")]
        public async Task<ActionResult<DropPlayerDto[]>> GetPlayerByDropId(int dropId)
        {
            return Ok(await this._dropPlayerService.GetPlayerByDropId(dropId));
        }

        /// <summary>
        /// Получить количество игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Количество игроков.</returns>
        [HttpGet("List/Count/{dropId}")]
        public async Task<ActionResult<int>> GetCountPlayerByDropId(int dropId)
        {
            return Ok(await this._dropPlayerService.GetCountPlayerByDropId(dropId));
        }

        /// <summary>
        /// Добавить игрока в список дропа.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        [Authorize]
        [RoleAuthorize(nameof(RoleEnum.AddDeletePlayerInDrop))]
        [HttpPost]
        public async Task<ActionResult<DropPlayerDto>> Add([FromBody] DropPlayerDto dto, int dropId)
        {
            return Ok(await this._dropPlayerService.Add(dto, dropId));
        }

        /// <summary>
        /// Обновить игрока в списке дропа.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        [Authorize]
        [RoleAuthorize(nameof(RoleEnum.AddDeletePlayerInDrop))]
        [HttpPut]
        public async Task<ActionResult<DropPlayerDto>> Update([FromBody] DropPlayerDto dto, [FromQuery] int dropId)
        {
            return Ok(await this._dropPlayerService.Update(dto, dropId));
        }

        /// <summary>
        /// Удалить связь игрока с дропом.
        /// </summary>
        /// <param name="dropPlayerId">Идентификатор связи игрока с дропом.</param>
        /// <returns>Сообщение.</returns>
        [Authorize]
        [RoleAuthorize(nameof(RoleEnum.AddDeletePlayerInDrop))]
        [HttpDelete("{dropPlayerId}")]
        public async Task<ActionResult<string>> Delete(int dropPlayerId)
        {
            return Ok(await this._dropPlayerService.Delete(dropPlayerId));
        }
    }
}
