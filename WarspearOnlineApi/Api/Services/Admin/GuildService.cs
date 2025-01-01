using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Models.BaseModels;
using WarspearOnlineApi.Api.Services.Base;
using WarspearOnlineApi.Api.Services.Users;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с гильдиями.
    /// </summary>
    public class GuildService : AdminBaseService
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
        public GuildService(
            AppDbContext context,
            JwtTokenService jwtTokenService,
            IMapper mapper) : base(context, jwtTokenService)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение списка гильдий.
        /// </summary>
        public async Task<CodeNameBaseModel[]> GetGuilds()
        {
            var userId = this._jwtTokenService.GetUserIdFromToken();
            var user = await this.GetAdminUserModel();
            
            return await this._context.wo_Guild
                .Where(x => x.rf_ServerID == user.ServerId && x.rf_FractionID == user.FractionId)
                .ProjectTo<CodeNameBaseModel>(this._mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        //public async Task
    }
}
