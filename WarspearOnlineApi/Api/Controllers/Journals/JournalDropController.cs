using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Filters;
using WarspearOnlineApi.Api.Services.Journals;

namespace WarspearOnlineApi.Api.Controllers.Journals
{
    /// <summary>
    /// Контроллер для работы с журналом дропа.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JournalDropController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с журналом дропа.
        /// </summary>
        public readonly JournalDropService journalDropService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public JournalDropController(JournalDropService journalDropService)
        {
            this.journalDropService = journalDropService;
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Журнал дропа.</returns>
        [HttpGet]
        public async Task<ActionResult<DropDto[]>> GetJournalDrop([FromQuery] JournalDropFilter filter)
        {
            return Ok(await journalDropService.GetJournalDrop(filter));
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количество дропа.</returns>
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetJournalDropCount([FromQuery] JournalDropFilter filter)
        {
            return Ok(await journalDropService.GetJournalDropCount(filter));
        }
    }
}
