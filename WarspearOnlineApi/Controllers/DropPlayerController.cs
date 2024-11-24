using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Services;

namespace WarspearOnlineApi.Controllers
{
    public class DropPlayerController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с дропом.
        /// </summary>
        private readonly DropPlayerService _dropPlayerService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dropService">Сервис для работы с дропом.</param>
        public DropPlayerController(DropPlayerService dropPlayerService)
        {
            this._dropPlayerService = dropPlayerService;
        }

        /// <summary>
        /// Получение информации о дропе.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Дроп.</returns>
        [HttpGet("{dropId}")]
        public async Task<ActionResult<DropDto>> GetDrop(int dropId)
        {
            return Ok(await this._dropPlayerService.GetDrop(dropId));
        }

        /// <summary>
        /// Создание новой записи дропа.
        /// </summary>
        /// <param name="dto">Dto-модель дропа.</param>
        /// <returns>Дроп.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DropDto>> AddDrop(DropDto dto)
        {
            return Ok(await this._dropService.AddDrop(dto));
        }

        /// <summary>
        /// Редактирование дропа.
        /// </summary>
        /// <param name="dto">Dto-модель дропа.</param>
        /// <returns>Дроп.</returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<DropDto>> EditDrop(DropDto dto)
        {
            return Ok(await this._dropService.EditDrop(dto));
        }
    }
}
