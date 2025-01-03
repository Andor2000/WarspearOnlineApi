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
        public async Task<GroupDto> AddGroup(string groupName)
        {
            groupName.ValidateGroupName();
            var user = await this.GetAdminUserModel();

            await this._context.wo_Group
                .AnyAsync(x => x.GroupName == groupName &&
                               x.rf_ServerID == user.ServerId &&
                               x.rf_FractionID == user.FractionId)
                .ThrowOnConditionAsync(x => x, "Группа с таким именем уже существует");

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
            var user = await this.GetAdminUserModel();
            var group = await this._context.wo_Group
                .Where(x => x.GroupID == groupId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор группы."))
                .Select(x => new
                {
                    x.GroupID
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.GroupID).IsNullOrDefault() ,"Дроп");

            return "Группа была успешно удалена";
        }
    }
}
