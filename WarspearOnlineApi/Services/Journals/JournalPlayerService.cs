using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Extensions;
using WarspearOnlineApi.Models.Dto;
using WarspearOnlineApi.Models.Dto.Journals;
using WarspearOnlineApi.Models.Entity;
using WarspearOnlineApi.Models.Filters;
using WarspearOnlineApi.Services.Base;

namespace WarspearOnlineApi.Services.Journals
{
    /// <summary>
    /// Сервис для работы с журналом игроков.
    /// </summary>
    public class JournalPlayerService : BaseService
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
        public JournalPlayerService(AppDbContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// Получить список игроков журнала.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список игроков журнала.</returns>
        public async Task<JournalPlayerDto[]> GetJournalPlayers(JournalPlayerFilterDto filter)
        {
            var players = await this.BuildFilter(filter)
                .OrderByDescending(x => x.PlayerID)
                .ProjectTo<JournalPlayerDto>(this._mapper.ConfigurationProvider)
                .SkipTake(filter)
                .ToArrayAsync();

            if (players.IsNullOrDefault())
            {
                return players;
            }

            var playerIds = players.Select(x => x.Player.Id).ToArray();
            var dropPlayer = await this._context.wo_DropPlayer
                .Where(x => playerIds.Contains(x.rf_PlayerID))
                .Select(x => new
                {
                    x.rf_DropID,
                    x.rf_PlayerID,
                    x.IsPaid,
                })
                .ToArrayAsync();

            if (dropPlayer.IsNullOrDefault())
            {
                return players;
            }

            var dtopIds = dropPlayer.Select(x => x.rf_DropID).Distinct().ToArray();
            var dropInfo = await this._context.wo_DropPlayer
                .Where(x => dtopIds.Contains(x.rf_DropID))
                .GroupBy(x => new { x.rf_Drop.DropID, x.rf_Drop.Price })
                .Select(x => new
                {
                    x.Key.DropID,
                    Part = x.Key.Price / x.Count(),
                }).ToArrayAsync();

            foreach (var player in players)
            {
                var dropsByPlayer = dropPlayer.Where(x => x.rf_PlayerID == player.Player.Id).ToArray();
                player.ParticipationCount = dropsByPlayer.Length;
                player.PaidOut = dropInfo.Where(x => dropsByPlayer.Any(y => y.IsPaid && y.rf_DropID == x.DropID)).Sum(x => x.Part);
                player.NotPaid = dropInfo.Where(x => dropsByPlayer.Any(y => !y.IsPaid && y.rf_DropID == x.DropID)).Sum(x => x.Part);
            }

            return players;
        }

        /// <summary>
        /// Получить журнал игроков.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Количество игроков.</returns>
        public async Task<int> GetJournalPlayersCount(JournalPlayerFilterDto filter)
        {
            return await this.BuildFilter(filter).CountAsync();
        }

        /// <summary>
        /// Построение фильтра для журнала.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Запрос для фильтрации записей.</returns>
        private IQueryable<wo_Player> BuildFilter(JournalPlayerFilterDto filter)
        {
            var query = this._context.wo_Player
                .Where(x => x.PlayerID > 0)
                .Where(x => x.rf_ServerID == filter.ServerId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор сервера."))
                .Where(x => x.rf_FractionID == filter.FractionId.ThrowOnCondition(x => x.IsNullOrDefault(), "Не указан идентификатор фракции."));

            if (!filter.Nick.IsNullOrDefault())
            {
                query = query.Where(x => x.Nick.Contains(filter.Nick.Trim()));
            }

            return query;
        }
    }
}
