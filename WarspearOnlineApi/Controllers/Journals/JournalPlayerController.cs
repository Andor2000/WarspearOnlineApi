﻿using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Models.Dto.Journals;
using WarspearOnlineApi.Models.Filters;
using WarspearOnlineApi.Services.Journals;

namespace WarspearOnlineApi.Controllers.Journals
{
    /// <summary>
    /// Контроллер для работы с журналом игроков.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JournalPlayerController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с дропом.
        /// </summary>
        private readonly JournalPlayerService _journalPlayerService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dropService">Сервис для работы с дропом.</param>
        public JournalPlayerController(JournalPlayerService dropPlayerService)
        {
            this._journalPlayerService = dropPlayerService;
        }

        /// <summary>
        /// Получение списка игроков.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список игроков.</returns>
        [HttpGet]
        public async Task<ActionResult<JournalPlayerDto[]>> GetJournalPlayers(JournalPlayerFilterDto filter)
        {
            return Ok(await this._journalPlayerService.GetJournalPlayers(filter));
        }

        /// <summary>
        /// Получить журнал игроков.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количество игроков.</returns>
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetJournalPlayersCount(JournalPlayerFilterDto filter)
        {
            return Ok(await this._journalPlayerService.GetJournalPlayersCount(filter));
        }
    }
}
