using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Services;

namespace WarspearOnlineApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с игроками.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с игроками.
        /// </summary>
        private readonly PlayerService _playerService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="playerService">Сервис для работы с игроками.</param>
        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Получить список игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Список игроков.</returns>
        [HttpGet("byDrop/{dropId}")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayerByDropId(int dropId)
        {
            return Ok(await this._playerService.GetPlayerByDropId(dropId));
        }
    }
}
