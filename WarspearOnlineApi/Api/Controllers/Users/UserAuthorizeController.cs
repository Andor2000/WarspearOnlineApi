using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Controllers.Attributies;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер для работы с авторизацией пользователей.
    /// </summary>
    [ApiController]
    [Route("api/user/authorize")]
    public class UserAuthorizeController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с авторизацией пользователей.
        /// </summary>
        private readonly UserAuthorizeService _userAuthorizeService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="userAuthorizeService">Сервис для работы с авторизацией пользователей.</param>
        public UserAuthorizeController(UserAuthorizeService userAuthorizeService)
        {
            this._userAuthorizeService = userAuthorizeService;
        }

        /// <summary>
        /// Проверка существования логина.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Признак существования логина.</returns>
        [HttpPost("CheckExistLoginAndFilledPassword")]
        public async Task<ActionResult<bool>> CheckExistLoginAndFilledPassword([FromBody] UserAuthorizeDto dto)
        {
            return Ok(await this._userAuthorizeService.CheckExistLoginAndFilledPassword(dto));
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="dto">Dto-модель для авторизации.</param>
        /// <returns>Модель авотризированного пользователя.</returns>
        [HttpPost("SignIn")]
        public async Task<ActionResult<UserSuccessAuthorizeDto>> SignIn([FromBody] UserAuthorizeDto dto)
        {
            return Ok(await this._userAuthorizeService.SignIn(dto)); 
        }
    }
}
