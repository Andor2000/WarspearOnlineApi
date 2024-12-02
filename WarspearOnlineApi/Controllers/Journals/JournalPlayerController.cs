using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Services.Journals;

namespace WarspearOnlineApi.Controllers.Journals
{
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
            _journalPlayerService = dropPlayerService;
        }
    }
}
