using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPoco.Expressions;
using System.Linq;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Models.Filters;
using WarspearOnlineApi.Api.Services;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Journals
{
    /// <summary>
    /// Сервис для работы с журналом дропа.
    /// </summary>
    public class JournalDropService : BaseService
    {
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
        /// <param name="dropService">Сервис для работы со страницей дропа.</param>
        public JournalDropService(AppDbContext context,
            IMapper mapper,
            DropService dropService) : base(context)
        {
            _mapper = mapper;
            _dropService = dropService;
        }

        /// <summary>
        /// Получить журнал дропа.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Журнал дропа.</returns>
        public async Task<DropDto[]> GetJournalDrop(JournalDropFilter filter)
        {
            var drops = await BuildFilter(filter)
                .OrderByDescending(x => x.DropID)
                .ProjectTo<DropDto>(_mapper.ConfigurationProvider)
                .SkipTake(filter)
                .ToArrayAsync();

            if (drops.IsNullOrDefault())
            {
                return drops;
            }

            await _dropService.SetPlayerCountAndPart(drops);
            return drops;
        }

        /// <summary>
        /// Получение количества дропа в журнале.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количества дропа.</returns>
        public async Task<int> GetJournalDropCount(JournalDropFilter filter)
        {
            return await BuildFilter(filter).CountAsync();
        }

        /// <summary>
        /// Построение фильтра для журнала.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Запрос для фильтрации записей.</returns>
        private IQueryable<wo_Drop> BuildFilter(JournalDropFilter filter)
        {
            var query = _context.wo_Drop
                .Where(x => x.DropID > 0)
                .Where(x => x.rf_ServerID == filter.ServerId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера."))
                .Where(x => x.rf_FractionID == filter.FractionId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор фракции."));

            if (!filter.GroupId.IsNullOrDefault())
            {
                query = query.Where(x => x.rf_GroupID == filter.GroupId);
            }

            if (!filter.ObjectId.IsNullOrDefault())
            {
                query = query.Where(x => x.rf_ObjectID == filter.ObjectId);
            }

            if (!filter.ObjectTypeId.IsNullOrDefault())
            {
                query = query.Where(x => x.rf_Object.rf_ObjectTypeID == filter.ObjectTypeId);
            }

            if (!filter.PlayerId.IsNullOrDefault())
            {
                var playerPredicate = PredicateBuilder.True<wo_DropPlayer>()
                    .And(dp => dp.rf_PlayerID == filter.PlayerId);

                playerPredicate = filter.DropPaymentStatusPlayer switch
                {
                    DropPaymentStatus.Paid => playerPredicate.And(dp => dp.IsPaid),
                    DropPaymentStatus.NotPaid => playerPredicate.And(dp => !dp.IsPaid),
                    _ => playerPredicate
                };

                query = query.Where(x => x.DropPlayers.AsQueryable().Any(playerPredicate));
            }

            return query;
        }
    }
}
