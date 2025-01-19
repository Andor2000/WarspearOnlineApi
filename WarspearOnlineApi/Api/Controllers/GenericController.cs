using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services;

namespace WarspearOnlineApi.Api.Controllers
{
    /// <summary>
    /// Контроллер для получения общих данных.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : Controller
    {
        /// <summary>
        /// Сервис для получения общих данных.
        /// </summary>
        private readonly GenericService _genericService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="genericService">Сервис для получения общих данных.</param>
        public GenericController(GenericService genericService)
        {
            this._genericService = genericService;
        }

        /// <summary>
        /// Получение информации о сервере.
        /// </summary>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <returns>Сервер.</returns>
        [HttpGet("Server/{serverId}")]
        public async Task<ActionResult<CodeNameBaseModel>> GetServer(int serverId)
        {
            return Ok(await this._genericService.GetServer(serverId));
        }

        /// <summary>
        /// Получение списка серверов.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <returns>Список серверов.</returns>
        [HttpGet("Server/List")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetServerList(string search)
        {
            return Ok(await this._genericService.GetServerList(search));
        }

        /// <summary>
        /// Получение информации о фракции.
        /// </summary>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Фракция.</returns>
        [HttpGet("Fraction/{fractionId}")]
        public async Task<ActionResult<CodeNameBaseModel>> GetFraction(int fractionId)
        {
            return Ok(await this._genericService.GetFraction(fractionId));
        }

        /// <summary>
        /// Получение списка фракций.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <returns>Список фракций.</returns>
        [HttpGet("Fraction/List")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetFractionList(string search)
        {
            return Ok(await this._genericService.GetFractionList(search));
        }

        /// <summary>
        /// Получение информации о классе.
        /// </summary>
        /// <param name="fractionId">Идентификатор фракции.</param>
        /// <returns>Класс.</returns>
        [HttpGet("Class/{fractionId}")]
        public async Task<ActionResult<CodeNameBaseModel>> GetClass(int classId)
        {
            return Ok(await this._genericService.GetFraction(classId));
        }

        /// <summary>
        /// Получение списка классов.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <param name="fractionCode">Код фракции.</param>
        /// <returns>Список классов.</returns>
        [HttpGet("Class/List")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetClassList(string search, FractionType fractionCode)
        {
            return Ok(await this._genericService.GetClassList(search, fractionCode));
        }

        /// <summary>
        /// Получить список статусов дропа.
        /// </summary>
        /// <param name="search">Строка поиска.</param>
        /// <returns>Список статусов дропа.</returns>
        [HttpGet("DropStatuses")]
        public async Task<ActionResult<CodeNameBaseModel[]>> GetDropStatuses(string search)
        {
            return Ok(await this._genericService.GetDropStatuses(search));
        }
    }
}
