using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с группами.
    /// </summary>
    public class GroupService : UserBaseService
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
            var user = await this.GetAdminUserModelAsync();
            var query = this._context.wo_Group.Where(x => x.rf_ServerID == user.ServerId && x.rf_FractionID == user.FractionId);

            if (user.AccessLevelCode.LevelValue() < AccessLevelEnum.LevelValue(nameof(AccessLevelEnum.AdminServer)))
            {
                var groupIds = await this.GetUserGroupIdsAsync();
                query = query.Where(x => groupIds.Contains(x.GroupID));
            }

            return await query
                .ProjectTo<GroupDto>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        /// <summary>
        /// Добавить группу.
        /// </summary>
        /// <returns></returns>
        public async Task<GroupDto> AddGroup(string groupName)
        {
            groupName.ValidateGroupName();
            var user = await this.GetAdminUserModelAsync();

            await this._context.wo_Group
                .AnyAsync(x => x.GroupName == groupName &&
                               x.rf_ServerID == user.ServerId &&
                               x.rf_FractionID == user.FractionId)
                .ThrowOnConditionAsync(x => x, "Группа с таким названием уже существует");

            var entity = new wo_Group
            {
                GroupName = groupName,
                rf_UserID = user.Id,
                rf_ServerID = user.ServerId,
                rf_FractionID = user.FractionId
            };

            await this._context.wo_Group.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return await this._context.wo_Group
                .Where(x => x.GroupID == entity.GroupID)
                .ProjectTo<GroupDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Удаление группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <returns>Строка.</returns>
        public async Task<string> DeleteGroup(int groupId)
        {
            groupId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор группы");
            await this.CheckUserHasGroupAsync(groupId);
            await this._context.wo_Drop.AnyAsync(x => x.rf_GroupID == groupId)
                .ThrowOnConditionAsync(x => x, "Нельзя удалить группу к которой относится дроп");

            this._context.wo_Group.Remove(new wo_Group { GroupID = groupId });
            await this._context.SaveChangesAsync();

            return "Группа была успешно удалена";
        }
    }
}
