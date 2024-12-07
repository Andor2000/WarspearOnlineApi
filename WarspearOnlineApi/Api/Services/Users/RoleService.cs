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
    /// Сервис для работы с ролями пользователей.
    /// </summary>
    public class RoleService : BaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст.</param>
        public RoleService(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Получить роли для пользователя.
        /// </summary>
        /// <param name="username">Логин пользователя.</param>
        /// <returns>Список ролей пользователя.</returns>
        public async Task<CodeNameBaseModel[]> GetRoleCodes(string username)
        {
            // Получаем пользователя
            var user = await _context.wo_User
                .Where(u => u.Login == username)
                .Select(x => new
                {
                    x.Login,
                    x.rf_AccessLevelID
                }).FirstOrDefaultAsync()
                .ThrowIfNullAsync("Пользователь");

            var roleIds = await _context.Database.GetDbConnection()
                .QueryAsync<int>(SqlRole.GetRolesByAccessLevel, new { accessLevelId = user.rf_AccessLevelID })
                .ThrowOnConditionAsync(x => x.Count() == 0, "У пользователя нет доступных ролей");

            return await _context.wo_Role
                .Where(x => roleIds.Contains(x.RoleID))
                .ProjectTo<CodeNameBaseModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}
