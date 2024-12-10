using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models;
using WarspearOnlineApi.Api.Models.Dto.Users;

namespace WarspearOnlineApi.Api.Services.Users
{
    public class JwtTokenService
    {
        /// <summary>
        /// Объект, содержащий настройки для генерации и верификации JWT (JSON Web Token).
        /// Используется для конфигурации и валидации параметров токенов, таких как секретный ключ, издатель, аудитория и время действия.
        /// </summary>
        private readonly JwtSetting jwtSetting = new JwtSetting();

        /// <summary>
        /// Объект, предоставляющий доступ к HTTP-запросу.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигуратор.</param>
        /// <param name="httpContextAccessor">Объект, предоставляющий доступ к HTTP-запросу.</param>
        public JwtTokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            jwtSetting.SecretKey = configuration["JwtSettings:SecretKey"];
            jwtSetting.Issuer = configuration["JwtSettings:Issuer"];
            jwtSetting.Audience = configuration["JwtSettings:Audience"];
            jwtSetting.ExpirationTimes = int.Parse(configuration["JwtSettings:ExpirationInTimes"]);

            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Генерация токена.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Токен.</returns>
        public void GenerateToken(SuccessAuthorizeDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var dateExpiresAt = DateTime.UtcNow.AddDays(jwtSetting.ExpirationTimes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserLogin", user.User.Login)
                }),
                Issuer = jwtSetting.Issuer,
                Audience = jwtSetting.Audience,
                Expires = dateExpiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.DateExpiresAt = dateExpiresAt;
            user.Token = tokenHandler.WriteToken(token);
        }


        /// <summary>
        /// Извлечение имени пользователя из токена, полученного из HTTP контекста.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Имя пользователя.</returns>
        public string GetUsernameFromToken(string token)
        {
            try
            {
                if (token.IsNullOrDefault())
                {
                    var authHeader = this._httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                    if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                    {
                        return null;
                    }
                    token = authHeader.Substring("Bearer ".Length).Trim();
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                return jwtToken.Claims.FirstOrDefault(c => c.Type == "UserLogin")?.Value;
            }
            catch
            {
                throw new Exception("Ошибка обработки токена.");
            }
        }


        /// <summary>
        /// Метод для получения даты окончания токена.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Дата.</returns>
        public JwtSecurityToken GetTokenExpiration(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken; // Возвращаем дату истечения токена
        }
    }
}
