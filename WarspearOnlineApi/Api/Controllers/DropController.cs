﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Services;

namespace WarspearOnlineApi.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы со страницей дропа.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DropController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с дропом.
        /// </summary>
        private readonly DropService _dropService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dropService">Сервис для работы с дропом.</param>
        public DropController(DropService dropService)
        {
            this._dropService = dropService;
        }

        /// <summary>
        /// Получение информации о дропе.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns>Дроп.</returns>
        [HttpGet("{dropId}")]
        public async Task<ActionResult<DropDto>> GetDrop(int dropId)
        {
            return Ok(await this._dropService.GetDrop(dropId));
        }

        /// <summary>
        /// Создание новой записи дропа.
        /// </summary>
        /// <param name="dto">Dto-модель дропа.</param>
        /// <returns>Дроп.</returns>
        [Authorize]
        [RoleAuthorize(nameof(RoleEnum.AddDrop))]
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
        [RoleAuthorize(nameof(RoleEnum.AddDrop))]
        [HttpPut]
        public async Task<ActionResult<DropDto>> EditDrop(DropDto dto)
        {
            return Ok(await this._dropService.EditDrop(dto));
        }

        /// <summary>
        /// Удаление дропа.
        /// </summary>
        /// <param name="dropId">Идентификатор дропа.</param>
        /// <returns></returns>
        [Authorize]
        [RoleAuthorize(nameof(RoleEnum.DeleteDrop))]
        [HttpDelete]
        public async Task<ActionResult<string>> Delete(int dropId)
        {
            return Ok(await this._dropService.Delete(dropId));
        }
    }
}
