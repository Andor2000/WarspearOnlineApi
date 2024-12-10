using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с группами.
    /// </summary>
    public class GroupService : BaseService
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="mapper">Маппер.</param>
        public GroupService(AppDbContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Добавить группу.
        /// </summary>
        /// <returns></returns>
        public async Task AddGroup(int serverId, int fractionId, string groupName)
        {
            // Админ сервера > выдает права админа, который редактирует конкретную группу
            // проверить что это админ конкретного сервера.
            var userId = 1;

            var userAccessLevelCode = await this._context.wo_User
                .Where(x => x.UserId == userId)
                .Select(x => x.rf_AccessLevel.AccessLevelCode)
                .FirstOrDefaultAsync();

            if(userAccessLevelCode != AccessLevelEnum.MainAdmin && 
                !await this._context.wo_UserServer.AnyAsync(x => x.rf_UserID == userId && x.rf_ServerID == serverId))
            {
                throw new Exception("У пользователя нет доступа к серверу");
            }



        }
    }
}
