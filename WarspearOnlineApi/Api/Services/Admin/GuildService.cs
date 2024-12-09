using AutoMapper;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Admin
{
    /// <summary>
    /// Сервис для работы с гильдиями.
    /// </summary>
    public class GuildService : BaseService
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
        public GuildService(AppDbContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение списка гильдий.
        /// </summary>
        /// <param name="serverId">Идентификатор сервера.</param>
        /// <param name="fractionId">Идентификатор фракции.</param>
        public async Task GetGuilds(int serverId, int fractionId)
        {

        }
    }
}
