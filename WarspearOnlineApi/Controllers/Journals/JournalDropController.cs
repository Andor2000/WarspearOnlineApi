using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Filters;
using WarspearOnlineApi.Services.Journals;

namespace WarspearOnlineApi.Controllers.Journals
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
        public async Task<ActionResult<DropDto[]>> GetJournalDrop(JournalDropFilter filter)
        {
            return Ok(await journalDropService.GetJournalDrop(filter));
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количество дропа.</returns>
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetJournalDropCount(JournalDropFilter filter)
        {
            return Ok(await journalDropService.GetJournalDropCount(filter));
        }
    }
}
