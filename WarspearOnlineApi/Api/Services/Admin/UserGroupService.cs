using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto.Intersections;
using WarspearOnlineApi.Api.Models.Dto.Users;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Models.Entity.Intersections;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с пользователем и группой.
    /// </summary>
    public class UserGroupService : UserBaseService
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
        public UserGroupService(
            AppDbContext context,
            IMapper mapper,
            JwtTokenService jwtTokenService) : base(context, jwtTokenService)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение списка связи пользователей группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <returns>Список связи пользователей группы.</returns>
        public async Task<UserGroupDto[]> GetUserGroup(int groupId)
        {
            groupId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор группы");
            return await this._context.wo_UserGroup
                .Where(x => x.rf_GroupID == groupId)
                .Select(x => x.rf_User)
                .ProjectTo<UserGroupDto>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        /// <summary>
        /// Добавление связи пользователей группы.
        /// </summary>
        /// <param name="dto">Dto-модель связи пользователя и группы.</param>
        /// <returns>Связь пользователя с группой.</returns>
        public async Task<UserGroupDto> AddUserGroup(UserGroupDto dto)
        {
            dto.ThrowOnCondition(x => x.GroupId.IsNullOrDefault(), "Не указан идентификатор группы")
                .ThrowOnCondition(x => x.User.Id.IsNullOrDefault(), "Не указан идентификатор пользователя");
            await this.GetUserGroup(dto.GroupId);

            var user = await this.GetAdminUserModelAsync();
            var entity = new wo_UserGroup()
            {
                rf_GroupID = dto.GroupId,
                rf_UserID = (await this._context.wo_User
                    .Where(x => x.UserId == dto.User.Id)
                    .Select(x => new
                    {
                        x.UserId,
                        x.rf_ServerID,
                        x.rf_FractionID,
                    }).FirstOrDefaultAsync()
                    .ThrowNotFoundAsync(x => (x?.UserId).IsNullOrDefault(), "Пользователь")
                    .ThrowOnConditionAsync(x => x.rf_ServerID != user.ServerId, "Пользователь находится на другом сервере")
                    .ThrowOnConditionAsync(x => x.rf_FractionID != user.FractionId, "Пользователь находится в противоположной фракции")
                    ).UserId
            };

            await this._context.wo_UserGroup.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return await this._context.wo_UserGroup
                .Where(x => x.UserGroupID == entity.UserGroupID)
                .ProjectTo<UserGroupDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Удаление связи пользователей с группой.
        /// </summary>
        /// <param name="userGroupId">Идентификатор пользователя с группой.</param>
        /// <returns>Строка сообщения.</returns>
        public async Task<string> DeleteUserGroup(int userGroupId)
        {
            userGroupId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификтаор пользователя с группой");
            var user = await this.GetAdminUserModelAsync();

            await this._context.wo_UserGroup
                .Where(x => x.UserGroupID == userGroupId)
                .Select(x => new
                {
                    x.UserGroupID,
                    x.rf_GroupID,
                    x.rf_Group.rf_ServerID,
                    x.rf_Group.rf_FractionID,
                }).FirstOrDefaultAsync()
                .ThrowNotFoundAsync(x => (x?.UserGroupID).IsNullOrDefault(), "Связь пользователя с группой")
                .ThrowOnConditionAsync(x => x.rf_ServerID != user.ServerId, "У вас нет прав редактирования группы на этом сервере")
                .ThrowOnConditionAsync(x => x.rf_FractionID != user.FractionId, "У вас нет прав на редактирование группы в этой фракции");

            this._context.wo_UserGroup.Remove(new wo_UserGroup { UserGroupID = userGroupId });
            await this._context.SaveChangesAsync();

            return "Связь пользователя с группой успешно удалена.";
        }
    }
}
