using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarspearOnlineApi.Api.Models;

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
        /// <param name="username">Имя пользователя.</param>
        /// <returns>Токен.</returns>
        public string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Login", username)
                }),
                Issuer = jwtSetting.Issuer,
                Audience = jwtSetting.Audience,
                Expires = DateTime.UtcNow.AddDays(jwtSetting.ExpirationTimes),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        /// <summary>
        /// Извлечение имени пользователя из токена, полученного из HTTP контекста.
        /// </summary>
        /// <returns>Имя пользователя.</returns>
        public string GetUsernameFromToken()
        {
            try
            {
                var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return null;
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                return jwtToken.Claims.FirstOrDefault(c => c.Type == "Login")?.Value;
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
