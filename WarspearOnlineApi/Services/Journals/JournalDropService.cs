using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Filters;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services.Journals
{
    /// <summary>
    /// Сервис для работы с журналом дропа.
    /// </summary>
    public class JournalDropService : BaseService
    {
        /// <summary>
        /// Сервис для работы с игроками.
        /// </summary>
        private readonly PlayerService _playerService;

        /// <summary>
        /// Сервис для работы со страницей дропа.
        /// </summary>
        private readonly DropService _dropService;

        /// <summary>
        /// Маппер.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст данных.</param>
        /// <param name="mapper">Маппер.</param>
        public JournalDropService(AppDbContext context,
            PlayerService playerService,
            DropService dropService,
            IMapper mapper) : base(context)
        {
            this._playerService = playerService;
            this._dropService = dropService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Журнал дропа.</returns>
        public async Task<IEnumerable<DropDto>> GetJournalDrop(DropFilter filter)
        {
            var drops = await _context.wo_Drop
                .Where(x => x.DropID > 0)
                .ProjectTo<DropDto>(this._mapper.ConfigurationProvider)
                .Take(filter.Take)
                .ToArrayAsync();

            if (drops.IsNullOrDefault())
            {
                return drops;
            }

            await this._dropService.SetPlayerCountAndPart(drops);
            return drops;
        }

        /// <summary>
        /// Получение количества дропа в журнале.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количества дропа.</returns>
        public async Task<int> GetJournalDropCount(DropFilter filter)
        {
            return await this._context.wo_Drop
                .Where(x => x.DropID > 0)
                .CountAsync();
        }
    }
}
