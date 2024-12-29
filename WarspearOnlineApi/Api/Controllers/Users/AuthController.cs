using Microsoft.AspNetCore.Mvc;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Controllers.Users
{
    /// <summary>
    /// Контроллер для работы с токенами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с токенами.
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        public AuthController(JwtTokenService jwtTokenService)
        {
            this._jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Получение токена.
        /// </summary>
        /// <param name="username">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns></returns>
        //[HttpPost("login")]
        //public IActionResult UserLogin(string username, string password)
        //{
        //    // В реальном приложении тут будет логика для проверки учетных данных.
        //    if (username == "test" && password == "password") // Пример
        //    {
        //        return Ok(_jwtTokenService.GenerateToken(username));
        //    }

        //    return Unauthorized();
        //}

        //[HttpGet("get-username")]
        //public IActionResult Logout()
        //{
        //    return Ok(new { Username = _jwtTokenService.GetUserIdFromToken() });
        //}

        //[HttpGet("token-expiration")]
        //public IActionResult GetTokenExpiration(string token)
        //{
        //    return Ok(new
        //    {
        //        Now_______Date = DateTime.UtcNow,
        //        ExpirationDate = _jwtTokenService.GetTokenExpiration(token)
        //    });
        //}
    }
}
