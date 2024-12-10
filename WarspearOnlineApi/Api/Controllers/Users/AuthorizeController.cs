using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер для работы с авторизацией пользователей.
    /// </summary>
    [ApiController]
    [Route("api/user/authorize")]
    public class AuthorizeController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с авторизацией пользователей.
        /// </summary>
        private readonly AuthorizeService _AuthorizeService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userAuthorizeService">Сервис для работы с авторизацией пользователей.</param>
        public AuthorizeController(AuthorizeService userAuthorizeService)
        {
            this._AuthorizeService = userAuthorizeService;
        }

        /// <summary>
        /// Проверка существования логина.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Признак существования логина.</returns>
        [HttpPost("CheckExistLoginAndFilledPassword")]
        public async Task<ActionResult<bool>> CheckExistLoginAndFilledPassword([FromBody] AuthorizeDto dto)
        {
            return Ok(await this._AuthorizeService.CheckExistLoginAndFilledPassword(dto));
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Модель авотризированного пользователя.</returns>
        [HttpPost("SignIn")]
        public async Task<ActionResult<SuccessAuthorizeDto>> SignIn([FromBody] AuthorizeDto dto)
        {
            return Ok(await this._AuthorizeService.SignIn(dto));
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Registration")]
        public async Task<ActionResult<SuccessAuthorizeDto>> Registration([FromBody] AuthorizeDto dto)
        {
            return Ok(await this._AuthorizeService.Registration(dto));
        }
    }
}
