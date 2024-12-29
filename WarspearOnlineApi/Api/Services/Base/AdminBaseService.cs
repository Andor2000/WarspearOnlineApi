using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto.Admin;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Base
{
    /// <summary>
    /// Базовый сервис администратора.
    /// </summary>
    public class AdminBaseService : BaseService
    {
        /// <summary>
        /// Сервис для работы с токенами.
        /// </summary>
        public readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        public AdminBaseService(
            AppDbContext context,
            JwtTokenService jwtTokenService) : base (context)
        {
            this._jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Получить модель администратора.
        /// </summary>
        /// <returns>Модель администратора.</returns>
        public async Task<UserAdminDto> GetAdminUserModel()
        {
            var userId = this._jwtTokenService.GetUserIdFromToken();
            return await this._context.wo_User
                .Where(x => x.UserId == userId)
                .Select(x => new UserAdminDto()
                {
                    Id = x.UserId,
                    ServerId = x.rf_ServerID,
                    FractionId = x.rf_FractionID,
                    AccessLevelCode = x.rf_AccessLevel.AccessLevelCode,
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.Id).IsNullOrDefault(), "Пользователь")
                .ThrowOnConditionAsync(x => x.ServerId == 0, "У пользователя не указан сервер")
                .ThrowOnConditionAsync(x => x.FractionId == 0, "У пользователя не указана фракция");
        }
    }
}
