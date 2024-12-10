using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.SqlQueries;

namespace WarspearOnlineApi.Api.Services.Users
{
    /// <summary>
    /// Сервис для работы с ролями.
    /// </summary>
    public class RoleService : BaseService
    {
        /// <summary>
        /// Сервис для работы с токенами.
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        /// <param name="mapper">Маппер.</param>
        public RoleService(
            AppDbContext context,
            JwtTokenService jwtTokenService,
            IMapper mapper) : base(context)
        {
            this._jwtTokenService = jwtTokenService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Получить роли для пользователя.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <returns>Список ролей пользователя.</returns>
        public async Task<CodeNameBaseModel[]> GetRoleCodes(string token = "")
        {
            var username = this._jwtTokenService.GetUsernameFromToken(token)
                .ThrowOnCondition(x => x.IsNullOrDefault(), "В токене не указан пользователь");

            var user = await this._context.wo_User
                .Where(u => u.Login == username)
                .Select(x => new
                {
                    x.UserId,
                    x.rf_AccessLevelID
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.UserId).IsNullOrDefault(), "Пользователь");

            var roleIds = await this._context.Database.GetDbConnection()
                .QueryAsync<int>(SqlRole.GetRolesByAccessLevel, new { accessLevelId = user.rf_AccessLevelID })
                .ThrowOnConditionAsync(x => x.Count() == 0, "Пользователь существует, но у него нет доступных ролей");

            return await this._context.wo_Role
                .Where(x => roleIds.Contains(x.RoleID))
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}
