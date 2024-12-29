using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Services;

namespace WarspearOnlineApi.Api.Controllers
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
            this._playerService = playerService;
        }
    }
}
