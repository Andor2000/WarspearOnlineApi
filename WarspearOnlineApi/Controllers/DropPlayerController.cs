using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Services;

namespace WarspearOnlineApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с интерсекцией дропа и игрока.
    /// </summary>
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
            _dropPlayerService = dropPlayerService;
        }

        /// <summary>
        /// Получить список игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Список игроков.</returns>
        [HttpGet("List/{dropId}")]
        public async Task<ActionResult<DropPlayerDto[]>> GetPlayerByDropId(int dropId)
        {
            return Ok(await _dropPlayerService.GetPlayerByDropId(dropId));
        }

        /// <summary>
        /// Получить количество игроков по дропу.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Количество игроков.</returns>
        [HttpGet("List/Count/{dropId}")]
        public async Task<ActionResult<int>> GetCountPlayerByDropId(int dropId)
        {
            return Ok(await _dropPlayerService.GetCountPlayerByDropId(dropId));
        }

        /// <summary>
        /// Добавить игрока в список дропа.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DropPlayerDto>> Add([FromBody] DropPlayerDto dto, int dropId)
        {
            return Ok(await _dropPlayerService.Add(dto, dropId));
        }

        /// <summary>
        /// Обновить игрока в списке дропа.
        /// </summary>
        /// <param name="dto">Dto-модель игрока.</param>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Модель игрока.</returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<DropPlayerDto>> Update([FromBody] DropPlayerDto dto, [FromQuery] int dropId)
        {
            return Ok(await _dropPlayerService.Update(dto, dropId));
        }

        /// <summary>
        /// Удалить связь игрока с дропом.
        /// </summary>
        /// <param name="dropPlayerId">Идентификатор связи игрока с дропом.</param>
        /// <returns>Сообщение.</returns>
        [Authorize]
        [HttpDelete("{dropPlayerId}")]
        public async Task<ActionResult<string>> Delete(int dropPlayerId)
        {
            return Ok(await _dropPlayerService.Delete(dropPlayerId));
        }
    }
}
