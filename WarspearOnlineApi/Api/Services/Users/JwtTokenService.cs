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
            this.jwtSetting.SecretKey = configuration["JwtSettings:SecretKey"];
            this.jwtSetting.Issuer = configuration["JwtSettings:Issuer"];
            this.jwtSetting.Audience = configuration["JwtSettings:Audience"];
            this.jwtSetting.ExpirationTimes = int.Parse(configuration["JwtSettings:ExpirationInTimes"]);

            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Генерация токена.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Токен.</returns>
        public void GenerateToken(UserSessionDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var dateExpiresAt = DateTime.UtcNow.AddDays(jwtSetting.ExpirationTimes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserID", user.Id.ToString())
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
        public int GetUserIdFromToken(string token = default)
        {
            try
            {
                if (token.IsNullOrDefault())
                {
                    var authHeader = this._httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                    if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                    {
                        return 0;
                    }
                    token = authHeader.Substring("Bearer ".Length).Trim();
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                return int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID").Value)
                    .ThrowOnCondition(x => x.IsNullOrDefault(), "В токене не указан идентификатор пользователя");
            }
            catch
            {
                throw new Exception("Ошибка обработки токена.");
            }
        }
    }
}
