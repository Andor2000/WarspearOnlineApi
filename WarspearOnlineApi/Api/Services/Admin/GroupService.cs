using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с группами.
    /// </summary>
    public class GroupService : AdminBaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="jwtTokenService">Сервис для работы с токенами.</param>
        /// <param name="mapper">Маппер.</param>
        public GroupService(
            AppDbContext context,
            IMapper mapper,
            JwtTokenService jwtTokenService) : base(context, jwtTokenService)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение списка групп.
        /// </summary>
        /// <returns>Список групп</returns>
        public async Task<GroupDto[]> GetGroups()
        {
            var user = await this.GetAdminUserModel();

            return await this._context.wo_Group
                .Where(x => x.rf_ServerID == user.ServerId && x.rf_FractionID == user.FractionId)
                .ProjectTo<GroupDto>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        /// <summary>
        /// Добавить группу.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupDto> AddGroup(int serverId, int fractionId, string groupName)
        {
            // Админ сервера > выдает права админа, который редактирует конкретную группу
            // проверить что это админ конкретного сервера.
            var userId = this._jwtTokenService.GetUserIdFromToken()
                .ThrowOnCondition(x => x.IsNullOrDefault(), "В токене не указан пользователь");

            var user = await this._context.wo_User
                .Where(x => x.UserId == userId)
                .Select(x => new 
                {
                    x.UserId,
                    x.rf_ServerID,
                    x.rf_AccessLevel.AccessLevelCode,
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.UserId).IsNullOrDefault(), "Пользователь")
                .ThrowOnConditionAsync(x => x.rf_ServerID != serverId, "Вы не являетесь администратором сервера");


            if (user.AccessLevelCode != AccessLevelEnum.MainAdmin &&
                !await this._context.wo_UserServer.AnyAsync(x => x.rf_UserID == userId && x.rf_ServerID == serverId))
            {
                throw new Exception("У пользователя нет доступа к серверу");
            }

            return await this._context.wo_Group
                .ProjectTo<GroupDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
