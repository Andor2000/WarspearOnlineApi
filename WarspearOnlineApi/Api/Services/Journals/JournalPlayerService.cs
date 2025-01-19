using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Enums.BaseRecordDB;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Models.Dto.Journals;
using WarspearOnlineApi.Api.Models.Entity;
using WarspearOnlineApi.Api.Models.Filters;
using WarspearOnlineApi.Api.Services.Base;

namespace WarspearOnlineApi.Api.Services.Journals
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
                .ProjectTo<JournalPlayerDto>(_mapper.ConfigurationProvider)
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
                }).ToArrayAsync();

            if (dropPlayer.IsNullOrDefault())
            {
                return players;
            }

            var dtopIds = dropPlayer.Select(x => x.rf_DropID).Distinct().ToArray();

            var dropInfo = await this._context.wo_DropPlayer
                .Where(x => dtopIds.Contains(x.rf_DropID))
                .GroupBy(x => new { x.rf_Drop.DropID, x.rf_Drop.Price, x.rf_Drop.rf_DropStatus.DropStatusCode })
                .Select(x => new
                {
                    x.Key.DropID,
                    Part = x.Key.Price / x.Count(),
                    StatucCode = x.Key.DropStatusCode,
                }).ToArrayAsync();

            foreach (var player in players)
            {
                var dropsByPlayer = dropPlayer.Where(x => x.rf_PlayerID == player.Player.Id).ToArray();
                player.ParticipationCount = dropsByPlayer.Length;
                player.PaidOut = dropInfo.Where(x => dropsByPlayer.Any(y => y.IsPaid && y.rf_DropID == x.DropID)).Sum(x => x.Part);
                player.NotPaid = dropInfo
                    .Where(x => x.StatucCode != DropStatusEnum.Closed &&
                                dropsByPlayer.Any(y => !y.IsPaid && y.rf_DropID == x.DropID))
                    .Sum(x => x.Part);
                player.NotPaidClosed = dropInfo
                    .Where(x => x.StatucCode == DropStatusEnum.Closed &&
                                dropsByPlayer.Any(y => !y.IsPaid && y.rf_DropID == x.DropID))
                    .Sum(x => x.Part);
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
            filter.ThrowOnCondition(x => x.ServerId.IsNullOrDefault(), "Не указан идентификатор сервера")
                .ThrowOnCondition(x => x.FractionId.IsNullOrDefault(), "Не указан идентификатор фракции");

            var query = this._context.wo_Player
                .Where(x => x.PlayerID > 0)
                .Where(x => x.rf_ServerID == filter.ServerId)
                .Where(x => x.rf_FractionID == filter.FractionId);

            if (!filter.Nick.IsNullOrDefault())
            {
                query = query.Where(x => x.Nick.Contains(filter.Nick.Trim()));
            }

            return query;
        }
    }
}
