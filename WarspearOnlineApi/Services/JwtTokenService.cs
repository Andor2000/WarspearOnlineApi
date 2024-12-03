using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarspearOnlineApi.Models;

namespace WarspearOnlineApi.Services
{
    public class JwtTokenService
    {
        /// <summary>
        /// Объект, содержащий настройки для генерации и верификации JWT (JSON Web Token).
        /// Используется для конфигурации и валидации параметров токенов, таких как секретный ключ, издатель, аудитория и время действия.
        /// </summary>
        private readonly JwtSetting jwtSetting = new JwtSetting();

        public JwtTokenService(IConfiguration configuration)
        {
            jwtSetting.SecretKey = configuration["JwtSetting:SecretKey"];
            jwtSetting.Issuer = configuration["JwtSetting:Issuer"];
            jwtSetting.Audience = configuration["JwtSetting:Audience"];
            jwtSetting.ExpirationTimes = int.Parse(configuration["JwtSetting:ExpirationInTimes"]);
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
                    new Claim(ClaimTypes.Name, username)
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
        /// Метод для получения даты окончания токена.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Дата.</returns>
        public DateTime GetTokenExpiration(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo; // Возвращаем дату истечения токена
        }
    }
}
